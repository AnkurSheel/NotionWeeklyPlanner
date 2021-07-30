using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Notion.Client;
using NotionWeeklyPlanner;

namespace ConsoleApp.ConsoleApp
{
    public class PageLinker
    {
        private readonly Client _client;

        public PageLinker(Client client)
        {
            _client = client;
        }

        public async Task LinkPages(string weeklyPageId, List<string> journalPageIds)
        {
            var relations = new RelationPropertyValue
            {
                Relation = new List<ObjectId>(
                    journalPageIds.Select(
                        x => new ObjectId
                        {
                            Id = x
                        }))
            };

            IDictionary<string, PropertyValue> updatedProperties = new Dictionary<string, PropertyValue>
            {
                { "Journal", relations }
            };

            await _client.NotionClient.Pages.UpdatePropertiesAsync(weeklyPageId, updatedProperties);
        }
    }
}
