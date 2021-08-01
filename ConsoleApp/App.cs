using System;
using System.Threading.Tasks;
using NotionWeeklyPlanner;

namespace ConsoleApp.ConsoleApp
{
    public class App
    {
        private readonly WeeklyPageCreator _weeklyPageCreator;
        private readonly JournalPagesCreator _journalPagesCreator;
        private readonly PageLinker _pageLinker;

        public App(WeeklyPageCreator weeklyPageCreator, JournalPagesCreator journalPagesCreator, PageLinker pageLinker)
        {
            _weeklyPageCreator = weeklyPageCreator;
            _journalPagesCreator = journalPagesCreator;
            _pageLinker = pageLinker;
        }

        public async Task Run(DateTime startDate)
        {
            var weeklyPageId = await _weeklyPageCreator.CreateWeeklyPage(startDate);
            var journalPageIds = await _journalPagesCreator.CreateJournalPages(startDate);

            await _pageLinker.LinkPages(weeklyPageId, journalPageIds);

        }
    }
}
