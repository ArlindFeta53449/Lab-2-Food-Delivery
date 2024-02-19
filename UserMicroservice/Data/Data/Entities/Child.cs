

namespace Data.Entities
{
    public class Child
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Difficulty { get; set; }


        public int ParentId { get; set; }
        public Parent Parent { get; set; }
    }
}
