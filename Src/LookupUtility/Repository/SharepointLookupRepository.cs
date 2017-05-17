using Microsoft.SharePoint.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizTalkComponents.Utilities.LookupUtility.Repository
{
    public class SharepointLookupRepository : ILookupRepository
    {
        public Dictionary<string, string> LoadList(string list)
        {
            var site = ConfigurationManager.AppSettings["SharePointSite"];

            ClientContext clientContext = new ClientContext(site);
            Web oWebsite = clientContext.Web;
            ListCollection collList = oWebsite.Lists;

            var spList = collList.GetByTitle(list);

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

                dictionary.Add(keyName.ToString(), valueName as string);
            }

            return dictionary;
        }
    }
}
