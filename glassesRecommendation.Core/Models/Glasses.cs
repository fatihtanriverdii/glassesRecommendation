using System.ComponentModel.DataAnnotations.Schema;

namespace glassesRecommendation.Core.Models
{
    public class Glasses
    {
        public long Id { get; set; }
        [Column(TypeName = "nvarchar(max)")]
        public string Image { get; set; }
        public string GlassesType { get; set; }
        public List<User> Users { get; set; } = [];
    }
}
