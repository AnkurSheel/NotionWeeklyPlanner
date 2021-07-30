using System.Collections.Generic;
using System.Linq;
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

        public async Task<PaginatedList<Page>> GetPages(string databaseId, string? sortProperty = null)
        {
            var databasesQueryParameters = sortProperty == null
                ? new DatabasesQueryParameters()
                : new DatabasesQueryParameters
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
                };
            return await NotionClient.Databases.QueryAsync(databaseId, databasesQueryParameters);
        }

        public async Task<Page?> GetPage(string databaseId, string title, string titlePropertyName)
        {
            var pages = await GetPages(databaseId);

            return GetPage(pages.Results, title, titlePropertyName);
        }

        public Page? GetPage(IReadOnlyList<Page> pages, string title, string titlePropertyName)
        {
            return pages.SingleOrDefault(x => x.Properties[titlePropertyName] is TitlePropertyValue titleValue && titleValue?.Title.FirstOrDefault()?.PlainText == title);
        }
    }
}
