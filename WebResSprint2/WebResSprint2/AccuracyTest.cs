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
        DateTime minDate;


        public AccuracyTest(string startDate, int user, List<VideoWeight> recVideos, object MinimumDate)
        {
            this._startDate = startDate;
            this.User = user;
            this.RecommendedVideos = recVideos;
            this.minDate = Convert.ToDateTime(MinimumDate);
        }

        public double TestVideoAccuracy(string excludeSql)
        {
            double percentage = 0.0;
            DataTable dt = SQL.GetData(String.Concat("SELECT uv.VideoId, v.VideoName, uv.ViewDate FROM UserVideo AS uv JOIN Video AS v ON uv.VideoId = v.VideoId Where UserId = ", User, " AND  uv.", excludeSql), new List<KeyValuePair<SqlParameter, string>>());
            double totalWatched = 0;
            List<string> viewed = new List<string>();
            object md = dt.Compute("MAX(ViewDate)", null);
            DateTime maxDate = Convert.ToDateTime(md);
            double WaitTime = 0;
            foreach (DataRow dr in dt.Rows)
            {
                int x = 0;
                if (Int32.TryParse(dr["VideoId"].ToString(), out x))
                {
                    int i = 1;
                    foreach (VideoWeight vw in RecommendedVideos)
                    {
                        if (!viewed.Contains(dr["VideoName"].ToString()))
                        {
                            if (vw.Name == dr["VideoName"].ToString())
                            {
                                totalWatched += 1;
                                WaitTime += (i/10) * (Convert.ToDateTime(dr["ViewDate"].ToString()) - minDate).TotalDays;
                                viewed.Add(vw.Name);
                            }
                        }
                        i += 1;
                    }
                }
            }
            //list order num / 10 * wait time
            //percentage of day range so out of 100 days and average wait time is 31 then percentage will be 69
            //1.0 * 126
            //0.1 * 131
            double AverageWaitTime = WaitTime / viewed.Count;

            double dateRange = (maxDate - minDate).TotalDays;

            double waitpercentage = (100 - ((AverageWaitTime / dateRange) * 100));

            percentage = (totalWatched / Convert.ToDouble(RecommendedVideos.Count)) * 100;

            return (percentage + waitpercentage) / 2;
        }
        //400 - date // total and int minLavel = Convert.ToInt32(dt.Compute("min([AccountLevel])", string.Empty));

    }
}
