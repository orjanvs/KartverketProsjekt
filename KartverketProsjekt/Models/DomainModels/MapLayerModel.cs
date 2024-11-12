namespace KartverketProsjekt.Models.DomainModels
{
    /// <summary>
    /// Represents a map layer with a unique identifier and type.
    /// </summary>
    public class MapLayerModel
    {
        public int MapLayerId { get; set; } // Primary key
        public string MapLayerType { get; set; }
    }
}