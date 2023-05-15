using Microsoft.AspNetCore.Http;


namespace Data.Entities
{
    public class Restaurant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Image { get; set; }

        public string ImagePath { get; set; }

    }
}
