using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizTalkComponents.Utilities.LookupUtility.Repository
{
    public class SQLLookupRepository : ILookupRepository
    {
        public Dictionary<string, string> LoadList(string list)
        {
            var conStr = ConfigurationManager.AppSettings["LookupUtilityConnection"];
            var commandString = string.Format("SELECT Key, Value FROM {0}", list);
            var dict = new Dictionary<string, string>();

            using (var con = new SqlConnection(conStr))
            {
                con.Open();

                using (var cmd = new SqlCommand(commandString, con))
                {
                    SqlDataReader reader = null;
                    try
                    {
                        reader = cmd.ExecuteReader();
                    }
                    catch (SqlException)
                    {
                        throw new ArgumentException(string.Format("Unable to read list {0} from SQL", list));
                    }

                    while (reader.Read())
                    {
                        dict.Add(reader.GetString(0), reader.GetString(1));
                    }
                }
            }

            return dict;
        }
    }
}
