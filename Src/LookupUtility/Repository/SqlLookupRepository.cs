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
        private string connectionString;
        public SqlLookupRepository(string connection)
        {
            connectionString = ConfigurationManager.ConnectionStrings[connection].ConnectionString;
        }
        public Dictionary<string, string> LoadList(string list, TimeSpan maxAge = default(TimeSpan))
        {
            string query = string.Format("SELECT [Key], Value FROM {0}", list);

            
            var dictionary = new Dictionary<string, string>();

            using (var con = new SqlConnection(connectionString))
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
