using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;


namespace WebResSprint2
{
    class Content
    {

        List<UserProfileListItem> UserProfileTagList;
        List<VideoWeight> VideoWeights;
        List<VideoWeight> RecommendedVideos;

        public int UserId;
        public string HalfWay;

        public DataTable UserVideoHistory;
        public DataTable Unwatched;



        public Content(int ui, string d, DataTable uvh, DataTable uw)
        {

            this.UserId = ui;
            this.HalfWay = d;
            this.UserVideoHistory = uvh;
            this.Unwatched = uw;


            UserProfileTagList = new List<UserProfileListItem>();
            VideoWeights = new List<VideoWeight>();
            RecommendedVideos = new List<VideoWeight>();
    

            BuildUserProfile();

            GenerateVideoWeighting();

            SortVideoRecommendations();

        }

        public List<VideoWeight> GetRecommendedVideos(int limit = 0)
        {
            if (limit != 0 && RecommendedVideos.Count >= limit)
            {
                RecommendedVideos.RemoveRange(limit, (RecommendedVideos.Count - limit));
            }
            return RecommendedVideos;
        }

      

        private void SortVideoRecommendations()
        {
            if (VideoWeights.Count > 0)
            {
                RecommendedVideos = VideoWeights.OrderByDescending(v => v.Weight).ToList();
            }
        }

        private void GenerateVideoWeighting()
        {
            //get videos user hasn't watched yet 

            foreach (DataRow dr in Unwatched.Rows)
            {
                   int x = 0;
                   if (Int32.TryParse(dr["VideoId"].ToString(), out x))
                   {

                       DataTable dt = SQL.GetVideoTags(x);
                       //get tags of each video
                       VideoWeight vw = new VideoWeight(dr["VideoName"].ToString(), 0, 0, 0);
                       foreach (DataRow tdr in dt.Rows)
                       {
                            int y = 0;
                            if (Int32.TryParse(tdr["Tag_TagId"].ToString(), out y))
                            {
                                //get name from datarow
                                foreach (UserProfileListItem uli in UserProfileTagList)
                                {// if tag ids match  0 = tagId
                                    if (uli.Tag == y)
                                    {
                                        vw.Weight += uli.Occurance;
                                    }
                                }
                            }
                       }
                       VideoWeights.Add(vw);
                   }
            }

        }

        private void BuildUserProfile()
        {


            foreach (DataRow dr in this.UserVideoHistory.Rows)
            {
                int x = 0;
                if (Int32.TryParse(dr["VideoId"].ToString(), out x))
                {

                    DataTable dt = SQL.GetVideoTags(x);

                    if (dt.Rows.Count > 0)
                    {
                        foreach(DataRow drt in dt.Rows)
                        {
                           int y = 0;
                            if (Int32.TryParse(drt["Tag_TagId"].ToString(), out y))
                            {
                                bool found = false;
                                foreach (UserProfileListItem upli in UserProfileTagList)
                                {
                                    //if tag is found, update occurance count
                                    if (upli.Tag == y)
                                    {
                                        upli.Occurance += 1;
                                        found = true;
                                        break;
                                    }

                                }
                                if (!found)
                                {
                                    UserProfileTagList.Add(new UserProfileListItem(y, 1));
                                }
                            }

                        }
                    }

            }
         
            }
       
        }

       
    }
}

public class UserProfileListItem
{
    // obviously you find meaningful names of the 2 properties
    public UserProfileListItem(int t, int o)
    {
        this.Tag = t;
        this.Occurance = o;
    }
    public int Tag { get; set; }
    public int Occurance { get; set; }
}


public class VideoWeight
{
    // obviously you find meaningful names of the 2 properties
    public VideoWeight(string t,
                       double o,
                       double m = 0.0,
                       int awt = 0,
                       int pl = 0)
    {
        this.Name = t;
        this.Weight = o;
        this.Metric = m;
        this.AverageWatchTime = awt;
        this.PercentageLiked = pl;

    }



    public VideoWeight(){ }
    public string Name { get; set; }
    public int AverageWatchTime { get; set; }
    public int PercentageLiked { get; set; }
    public double Weight { get; set; }
    public double Metric { get; set; }

}