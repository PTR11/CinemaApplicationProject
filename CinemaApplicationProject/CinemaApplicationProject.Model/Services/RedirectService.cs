using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CinemaApplicationProject.Model.Services
{
    public class RedirectService
    {
        public static HttpResponseMessage RedirectMethod(String message, HttpStatusCode code, Uri location = null)
        {
            var response = new HttpResponseMessage();
            response.StatusCode = code;
            response.Headers.Location = location;
            response.Headers.Add("Message",message);
            return response;
        }
    }
}
