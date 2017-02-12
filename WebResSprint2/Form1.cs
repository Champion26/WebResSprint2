using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WebResSprint2
{
    public partial class Form1 : Form
    {
        Popularity pop;
        Content content;
        Hybrid hybrid;
        List<int> UserIdList;
        string Users = "[1, 2]";

        string HalfDate = "2016-06-06";
        string AccStartDate = "2016-06-07";

        public Form1()
        {
            InitializeComponent();
            UserIdList = new List<int>();
            List<KeyValuePair<SqlParameter, string>> UserParameters = new List<KeyValuePair<SqlParameter, string>>();

            //UserParameters.Add(GetUserIDKVP());

            DataTable UserDt = SQL.GetData("SELECT * FROM [dbo].[User] ", UserParameters);

            foreach (DataRow dr in UserDt.Rows)
            {
                int x = 0;
                if (Int32.TryParse(dr["UserId"].ToString(), out x)){
                    UserIdList.Add(x);
                }
            }

            double ContentTotal = 0; double PopularityTotal = 0; double HybridTotal = 0; int UserTotal = 0;
            foreach (int user in UserIdList)
            {
                DataTable dt = SQL.GetData(String.Concat("SELECT uv.VideoId, v.VideoName FROM UserVideo AS uv JOIN Video AS v ON uv.VideoId = v.VideoId Where UserId = ", user, "AND ViewDate <= Convert(date, '", this.HalfDate, "')"), new List<KeyValuePair<SqlParameter, string>>());
                DataTable unwatched = SQL.GetData(String.Concat("SELECT VideoId, VideoName, AverageWatchTime, PercentageLiked FROM Video WHERE VideoId Not In ( SELECT VideoId FROM UserVideo Where UserId =  ", user, ")"), new List<KeyValuePair<SqlParameter, string>>());
                if (dt.Rows.Count > 0)
                {
                    content = new Content(user, HalfDate, dt, unwatched);
                    ContentTotal += TestAccuracy(this.AccStartDate, user, content.GetRecommendedVideos(10));
                    pop = new Popularity(unwatched);
                    PopularityTotal += TestAccuracy(this.AccStartDate, user, pop.GetRecommendedVideos(10));
                    hybrid = new Hybrid(content.GetRecommendedVideos());
                    HybridTotal += TestAccuracy(this.AccStartDate, user, hybrid.GetRecommendedVideos(10));
                    UserTotal += 1;
                }
            
            }

            ContentTotal = ContentTotal / UserTotal;
            PopularityTotal = PopularityTotal / UserTotal;
            HybridTotal = HybridTotal / UserTotal;
            lblContentValue.Text = String.Concat(ContentTotal, "%");
            lblPoplarityValue.Text = String.Concat(PopularityTotal, "%");
            lblHybridValue.Text = String.Concat(HybridTotal, "%");


            

        }

        private double TestAccuracy(string startdate, int user, List<VideoWeight> recommended)
        {
            AccuracyTest at = new AccuracyTest(startdate, user, recommended);
            return at.TestVideoAccuracy();
        }

        private KeyValuePair<SqlParameter, string> GetUserIDKVP()
        {
            SqlParameter userId = new SqlParameter("User", 1);
            string SQLClause = "WHERE UserId = @User";
            return new KeyValuePair<SqlParameter, string>(userId, SQLClause);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
