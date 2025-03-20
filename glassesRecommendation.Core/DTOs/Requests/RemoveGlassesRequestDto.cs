using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace glassesRecommendation.Core.DTOs.Requests
{
	public class RemoveGlassesRequestDto
	{
		public long GlassesId { get; set; }
		public string Email { get; set; }
	}
}
