using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaeedPay76.Common.Helper
{
    public static class Extensions
    {
        public static void AddAppError(this HttpResponse response, string message)
        {
            response.Headers.Add("App-Error", message);
            response.Headers.Add("Acces-Control-Expose-Headers", "App-Error");
            response.Headers.Add("Acces-Control-Allow-Origin", "*");
        }
    }
}
