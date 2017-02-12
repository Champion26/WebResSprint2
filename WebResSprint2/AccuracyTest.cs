using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;


namespace WebResSprint2
{
    class AccuracyTest
    {
        public string _startDate;
        public int User;
        public List<VideoWeight> RecommendedVideos;

        public AccuracyTest(string startDate, int user, List<VideoWeight> recVideos)
        {
            this._startDate = startDate;
            this.User = user;
            this.RecommendedVideos = recVideos;
        }

        public double TestVideoAccuracy()
        {
            double percentage = 0.0;
            DataTable dt = SQL.GetData(String.Concat("SELECT uv.VideoId, v.VideoName FROM UserVideo AS uv JOIN Video AS v ON uv.VideoId = v.VideoId Where UserId = ", User, "AND ViewDate > Convert(date, '", this._startDate, "')"), new List<KeyValuePair<SqlParameter, string>>());

            int totalWatched = 0;

            foreach (DataRow dr in dt.Rows)
            {
                int x = 0;
                if (Int32.TryParse(dr["VideoId"].ToString(), out x))
                {
                    foreach (VideoWeight vw in RecommendedVideos)
                    {
                        if (vw.Name == dr["VideoName"].ToString())
                        {
                            totalWatched += 1;
                        }
                    }
                }
            }

            percentage = (totalWatched / RecommendedVideos.Count) * 100;

            return percentage;
        }

    }
}
