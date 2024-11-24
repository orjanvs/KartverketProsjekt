// Function for generating markers using the centre point + preview functionality
function generateMarker(id, geojsonstring, maplayerid) {
    var geoJson = L.geoJson(geojsonstring,
        {
            onEachFeature: function (feature, layer) {
                var centroid = turf.centroid(feature);
                var lon = centroid.geometry.coordinates[0];
                var lat = centroid.geometry.coordinates[1];
                var marker = L.marker([lat, lon]);
                var title = `<a>${id}</a>`;
                marker.bindPopup(title);
                marker.on('click', function (e) {
                    $('#previewInfo').collapse('show');
                    $.get(`/MapReport/PreviewMapReport/${id}`, function (data) {
                        $('#loadPreview').html(data);
                    });
                });

                markerGroups[maplayerid - 1].addLayer(marker);
            }
        });
}

// Defining markerGroups for displaying markers according to selected basemap
var markerGroup1 = L.markerClusterGroup();
var markerGroup2 = L.markerClusterGroup();
var markerGroup3 = L.markerClusterGroup();
var markerGroup4 = L.markerClusterGroup();
var markerGroup5 = L.markerClusterGroup();

var markerGroups = [markerGroup1, markerGroup2, markerGroup3, markerGroup4, markerGroup5];

// Wait for the page to load before initializing the map
document.addEventListener("DOMContentLoaded", function () {
    // Add map layers
    var baseMaps = {
        "Fargekart": L.tileLayer('https://cache.kartverket.no/v1/wmts/1.0.0/topo/default/webmercator/{z}/{y}/{x}.png', { attribution: '&copy; <a href="http://www.kartverket.no/">Kartverket</a>' }),
        "Gråtonekart": L.tileLayer('https://cache.kartverket.no/v1/wmts/1.0.0/topograatone/default/webmercator/{z}/{y}/{x}.png', { attribution: '&copy; <a href="http://www.kartverket.no/">Kartverket</a>' }),
        "Turkart": L.tileLayer('https://cache.kartverket.no/v1/wmts/1.0.0/toporaster/default/webmercator/{z}/{y}/{x}.png', { attribution: '&copy; <a href="http://www.kartverket.no/">Kartverket</a>' }),
        "Sjøkart": L.tileLayer('https://cache.kartverket.no/v1/wmts/1.0.0/sjokartraster/default/webmercator/{z}/{y}/{x}.png', { attribution: '&copy; <a href="http://www.kartverket.no/">Kartverket</a>' })
    };

    // Initialising map with geolocation variables as position and default map layer
    var map = L.map('map', {
        center: [60.14, 10.25],
        zoom: 6,
        layers: [baseMaps["Fargekart"]], // Set default layer
        zoomControl: false
    });

    // Positioning the zoom controls
    L.control.zoom({
        position: 'bottomright'
    }).addTo(map);

    // Add button to toggle map layers
    L.control.layers(baseMaps).addTo(map);

    // Add initial markerGroup
    map.addLayer(markerGroup1);

    // Functionality for hiding preview card
    map.on('click', function (e) {
        $('#previewInfo').collapse('hide');
    });

    // Replace current markers when changing base layer
    map.on('baselayerchange', function (event) {
        for (const markerGroup of markerGroups) {
            map.removeLayer(markerGroup);
        }
        map.removeLayer(markerGroup1)
        if (event.name === "Gråtonekart") {
            map.addLayer(markerGroup2);
        } else if (event.name === "Turkart") {
            map.addLayer(markerGroup3);
        } else if (event.name === "Sjøkart") {
            map.addLayer(markerGroup4);
        } else {
            map.addLayer(markerGroup1);
        }
    });
});