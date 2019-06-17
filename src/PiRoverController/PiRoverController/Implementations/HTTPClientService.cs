using PiRoverController.PresentationLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace PiRoverController.Implementations
{
    public class HTTPClientService : IHTTPClient
    {
        private readonly HttpClient _client = new HttpClient();
        public Task<HttpResponseMessage> GetAsync(Uri requestUri)
        {
            return _client.GetAsync(requestUri);
        }

        public bool HostAvailable(string host)
        {
            //TODO: Apparently ping system is not compatible on UWP.
            bool pingable = false;
            Ping pinger = null;

            try
            {
                pinger = new Ping();
                PingReply reply = pinger.Send(host);
                pingable = reply.Status == IPStatus.Success;
            }
            catch (PingException e)
            {
                string ex = e.ToString();
            }
            finally
            {
                if (pinger != null)
                {
                    pinger.Dispose();
                }
            }

            return pingable;
        }
    }
}
