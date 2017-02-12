using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;


namespace WebResSprint2
{
    class Popularity
    {

        List<VideoWeight> RecommendedVideos;
        public DataTable Unwatched;

        public Popularity(DataTable unwatched)
        {
            RecommendedVideos = new List<VideoWeight>();
            this.Unwatched = unwatched;
            GenerateVideoMetrics();

        }

        public Popularity()
        {
            RecommendedVideos = new List<VideoWeight>();
        }

        public List<VideoWeight> GetRecommendedVideos(int limit = 0)
        {
            if (limit != 0 && RecommendedVideos.Count >= limit)
            {
                RecommendedVideos.RemoveRange(limit, (RecommendedVideos.Count - limit));
            }
            return RecommendedVideos;
        }

        public List<VideoWeight> GenerateHybridContentPopularityMetric(List<VideoWeight> vwL)
        {
            if (vwL.Count > 0)
            {
                foreach (VideoWeight vw in vwL)
                {
                    vw.Metric = (Convert.ToDouble(vw.AverageWatchTime) + (Convert.ToDouble(vw.PercentageLiked) / 100)) / 60;
                }
            }

            return vwL.OrderByDescending(v => v.Metric).ToList();

        }

        private void GenerateVideoMetrics()
        {
            //get all video records that user hasn't watched


            if (Unwatched.Rows.Count > 0)
            {
                foreach (DataRow dr in Unwatched.Rows)
                {
                    int awt = 0;
                    Int32.TryParse(dr["AverageWatchTime"].ToString(), out awt);

                    int pl = 0;
                    Int32.TryParse(dr["PercentageLiked"].ToString(), out pl);
                    
                    VideoWeight vp = new VideoWeight(dr["VideoName"].ToString(), -1, -1, awt, pl);

                    vp.Metric = (vp.AverageWatchTime + (vp.PercentageLiked / 100)) / 60; //

                    RecommendedVideos.Add(vp);
                }
            }
            RecommendedVideos = RecommendedVideos.OrderByDescending(v => v.Metric).ToList();
        }

    }

}
