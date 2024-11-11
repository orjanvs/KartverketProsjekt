using System.Text.Json.Serialization;

// Namespace for API models in KartverketProsjekt
namespace KartverketProsjekt.API_Models
{
    // Class representing the response structure for place names (Stedsnavn) API
    public class StedsnavnResponse
    {
        [JsonPropertyName("metadata")]
        public Metadata Metadata { get; set; }

        [JsonPropertyName("navn")]
        public List<Navn> Navn { get; set; } = new List<Navn>();
    }

    // Class representing metadata for pagination and search details
    public class Metadata
    {
        [JsonPropertyName("side")]
        public int? Side { get; set; }

        [JsonPropertyName("sokeStreng")]
        public string? SokeStreng { get; set; }

        [JsonPropertyName("totaltAntallTreff")]
        public int? TotaltAntallTreff { get; set; }

        [JsonPropertyName("treffPerSide")]
        public int? TreffPerSide { get; set; }

        [JsonPropertyName("viserFra")]
        public int? ViserFra { get; set; }

        [JsonPropertyName("viserTil")]
        public int? ViserTil { get; set; }
    }

    // Class representing details of a place name
    public class Navn
    {
        [JsonPropertyName("fylker")]
        public List<Fylke> Fylker { get; set; } = new List<Fylke>();

        [JsonPropertyName("kommuner")]
        public List<Kommune> Kommuner { get; set; } = new List<Kommune>();

        [JsonPropertyName("navneobjekttype")]
        public string? Navneobjekttype { get; set; }

        [JsonPropertyName("navnestatus")]
        public string? Navnestatus { get; set; }

        [JsonPropertyName("representasjonspunkt")]
        public Representasjonspunkt Representasjonspunkt { get; set; }

        [JsonPropertyName("skrivemåte")]
        public string? Skrivemåte { get; set; }

        [JsonPropertyName("skrivemåtestatus")]
        public string? Skrivemåtestatus { get; set; }

        [JsonPropertyName("språk")]
        public string? Språk { get; set; }

        [JsonPropertyName("stedsnummer")]
        public int? Stedsnummer { get; set; }

        [JsonPropertyName("stedstatus")]
        public string? Stedstatus { get; set; }
    }

    // Class representing county (fylke) information
    public class Fylke
    {
        [JsonPropertyName("fylkesnavn")]
        public string? Fylkesnavn { get; set; }

        [JsonPropertyName("fylkesnummer")]
        public string? Fylkesnummer { get; set; }
    }

    // Class representing municipality information
    public class Kommune
    {
        [JsonPropertyName("kommunenavn")]
        public string? Kommunenavn { get; set; }

        [JsonPropertyName("kommunenummer")]
        public string? Kommunenummer { get; set; }
    }

    // Class representing coordinates in a coordinate system
    public class Representasjonspunkt
    {
        [JsonPropertyName("koordsys")]
        public int? Koordsys { get; set; }

        [JsonPropertyName("nord")]
        public double? Nord { get; set; }

        [JsonPropertyName("\u00f8st")]
        public double? Øst { get; set; }
    }
}
