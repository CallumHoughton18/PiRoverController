using PiRoverController.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace PiRoverController.Implementations
{
    public class ServerConnection : IServerConnection
    {
        public async Task<bool> ConnectToServer(Uri serverUri)
        {
            //TODO: Apparently ping system is not compatible on UWP.
            bool pingable = false;
            Ping pinger = null;

            try
            {
                pinger = new Ping();
                var reply = await pinger.SendPingAsync(serverUri.Host);
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
