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
        string AccStartDate = "2016-06-06";

        List<int> PreRecVideos;

        public Form1()
        {
            InitializeComponent();
            UserIdList = new List<int>();
            PreRecVideos = new List<int>();

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
                DataTable dt = SQL.GetData(String.Concat("SELECT TOP 25 uv.VideoId, v.VideoName, uv.ViewDate FROM UserVideo AS uv JOIN Video AS v ON uv.VideoId = v.VideoId Where UserId = ", user, "Order By ViewDate ASC"), new List<KeyValuePair<SqlParameter, string>>());

                if (dt.Rows.Count > 0 && user == 2)
                {
                    StringBuilder sb = new StringBuilder("VideoId NOT IN (");
                    foreach (DataRow dr in dt.Rows)
                    {
                        int vid = 0;
                        Int32.TryParse(dr["VideoId"].ToString(), out vid);
                        PreRecVideos.Add(vid);
                        sb.Append(vid);
                        sb.Append(",");
                    }
                    sb.Length--;
                    sb.Append(")");

                    int recNumber = 10;
                    object md = dt.Compute("MAX(ViewDate)", null);


                    DataTable unwatched = SQL.GetData(String.Concat("SELECT VideoId, VideoName, AverageWatchTime, PercentageLiked FROM Video WHERE ", sb.ToString()), new List<KeyValuePair<SqlParameter, string>>());
                    if (dt.Rows.Count > 0 && user == 2)
                    {
                        content = new Content(user, HalfDate, dt, unwatched);
                        UpdateLabelRec(lblContentRec, content.GetRecommendedVideos(recNumber));
                        ContentTotal += TestAccuracy(this.AccStartDate, user, content.GetRecommendedVideos(recNumber), sb.ToString(), md);
                        pop = new Popularity(unwatched);
                        UpdateLabelRec(lblPopularityRec, pop.GetRecommendedVideos(recNumber));

                        PopularityTotal += TestAccuracy(this.AccStartDate, user, pop.GetRecommendedVideos(recNumber), sb.ToString(), md);
                        hybrid = new Hybrid(content.GetRecommendedVideos());
                        UpdateLabelRec(lblHybridRec, hybrid.GetRecommendedVideos(recNumber));

                        HybridTotal += TestAccuracy(this.AccStartDate, user, hybrid.GetRecommendedVideos(recNumber), sb.ToString(), md);
                        UserTotal += 1;
                    }
                }
         
            
            }

            //ContentTotal = ContentTotal / UserTotal;
            //PopularityTotal = PopularityTotal / UserTotal;
            //HybridTotal = HybridTotal / UserTotal;
            lblContentValue.Text = String.Concat(ContentTotal, "%");
            lblPoplarityValue.Text = String.Concat(PopularityTotal, "%");
            lblHybridValue.Text = String.Concat(HybridTotal, "%");


            

        }

        private void UpdateLabelRec(Label lbl, List<VideoWeight> lvw)
        {
            StringBuilder sb = new StringBuilder();
            int it = 1;
            foreach (VideoWeight vw in lvw)
            {
                sb.Append(vw.Name);
                sb.Append("| ");
                sb.Append(it);
                sb.Append("\r");
                it += 1;
            }
            lbl.Text = sb.ToString();
        }

        private double TestAccuracy(string startdate, int user, List<VideoWeight> recommended, string excludeSql, object md)
        {
            AccuracyTest at = new AccuracyTest(startdate, user, recommended, md);
            return Math.Round(at.TestVideoAccuracy(excludeSql));
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
