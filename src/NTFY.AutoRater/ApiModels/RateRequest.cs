using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NTFY.AutoRater.ApiModels
{
    public class RateRequest
    {
        public string data { get; set; }

        public RateRequest(IEnumerable<int> bagItemIds)
        {
            var random = new Random(Guid.NewGuid().GetHashCode());
            var dataContent = bagItemIds.Select(bagItemId => new
            {
                bagItem = bagItemId,
                comment = "",
                rate = random.Next(1, 4)
            }); 

            data = JsonSerializer.Serialize(dataContent);
        }
    }
}
