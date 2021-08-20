using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTFY.AutoRater.ApiModels
{
    public class CalendarResponse
    {
        public DayItem[] days { get; set; }
    }

    public class DayItem
    {
        //    {
        //  "bag": null,
        //  "day": 2,
        //  "isDelivered": false,
        //  "isRated": false,
        //  "isMealsVisible": false,
        //  "isMenuPrepared": false,
        //  "isMenuChanged": false,
        //  "canChangeMenuTo": null,
        //  "canRateTo": null,
        //  "canRateFrom": null,
        //  "status": "ACTIVE"
        //},
        public int? bag { get; set; }

        public int day { get; set; }

        public bool isRated { get; set; }

        public DateTime? canRateTo { get; set; }

        public DateTime? canRateFrom { get; set; }
    }
}
