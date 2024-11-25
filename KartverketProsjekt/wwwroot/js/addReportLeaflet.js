// Function for request to Kommune API, makes a httprequest, returns response or reject error
function ajax(url) {
    return new Promise(function (resolve, reject) {
        var xhr = new XMLHttpRequest();
        xhr.onload = function () {
            resolve(this.responseText);
        };
        xhr.onerror = reject;
        xhr.open('GET', url);
        xhr.send();
    });
}
// Wait for the page to load before initializing the map
document.addEventListener("DOMContentLoaded", function () {
    // Using browser geolocation to get the user's position
    navigator.geolocation.getCurrentPosition(
        function (position) {
            var latitude = position.coords.latitude;
            var longitude = position.coords.longitude;
            initializeMap(latitude, longitude);
        },
        function (error) {
            // Default coordinates for Kristiansand
            var latitude = 58.1467;
            var longitude = 7.9956;
            initializeMap(latitude, longitude);
        }
    );

    // Init map
    function initializeMap(latitude, longitude) {
        // Add map layers
        var baseMaps = {
            "Fargekart": L.tileLayer('https://cache.kartverket.no/v1/wmts/1.0.0/topo/default/webmercator/{z}/{y}/{x}.png', { maxZoom: 18, attribution: '&copy; <a href="http://www.kartverket.no/">Kartverket</a>' }),
            "Gråtonekart": L.tileLayer('https://cache.kartverket.no/v1/wmts/1.0.0/topograatone/default/webmercator/{z}/{y}/{x}.png', { maxZoom: 18, attribution: '&copy; <a href="http://www.kartverket.no/">Kartverket</a>' }),
            "Turkart": L.tileLayer('https://cache.kartverket.no/v1/wmts/1.0.0/toporaster/default/webmercator/{z}/{y}/{x}.png', { maxZoom: 18, attribution: '&copy; <a href="http://www.kartverket.no/">Kartverket</a>' }),
            "Sjøkart": L.tileLayer('https://cache.kartverket.no/v1/wmts/1.0.0/sjokartraster/default/webmercator/{z}/{y}/{x}.png', { maxZoom: 18, attribution: '&copy; <a href="http://www.kartverket.no/">Kartverket</a>' })
        };

        // Initialising map with geolocation variables as position and default map layer
        var map = L.map('map', {
            center: [latitude, longitude],
            zoom: 14,
            layers: [baseMaps["Fargekart"]] // Set default layer
        });

        // Set default MapLayerId to 1
        document.getElementById('mapLayerIdInput').value = 1;

        // Add button to toggle map layers
        L.control.layers(baseMaps).addTo(map);

        var geocoder = L.Control.geocoder({
            geocoder: new L.Control.Geocoder.Nominatim({
                geocodingQueryParams: {
                    "viewbox": "1.625977, 57.610107, 32.333008, 71.773941"
                }
            }),
            defaultMarkGeocode: false,
            collapsed: false,
            position: ('bottomleft'),
        })
            .on('markgeocode', function (e) {
                var latlng = e.geocode.center;
                map.setView(latlng, 13);
            })
            .addTo(map);

        // Initializes the feature that will hold the drawn shapes and adds it to the map layer
        var drawnItems = new L.FeatureGroup();
        map.addLayer(drawnItems);

        // Creates draw toolbar on left-hand side
        var drawControl = new L.Control.Draw({
            draw: {
                marker: true,
                polyline: true,
                polygon: true,
                circle: false, // Disable the circle tool because of error with the GeoJson data
                circlemarker: false,
                rectangle: true,
            },
            edit: {
                featureGroup: drawnItems,
                edit: false, // Disable the edit functionality
                remove: true // Enable the remove functionality
            }
        });
        map.addControl(drawControl);
        


        // Event listener for when a shape is drawn
        map.on('draw:created', (e) => {
            var type = e.layerType,
                layer = e.layer;

            // Clear existing drawn items
            drawnItems.clearLayers();

            // Add the new layer
            drawnItems.addLayer(layer);

            // Convert the drawn shape to GeoJSON and set the value of the input field
            var geoJsonString = JSON.stringify(layer.toGeoJSON());

            // Set the value of the input field to the GeoJSON string
            document.getElementById('geoJsonInput').value = geoJsonString;


            var centroid = turf.centroid(JSON.parse(geoJsonString));
            
            var thisLon = centroid.geometry.coordinates[0];
            var thisLat = centroid.geometry.coordinates[1];

            var apiUrl = 'https://api.kartverket.no/kommuneinfo/v1/punkt?nord=' + thisLat + '&ost=' + thisLon + '&koordsys=4258';

            ajax(apiUrl)
                .then(function (result) {
                    var kommuneInfo = JSON.parse(result);
                    document.getElementById('municipalityInput').value = kommuneInfo.kommunenavn;
                    document.getElementById('countyInput').value = kommuneInfo.fylkesnavn;
                })
                .catch(function () {
                    console.log('error');
                });
        });

        // Event listener for when the base layer is changed
        map.on('baselayerchange', function (e) {
            var mapLayerId = determineMapLayerIdFromName(e.name);
            document.getElementById('mapLayerIdInput').value = mapLayerId;
            console.log(mapLayerId);
            console.log(e);
            console.log('MapLayerIdInput');
        });

        function determineMapLayerIdFromName(name) {
            // Implement logic to determine MapLayerId based on the layer name
            switch (name) {
                case "Fargekart":
                    return 1;
                case "Gråtonekart":
                    return 2;
                case "Turkart":
                    return 3;
                case "Sjøkart":
                    return 4;
                default:
                    return 1;
            }
            console.log(name);
        }
    }
});