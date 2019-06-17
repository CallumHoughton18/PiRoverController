using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PiRoverController.PresentationLogic.Interfaces
{
    public interface IHTTPClient
    {
        Task<HttpResponseMessage> GetAsync(Uri requestUri);

        bool HostAvailable(string host);
    }
}
