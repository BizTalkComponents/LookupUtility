using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizTalkComponents.Utilities.LookupUtility.Repository
{
    public class SharepointLookupRepository : ILookupRepository
    {
        Microsoft.SharePoint.Client.List spList;
        ClientContext clientContext;

        public SharepointLookupRepository(string connection)
        {
            Trace.WriteLine("Initializing new Sharepoint repo");
            var site = ConfigurationManager.AppSettings[connection];
            Trace.WriteLine($"Site {site}");
            clientContext = new ClientContext(site);
            Trace.WriteLine($"Got site {site}");
        }

        public Dictionary<string, string> LoadList(string list, TimeSpan maxAge = default(TimeSpan))
        {
            Trace.WriteLine("Loading list");
            SetList(list);

            if (maxAge != default(TimeSpan) && maxAge < GetAgeOfList(list))
                spList.RefreshLoad();

            var q = new CamlQuery();
            ListItemCollection collListItem = spList.GetItems(q);
            clientContext.Load(
                collListItem,
                items => items.Include(
                item => item["Key"],
                item => item["Value"]));

            clientContext.ExecuteQuery();

            var dictionary = new Dictionary<string, string>();

            object keyName;
            object valueName;
            foreach (var item in collListItem)
            {
                item.FieldValues.TryGetValue("Key", out keyName);
                item.FieldValues.TryGetValue("Value", out valueName);
                Trace.WriteLine($"Populating list key {keyName.ToString()} value {valueName as string}");
                dictionary.Add(keyName.ToString(), valueName as string);
            }

            return dictionary;
        }

        private void SetList(string list)
        {
            Trace.WriteLine($"Getting list {list}");
            Web oWebsite = clientContext.Web;
            ListCollection collList = oWebsite.Lists;
            
            spList = collList.GetByTitle(list);
        }

        private TimeSpan GetAgeOfList(string list)
        {
            return DateTime.Now.Subtract(spList.LastItemModifiedDate);
        }
    }
}
