using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace glassesRecommendation.Core.Models
{
    public class Glasses
    {
        public long Id { get; set; }
        [Column(TypeName = "nvarchar(max)")]
        public string Image { get; set; }
        public string GlassesType { get; set; }
        public string Link { get; set; }
        public bool IsRecycling { get; set; }
        public bool IsActive { get; set; } = true;
        public long Likes { get; set; }
        public long Views { get; set; }
        public bool Oval { get; set; }
        public bool Oblong { get; set; }
        public bool Heart { get; set; }
        public bool Round { get; set; }
        public bool Square { get; set; }
		[JsonIgnore]
		public List<User> Users { get; set; } = [];
    }
}
