using System.Text.Json.Serialization;

namespace glassesRecommendation.Core.Models
{
    public class User
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<Glasses> Glasses { get; set; } = [];
    }
}
