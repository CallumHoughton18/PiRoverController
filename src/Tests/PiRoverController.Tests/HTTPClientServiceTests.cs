
using NUnit.Framework;
using PiRoverController.Implementations;

namespace PiRoverController.Tests
{
    public class HTTPClientServiceTests
    {
        [Test]
        public void HostAvailable_Valid()
        {
            var sut = new HTTPClientService();

            var available = sut.HostAvailable("google.com");

            Assert.That(available, Is.True);
        }

        [Test]
        public void HostAvailable_NotValidHost()
        {
            var sut = new HTTPClientService();

            var available = sut.HostAvailable("ewojfwjdwqj13133ddww.com");

            Assert.That(available, Is.False);
        }
    }
}
