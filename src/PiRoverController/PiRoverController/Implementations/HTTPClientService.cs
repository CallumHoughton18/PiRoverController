using PiRoverController.PresentationLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace PiRoverController.Implementations
{
    sealed class HTTPClientService : IHTTPClient
    {
        private readonly HttpClient _client = new HttpClient();
        public Task<HttpResponseMessage> GetAsync(Uri requestUri)
        {
            return _client.GetAsync(requestUri);
        }

        public bool HostAvailable(Uri hostUri)
        {
            bool pingable = false;
            Ping pinger = null;

            try
            {
                pinger = new Ping();
                PingReply reply = pinger.Send(hostUri.AbsolutePath);
                pingable = reply.Status == IPStatus.Success;
            }
            catch (PingException)
            {
                // Discard PingExceptions and return false;
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
