var map = L.map('map').setView([60.145, 10.25], 15);

L.tileLayer('https://cache.kartverket.no/v1/wmts/1.0.0/topo/default/webmercator/{z}/{y}/{x}.png', {
        maxZoom: 15,
    attribution: '&copy; <a href="http://www.kartverket.no/">Kartverket</a>' 
        }).addTo(map);

   
if (modelIntoJson != null && modelIntoJson.GeoJson) {

    try {
        // Initializing a variable with the GeoJson property value.
        var geoJsonData = modelIntoJson.GeoJson;

        // Creates new featuregroup instance to store layers from GeoJSON data
        var drawnItems = new L.FeatureGroup();

        // Adds the GeoJSON data to the map
        L.geoJSON(geoJsonData, {
            onEachFeature: function (feature, layer) {
                // Bind the popup to the layer
                layer.bindPopup(JSON.stringify(feature.properties, null, 2));
                drawnItems.addLayer(layer);
            }
        }).addTo(map);

        map.addLayer(drawnItems);

        // Fit the map to the GeoJSON bounds
        var bounds = drawnItems.getBounds();
        if (bounds.isValid()) {
            map.fitBounds(bounds);
        }
    } catch (error) {
        console.error("Error processing GeoJSON:", error)
    }

} else {
        console.error("No GeoJSON data found in the model.");
    }