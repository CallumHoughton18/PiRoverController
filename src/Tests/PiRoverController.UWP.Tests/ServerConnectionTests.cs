using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PiRoverController.UWP.Tests
{
    [TestClass]
    public class ServerConnectionTests
    {
        [TestMethod]
        public async Task ConnectToServer_Valid()
        {
            var sut = new ServerConnection();

            var hostAvailable = await sut.ConnectToServer(new Uri("https://google.com"));

            Assert.IsTrue(hostAvailable);
        }

        [TestMethod]
        public async Task ConnectToServer_Invalid()
        {
            var sut = new ServerConnection();

            var hostAvailable = await sut.ConnectToServer(new Uri("https://eifeihggreheuh43.com"));

            Assert.IsFalse(hostAvailable);
        }
    }
}
