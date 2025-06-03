using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace glassesRecommendation.Core.DTOs.Requests
{
    public class RemoveGlassesRequestDto
    {
        [Range(1, long.MaxValue)]
        public long GlassesId { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
