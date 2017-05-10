using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizTalkComponents.Utilities.LookupUtility.Repository
{
    public class SqlLookupRepository : ILookupRepository
    {
        public Dictionary<string, string> LoadList(string list)
        {
            string query = string.Format("SELECT Key, Value FROM {0}", list);

            var connectionString = ConfigurationManager.ConnectionStrings["SqlLookupRepository"].ConnectionString;
            var dictionary = new Dictionary<string, string>();

            using (var con = new SqlConnection())
            {
                con.Open();
                using (var cmd = new SqlCommand(query, con))
                {
                    var reader = cmd.ExecuteReader();

                    while(reader.Read())
                    {
                        dictionary.Add(reader.GetString(0), reader.GetString(1));
                    }

                    reader.Close();
                }
            }

            return dictionary;
        }
    }
}
