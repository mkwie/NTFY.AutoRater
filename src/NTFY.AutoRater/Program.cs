using NTFY.AutoRater.ApiModels;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace NTFY.AutoRater
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var client = new NTFYHttpClient(new HttpClient(new LoggingHandler(new HttpClientHandler())));

            var password = args[0];
            var login = args[1];

            await client.Login(new LoginRequest()
            {
                _password = password,
                _username = login
            });

            var dietHistory = await client.FetchHistory();

            var timeZone = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time");
            var utcInTimeZone = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZone);
            var startOfCurrentMonth = new DateTime(utcInTimeZone.Year, utcInTimeZone.Month, 1);
            foreach (var diet in dietHistory.items)
            {
                var calendar = await client.GetCalendar(startOfCurrentMonth, diet.id);

                var toRate = calendar.days.Where(e => e.bag != null && e.isRated == false)
                    .Where(e => e.canRateFrom < utcInTimeZone && e.canRateTo > utcInTimeZone)
                    .ToList();

                foreach (var bagId in toRate.Select(f => f.bag.Value))
                {
                    var details = await client.GetDetails(bagId);

                    var rateRequest = new RateRequest(details.mealTypes.Select(f => f.bagItemId));

                    await client.Rate(bagId, rateRequest);
                }
            }
        }
    }
}
