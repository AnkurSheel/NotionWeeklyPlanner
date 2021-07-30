using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Notion.Client;

namespace NotionWeeklyPlanner
{
    public class JournalPagesCreator
    {
        private readonly string _databaseId;
        private readonly Client _client;

        public JournalPagesCreator(NotionSettings notionSettings, Client client)
        {
            _client = client;
            _databaseId = notionSettings.JournalDatabaseId ?? throw new ArgumentNullException(nameof(notionSettings));
        }

        public async Task<List<string>> CreateJournalPages(DateTime startDate)
        {
            var createdPageIds = new List<string>();
            var endDate = startDate.AddDays(6);

            var pages = await _client.GetPages(_databaseId, "Date");

            for (var i = 0; i <= 6; i++)
            {
                var date = startDate.AddDays(i);
                var title = $"{date:MMM dd, yyyy}";

                var existingPage = _client.GetPage(pages.Results, title, "Name");

                if (existingPage != null)
                {
                    createdPageIds.Add(existingPage.Id);
                    continue;
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
                            "Date", new DatePropertyValue
                            {
                                Date = new Date
                                {
                                    Start = date.ToString("yyyy-MM-dd"),
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
                createdPageIds.Add(page.Id);
            }

            return createdPageIds;
        }
    }
}
