﻿// Define the map layers
var baseMaps = {
    "Fargekart": L.tileLayer('https://cache.kartverket.no/v1/wmts/1.0.0/topo/default/webmercator/{z}/{y}/{x}.png', { maxZoom: 18, attribution: '&copy; <a href="http://www.kartverket.no/">Kartverket</a>' }),
    "Gråtonekart": L.tileLayer('https://cache.kartverket.no/v1/wmts/1.0.0/topograatone/default/webmercator/{z}/{y}/{x}.png', { maxZoom: 18, attribution: '&copy; <a href="http://www.kartverket.no/">Kartverket</a>' }),
    "Turkart": L.tileLayer('https://cache.kartverket.no/v1/wmts/1.0.0/toporaster/default/webmercator/{z}/{y}/{x}.png', { maxZoom: 18, attribution: '&copy; <a href="http://www.kartverket.no/">Kartverket</a>' }),
    "Sjøkart": L.tileLayer('https://cache.kartverket.no/v1/wmts/1.0.0/sjokartraster/default/webmercator/{z}/{y}/{x}.png', { maxZoom: 18, attribution: '&copy; <a href="http://www.kartverket.no/">Kartverket</a>' })
};

// Function to get the map layer based on mapLayerId
function getMapLayerById(mapLayerId) {
    switch (mapLayerId) {
        case 1:
            return baseMaps["Fargekart"];
        case 2:
            return baseMaps["Gråtonekart"];
        case 3:
            return baseMaps["Turkart"];
        case 4:
            return baseMaps["Sjøkart"];
        default:
            return baseMaps["Fargekart"];
    }
}

// Initialize the map with the correct layer
var map = L.map('map').setView([60.145, 10.25], 15);
getMapLayerById(mapLayerId).addTo(map);

// Handle the GeoJSON data if it exists
if (typeof geoJsonData !== 'undefined' && geoJsonData) {
    try {
        // Adds the GeoJSON data to the map
        L.geoJSON(geoJsonData).addTo(map);

        // Fit the map to the GeoJSON bounds
        var bounds = L.geoJSON(geoJsonData).getBounds();
        if (bounds.isValid()) {
            map.fitBounds(bounds);
        }
        // Get centroid of figure and insert lat, lon into url
        var centroid = turf.centroid(geoJsonData);
        var thisLon = centroid.geometry.coordinates[0];
        var thisLat = centroid.geometry.coordinates[1];
        var mapsUrl = 'https://www.google.com/maps/place/' + thisLat + ',' + thisLon;
        document.getElementById('googleMapsLink').href = mapsUrl;

    // Error in case of GeoJSON processing failure, e.g. formatting errors
    } catch (error) {
        console.error("Error processing GeoJSON:", error);
    }
}
