using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace WebResSprint2
{
    class SQL
    {

        public static DataTable GetData(string sql, List<KeyValuePair<SqlParameter, string>> paramList)
        {

            DataTable dt = new DataTable();
            SqlConnection conn = new SqlConnection();
            conn.ConnectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\mcham\OneDrive\Documents\Visual Studio 2013\Projects\WebResSprint2\WebResSprint2\DataSpecificUser - Copy.mdf;Integrated Security=True";
            conn.Open();
            
            // use the connection here

            StringBuilder sb = new StringBuilder(sql);
            List<SqlParameter> spList = new List<SqlParameter>();
            if (paramList.Count > 0)
            {
                foreach (KeyValuePair<SqlParameter, string> sp in paramList)
                {
                    sb.Append(sp.Value);
                    spList.Add(sp.Key);
                }
            }

            sb.Append(";");

            SqlCommand command = new SqlCommand(sb.ToString(), conn);

            foreach (SqlParameter p in spList)
            {
                command.Parameters.Add(p);
            }
            SqlDataReader reader = command.ExecuteReader();
            dt.Load(reader);
            conn.Close();
            //conn.Dipose();

            return dt;


        }

        public static DataTable GetVideoTags(int VideoId)
        {
              return SQL.GetData(String.Concat("SELECT vt.Tag_TagId, t.TagName FROM VideoTag AS vt JOIN Tag AS t ON vt.Tag_TagId = t.TagId Where Video_VideoId= ", VideoId), new List<KeyValuePair<SqlParameter, string>>());
        }
    }
}
