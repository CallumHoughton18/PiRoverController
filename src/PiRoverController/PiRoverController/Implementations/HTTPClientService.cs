using PiRoverController.Interfaces;
using PiRoverController.PresentationLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PiRoverController.Implementations
{
    public class HTTPClientService : IHTTPClient
    {
        IServerConnection _platformPingConnection;
        public HTTPClientService(IServerConnection pingConnection)
        {
            _platformPingConnection = pingConnection;
        }
        private readonly HttpClient _client = new HttpClient();
        public Task<HttpResponseMessage> GetAsync(Uri requestUri)
        {
            return _client.GetAsync(requestUri);
        }

        public async Task<bool> HostAvailable(Uri baseUri)
        {
            var result = await _platformPingConnection.ConnectToServer(baseUri);
            return result;
        }

    }
}
