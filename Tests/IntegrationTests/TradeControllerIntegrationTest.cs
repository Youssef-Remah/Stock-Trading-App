using Fizzler.Systems.HtmlAgilityPack;
using FluentAssertions;
using HtmlAgilityPack;

namespace Tests.IntegrationTests
{
    public class TradeControllerIntegrationTest : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _httpClient;


        public TradeControllerIntegrationTest(CustomWebApplicationFactory factory)
        {
            _httpClient = factory.CreateClient();
        }


        #region Index

        [Fact]
        public async Task Index_ToReturnView()
        {
            //Act
            HttpResponseMessage responseMessage = await _httpClient.GetAsync("/Trade/Index/MSFT");

            //Assert
            responseMessage.Should().BeSuccessful();

            string responseBody = await responseMessage.Content.ReadAsStringAsync();

            HtmlDocument htmlDocument = new();

            htmlDocument.LoadHtml(responseBody);

            HtmlNode documentNode = htmlDocument.DocumentNode;

            documentNode.QuerySelectorAll(".price").Should().NotBeNull();
        }

        #endregion
    }
}