using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Notion.Client;

namespace NotionWeeklyPlanner
{
    public class WeeklyPageCreator
    {
        private readonly string _databaseId;
        private readonly Client _client;

        public WeeklyPageCreator(NotionSettings notionSettings, Client client)
        {
            _client = client;
            _databaseId = notionSettings.WeeklyDatabaseId ?? throw new ArgumentNullException(nameof(notionSettings));
        }

        public async Task<string> CreateWeeklyPage(DateTime startDate)
        {
            var endDate = startDate.AddDays(6);

            var title = $"{startDate:MMM dd, yyyy} → {endDate:MMM dd, yyyy}";

            var existingPage = await _client.GetPage(_databaseId, title, "Name");

            if (existingPage != null)
            {
                return existingPage.Id;
            }

            var newPage = new NewPage
            {
                Parent = new DatabaseParent
                {
                    DatabaseId = _databaseId
                },
                Properties = new Dictionary<string, PropertyValue>
                {
                    {
                        "Week", new DatePropertyValue
                        {
                            Date = new Date
                            {
                                Start = startDate.ToString("yyyy-MM-dd"),
                                End = endDate.ToString("yyyy-MM-dd")
                            }
                        }
                    },
                    {
                        "Name", new TitlePropertyValue
                        {
                            Title = new List<RichTextBase>
                            {
                                new RichTextText
                                {
                                    Text = new Text
                                    {
                                        Content = title
                                    }
                                }
                            }
                        }
                    }
                }
            };

            var page = await _client.NotionClient.Pages.CreateAsync(newPage);
            return page.Id;
        }
    }
}
