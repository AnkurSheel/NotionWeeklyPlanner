using System;
using System.Threading.Tasks;
using NotionWeeklyPlanner;

namespace ConsoleApp.ConsoleApp
{
    public class App
    {
        private readonly WeeklyPageCreator _weeklyPageCreator;

        public App(WeeklyPageCreator weeklyPageCreator)
        {
            _weeklyPageCreator = weeklyPageCreator;
        }

        public async Task Run()
        {
            var weeklyPageId = await _weeklyPageCreator.CreateWeeklyPage(startDate);
        }
    }
}
