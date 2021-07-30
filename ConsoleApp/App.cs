using System;
using System.Threading.Tasks;
using NotionWeeklyPlanner;

namespace ConsoleApp.ConsoleApp
{
    public class App
    {
        private readonly WeeklyPageCreator _weeklyPageCreator;
        private readonly JournalPagesCreator _journalPagesCreator;

        public App(WeeklyPageCreator weeklyPageCreator, JournalPagesCreator journalPagesCreator)
        {
            _weeklyPageCreator = weeklyPageCreator;
            _journalPagesCreator = journalPagesCreator;
        }

        public async Task Run()
        {
            var startDate = new DateTime(2021, 07, 25);

            var weeklyPageId = await _weeklyPageCreator.CreateWeeklyPage(startDate);
            var journalPageIds = await _journalPagesCreator.CreateJournalPages(startDate);
        }
    }
}
