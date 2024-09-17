namespace DrexlerMacaspac.Models
{
    public class PopulationData
    {
        public List<PopulationRecord> Data { get; set; } = new List<PopulationRecord>();
        public List<Source> Source { get; set; } = new List<Source>();
    }

    public class PopulationRecord
    {
        public string IDNation { get; set; } = string.Empty;
        public string Nation { get; set; } = string.Empty;
        public int IDYear { get; set; }
        public string Year { get; set; } = string.Empty;
        public int Population { get; set; }
        public string SlugNation { get; set; } = string.Empty;
    }

    public class Source
    {
        public List<string> Measures { get; set; } = new List<string>();
        public Annotations Annotations { get; set; } = new Annotations();
        public string Name { get; set; } = string.Empty;
        public List<string> Substitutions { get; set; } = new List<string>();
    }

    public class Annotations
    {
        public string SourceName { get; set; } = string.Empty;
        public string SourceDescription { get; set; } = string.Empty;
        public string DatasetName { get; set; } = string.Empty;
        public string DatasetLink { get; set; } = string.Empty;
        public string TableId { get; set; } = string.Empty;
        public string Topic { get; set; } = string.Empty;
        public string Subtopic { get; set; } = string.Empty;
    }
}
