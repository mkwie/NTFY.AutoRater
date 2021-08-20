using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTFY.AutoRater.ApiModels
{
    public class BagDetailsResponse
    {
        public BagDetailsMealType[] mealTypes { get; set; }
    }

    public class BagDetailsMealType
    {
        public int bagItemId { get; set; }
    }
}
