namespace SoftUniClone.Models
{
    public class Resource
    {
        public int Id { get; set; }

        public int LectorId { get; set; }
        public Lecture Lecture { get; set; }

        public int Order { get; set; }

        public ResourceType Type { get; set; }

        public string ResourceUrl { get; set; }
    }
}