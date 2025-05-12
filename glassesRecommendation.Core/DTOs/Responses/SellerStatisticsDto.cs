using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace glassesRecommendation.Core.DTOs.Responses
{
	public class SellerStatisticsDto
	{
		public int TotalGlasses { get; set; }
		public int ActiveGlasses { get; set; }
		public long TotalViews { get; set; }
		public long TotalLikes { get; set; }
	}
}
