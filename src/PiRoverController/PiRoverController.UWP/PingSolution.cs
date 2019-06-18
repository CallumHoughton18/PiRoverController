using PiRoverController.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking.Sockets;

namespace PiRoverController.UWP
{
    public class PingSolution : IPingConnection
    {
        public async Task<bool> ConnectToServer(Uri serverUri)
        {
            try
            {
                using (var tcpClient = new StreamSocket())
                {
                    await tcpClient.ConnectAsync(
                        new Windows.Networking.HostName(serverUri.Host),
                        serverUri.Port.ToString(),
                        SocketProtectionLevel.PlainSocket);

                    var localIp = tcpClient.Information.LocalAddress.DisplayName;
                    var remoteIp = tcpClient.Information.RemoteAddress.DisplayName;

                    tcpClient.Dispose();

                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
