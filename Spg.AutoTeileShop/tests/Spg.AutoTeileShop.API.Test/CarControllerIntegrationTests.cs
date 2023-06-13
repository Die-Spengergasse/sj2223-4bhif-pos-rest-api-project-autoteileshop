//using System;
//using System.Collections.Generic;
//using System.Net;
//using System.Net.Http;
//using System.Text;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc.Testing;
//using Xunit;

//public class CarControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
//{
//    private readonly HttpClient _httpClient;

//    public CarControllerIntegrationTests(WebApplicationFactory<Startup> factory)
//    {
//        _httpClient = factory.CreateDefaultClient();
//    }

//    [Fact]
//    public async Task GetAllCars_ReturnsOkResponse()
//    {
//        // Arrange
//        var request = new HttpRequestMessage(HttpMethod.Get, "/api/v2/Car/Old");

//        // Act
//        var response = await _httpClient.SendAsync(request);

//        // Assert
//        response.EnsureSuccessStatusCode();
//        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
//    }

//    [Fact]
//    public async Task GetCarById_ReturnsOkResponse()
//    {
//        // Arrange
//        var request = new HttpRequestMessage(HttpMethod.Get, "/api/v2/Car/1");

//        // Act
//        var response = await _httpClient.SendAsync(request);

//        // Assert
//        response.EnsureSuccessStatusCode();
//        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
//    }

//    // Weitere Tests für die anderen Endpunkte des CarControllers...

//    [Fact]
//    public async Task AddCar_ReturnsCreatedResponse()
//    {
//        // Arrange
//        var carDto = new
//        {
//            // Daten für den zu erstellenden CarDTO
//        };

//        var request = new HttpRequestMessage(HttpMethod.Post, "/api/v2/Car");
//        request.Content = new StringContent(JsonConvert.SerializeObject(carDto), Encoding.UTF8, "application/json");

//        // Act
//        var response = await _httpClient.SendAsync(request);

//        // Assert
//        response.EnsureSuccessStatusCode();
//        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
//    }

//    [Fact]
//    public async Task UpdateCar_ReturnsOkResponse()
//    {
//        // Arrange
//        var carId = 1;
//        var carDto = new
//        {
//            // Aktualisierte Daten für den CarDTO
//        };

//        var request = new HttpRequestMessage(HttpMethod.Put, $"/api/v2/Car/{carId}");
//        request.Content = new StringContent(JsonConvert.SerializeObject(carDto), Encoding.UTF8, "application/json");

//        // Act
//        var response = await _httpClient.SendAsync(request);

//        // Assert
//        response.EnsureSuccessStatusCode();
//        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
//    }

//    [Fact]
//    public async Task DeleteCar_ReturnsOkResponse()
//    {
//        // Arrange
//        var carId = 1;
//        var request = new HttpRequestMessage(HttpMethod.Delete, $"/api/v2/Car/{carId}");

//        // Act
//        var response = await _httpClient.SendAsync(request);

//        // Assert
//        response.EnsureSuccessStatusCode();
//        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
//    }
//}

