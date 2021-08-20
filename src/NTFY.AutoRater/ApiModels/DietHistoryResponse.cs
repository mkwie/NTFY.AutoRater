using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTFY.AutoRater.ApiModels
{
    public class DietHistoryResponse
    {
        public DietItem[] items { get; set; }
    }
    public class DietItem
    {
        public DietItemOrder order { get; set; }
        public int id { get; set; }
    }

    public class DietItemOrder
    {
        public int id { get; set; }
    }
}
