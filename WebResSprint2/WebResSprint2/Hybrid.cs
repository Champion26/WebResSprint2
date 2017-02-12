using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace WebResSprint2
{
    class Hybrid
    {
        List<VideoWeight> HybridList;

        public Hybrid(List<VideoWeight> contentList)
        {
            HybridList = new List<VideoWeight>();
            Popularity pop = new Popularity();
            this.HybridList = pop.GenerateHybridContentPopularityMetric(contentList);
        }

        public List<VideoWeight> GetRecommendedVideos(int limit = 0)
        {
            if (limit != 0 && this.HybridList.Count >= limit)
            {
                this.HybridList.RemoveRange(limit, (this.HybridList.Count - limit));
            }
            return this.HybridList;
        }
    
    }
}
