using System.Collections.Generic;
using System.Threading.Tasks;
using Notion.Client;

namespace NotionWeeklyPlanner
{
    public class Client
    {
        public NotionClient NotionClient { get; }

        public Client(NotionSettings notionSettings)
        {
            var clientOptions = new ClientOptions
            {
                AuthToken = notionSettings.IntegrationToken
            };

            NotionClient = new NotionClient(clientOptions);
        }

        public async Task<PaginatedList<Page>> GetPages(string databaseId, string sortProperty)
            => await NotionClient.Databases.QueryAsync(
                databaseId,
                new DatabasesQueryParameters
                {
                    Sorts = new List<Sort>
                    {
                        new Sort
                        {
                            Property = sortProperty,
                            Timestamp = Timestamp.CreatedTime,
                            Direction = Direction.Descending
                        }
                    }
                });
    }
}
