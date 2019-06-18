
using NUnit.Framework;
using PiRoverController.Implementations;
using System;
using System.Threading.Tasks;

namespace PiRoverController.Tests
{
    public class ServerConnectionTests
    {
        [Test]
        public async Task HostAvailable_Valid()
        {
            var sut = new ServerConnection();

            var available = await sut.ConnectToServer(new Uri("https://google.com"));

            Assert.That(available, Is.True);
        }

        [Test]
        public async Task HostAvailable_NotValidHost()
        {
            var sut = new ServerConnection();

            var available = await sut.ConnectToServer(new Uri("https://ejfiewjfjdiwqjeq3rj4wfjewufjewfuew.com"));

            Assert.That(available, Is.False);
        }
    }
}
