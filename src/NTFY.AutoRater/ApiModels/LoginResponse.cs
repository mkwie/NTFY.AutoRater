using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTFY.AutoRater.ApiModels
{
    public class LoginResponse
    {
        public string token { get; set; }
        public string refresh_token { get; set; }
    }
}
