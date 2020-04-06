using System;
using System.Net.Http;
using System.Threading.Tasks;
using IsolatedDevelopment.Tests.Setup;
using IsolatedDevelopment.Tests.Stubs;
using Xunit;

namespace IsolatedDevelopment.Tests
{
    public class InMemoryTests : IClassFixture<IsolatedDevelopmentWebApplicationFactory>, IDisposable
    {
        private readonly HttpClient _client;

        public InMemoryTests(IsolatedDevelopmentWebApplicationFactory factory) => _client = factory.CreateClient();

        /// <summary>
        /// Gets work
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Index_Get_ReturnsOk()
        {
            // Act
            var response = await _client.GetAsync("/");

            // Assert
            response.EnsureSuccessStatusCode();
        }

        /// <summary>
        /// Posts work too after ignoring anti forgery tokens in the application factory
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Index_Post_ReturnsStubMessage()
        {
            // Act
            var response = await _client.PostAsync("/", null);

            // Assert
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            Assert.Equal(new IntegratedDependencyStub().GetMessage(), result);
        }

        public void Dispose() => _client?.Dispose();
    }
}
