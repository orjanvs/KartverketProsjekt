using System.Text.Json.Serialization;

// Namespace for API models in KartverketProsjekt
namespace KartverketProsjekt.API_Models
{
    // Class representing coordinate reference system (CRS) details
    public class Crs
    {
        [JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonPropertyName("properties")]
        public CrsProperties Properties { get; set; }
    }

    // Class representing properties of the CRS
    public class CrsProperties
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }
    }

    // Class representing a bounding box with coordinates and CRS
    public class Avgrensningsboks
    {
        [JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonPropertyName("coordinates")]
        public List<List<List<double>>> Coordinates { get; set; }

        [JsonPropertyName("crs")]
        public Crs Crs { get; set; }
    }

    // Class representing a point in an area with coordinates and CRS
    public class PunktIOmrade
    {
        [JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonPropertyName("coordinates")]
        public List<double> Coordinates { get; set; }

        [JsonPropertyName("crs")]
        public Crs Crs { get; set; }
    }

    // Class representing a valid name with attributes such as priority and language
    public class GyldigeNavn
    {
        [JsonPropertyName("navn")]
        public string? Navn { get; set; }

        [JsonPropertyName("prioritet")]
        public int? Prioritet { get; set; }

        [JsonPropertyName("sprak")]
        public string? Sprak { get; set; }
    }

    // Main class representing information about a municipality
    public class KommuneInfo
    {
        [JsonPropertyName("fylkesnavn")]
        public string? Fylkesnavn { get; set; }

        [JsonPropertyName("fylkesnummer")]
        public string? Fylkesnummer { get; set; }

        [JsonPropertyName("kommunenavn")]
        public string? Kommunenavn { get; set; }

        [JsonPropertyName("kommunenavnNorsk")]
        public string? KommunenavnNorsk { get; set; }

        [JsonPropertyName("kommunenummer")]
        public string? Kommunenummer { get; set; }

        [JsonPropertyName("samiskForvaltningsomrade")]
        public bool SamiskForvaltningsomrade { get; set; }

        [JsonPropertyName("avgrensningsboks")]
        public Avgrensningsboks Avgrensningsboks { get; set; }

        [JsonPropertyName("punktIOmrade")]
        public PunktIOmrade PunktIOmrade { get; set; }

        [JsonPropertyName("gyldigeNavn")]
        public List<GyldigeNavn> GyldigeNavn { get; set; }
    }
}
