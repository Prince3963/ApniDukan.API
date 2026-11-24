using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using ApniDukan.Application.ServiceResponse;
using ApniDukan.Domain.DTOs.Auth;
using ApniDukan.Domain.ENUMs;
using ApniDukan.Domain.Models;
using ApniDukan.Infrastrcture.Data;
using ApniDukan.Tests.Factories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace ApniDukan.Tests.IntegrationTests;

public class AuthControllerTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory _factory;

    public AuthControllerTests(CustomWebApplicationFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient();

        using var scope = _factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        db.Database.EnsureCreated();
    }

    private static Dictionary<string, object?> ValidRequest(string? email = null)
    {
        return new Dictionary<string, object?>
        {
            ["Name"] = "Prince Patel",
            ["Email"] = email ?? $"prince{Guid.NewGuid():N}@test.com",
            ["Password"] = "Prince@12345",
            ["PhoneNumber"] = "9876543210",
            ["AlternativeNumber"] = null,
            ["PermanentAddress"] = "Surat",
            ["CurrentAddress"] = "Surat",
            ["ShopName"] = "Prince Grocery",
            ["ShopDescription"] = "Fresh groceries delivered daily.",
            ["ShopImageUrl"] = "https://example.com/shop.jpg",
            ["ShopAddress"] = "Ring Road, Surat, Gujarat"
        };
    }

    private async Task<HttpResponseMessage> Register(Dictionary<string, object?> request)
    {
        return await _client.PostAsJsonAsync("/api/Auth/register/shop-owner", request);
    }

    // =========================================================================
    // 1. POSITIVE / HAPPY PATH TESTS
    // =========================================================================

    [Fact]
    public async Task RegisterShopOwner_WithAllValidData_ReturnsOkWithResponseData()
    {
        // Arrange
        var request = ValidRequest();

        // Act
        var response = await Register(request);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var result = await response.Content.ReadFromJsonAsync<ServiceResponse<ShopOwnerRegisterResponse>>();
        Assert.NotNull(result);
        Assert.True(result.Success);
        Assert.Equal("Shop owner registered successfully.", result.Message);
        Assert.NotNull(result.Data);
        Assert.True(result.Data.UserId > 0);
        Assert.True(result.Data.ShopId > 0);
        Assert.Equal(request["Name"]!.ToString(), result.Data.OwnerName);
        Assert.Equal(request["ShopName"]!.ToString(), result.Data.ShopName);
    }

    [Fact]
    public async Task RegisterShopOwner_WithOptionalFieldsNull_ReturnsOk()
    {
        // Arrange
        var request = ValidRequest();
        request["AlternativeNumber"] = null;
        request["PermanentAddress"] = null;
        request["ShopImageUrl"] = null;

        // Act
        var response = await Register(request);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Theory]
    [InlineData("9876543210")]
    [InlineData("+919876543210")]
    [InlineData("+91 9876543210")]
    public async Task RegisterShopOwner_WithValidIndianPhoneNumberFormats_ReturnsOk(string phoneNumber)
    {
        // Arrange
        var request = ValidRequest();
        request["PhoneNumber"] = phoneNumber;

        // Act
        var response = await Register(request);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    // =========================================================================
    // 2. VALIDATION TESTS (FLUENT VALIDATION RULES)
    // =========================================================================

    [Theory]
    [InlineData("Name", "")]                            // Owner name empty
    [InlineData("Email", "")]                           // Email empty
    [InlineData("Email", "not-an-email")]               // Invalid email format
    [InlineData("CurrentAddress", "")]                  // Current address empty
    [InlineData("ShopName", "")]                        // Shop name empty
    [InlineData("ShopDescription", "")]                 // Shop description empty
    [InlineData("ShopAddress", "")]                     // Shop address empty
    [InlineData("PhoneNumber", "")]                     // Phone number empty
    [InlineData("PhoneNumber", "1234567890")]           // Does not start with 6-9
    [InlineData("PhoneNumber", "98765")]                // Too short
    [InlineData("PhoneNumber", "9876543210123")]        // Too long
    [InlineData("AlternativeNumber", "12345")]          // Invalid alternative number format
    [InlineData("ShopImageUrl", "ftp://example.com/p")] // Not HTTP/HTTPS scheme
    [InlineData("ShopImageUrl", "not_a_valid_url")]     // Malformed URL
    public async Task RegisterShopOwner_WithInvalidField_ReturnsBadRequest(string field, string value)
    {
        // Arrange
        var request = ValidRequest();
        request[field] = value;

        // Act
        var response = await Register(request);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Theory]
    [InlineData("short")]            // < 8 characters
    [InlineData("alllowercase123")]  // Missing uppercase
    [InlineData("ALLUPPERCASE123")]  // Missing lowercase
    [InlineData("NoNumberPassword")] // Missing number
    public async Task RegisterShopOwner_WithWeakPassword_ReturnsBadRequest(string weakPassword)
    {
        // Arrange
        var request = ValidRequest();
        request["Password"] = weakPassword;

        // Act
        var response = await Register(request);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task RegisterShopOwner_WithDescriptionExceeding500Chars_ReturnsBadRequest()
    {
        // Arrange
        var request = ValidRequest();
        request["ShopDescription"] = new string('A', 501);

        // Act
        var response = await Register(request);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task RegisterShopOwner_WithCurrentAddressExceeding250Chars_ReturnsBadRequest()
    {
        // Arrange
        var request = ValidRequest();
        request["CurrentAddress"] = new string('A', 251);

        // Act
        var response = await Register(request);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task RegisterShopOwner_WithPermanentAddressExceeding250Chars_ReturnsBadRequest()
    {
        // Arrange
        var request = ValidRequest();
        request["PermanentAddress"] = new string('A', 251);

        // Act
        var response = await Register(request);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task RegisterShopOwner_WithShopAddressExceeding250Chars_ReturnsBadRequest()
    {
        // Arrange
        var request = ValidRequest();
        request["ShopAddress"] = new string('A', 251);

        // Act
        var response = await Register(request);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task RegisterShopOwner_WithPasswordExceeding100Chars_ReturnsBadRequest()
    {
        // Arrange
        var request = ValidRequest();
        request["Password"] = "P1" + new string('a', 99); // 101 characters

        // Act
        var response = await Register(request);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    [Fact]
    public async Task RegisterShopOwner_WithValidAlternativeNumber_ReturnsOk()
    {
        // Arrange
        var request = ValidRequest();
        request["AlternativeNumber"] = "9123456789";

        // Act
        var response = await Register(request);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task RegisterShopOwner_WithEmptyPayload_ReturnsBadRequest()
    {
        // Arrange
        var emptyRequest = new Dictionary<string, object?>();

        // Act
        var response = await Register(emptyRequest);

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }

    // =========================================================================
    // 3. BUSINESS LOGIC & CONFLICT TESTS
    // =========================================================================

    [Fact]
    public async Task RegisterShopOwner_WithDuplicateEmail_ReturnsBadRequest()
    {
        // Arrange
        var email = $"duplicate{Guid.NewGuid():N}@test.com";
        var firstRequest = ValidRequest(email);
        var secondRequest = ValidRequest(email);

        // Act
        var firstResponse = await Register(firstRequest);
        var secondResponse = await Register(secondRequest);

        // Assert
        Assert.Equal(HttpStatusCode.OK, firstResponse.StatusCode);
        Assert.Equal(HttpStatusCode.BadRequest, secondResponse.StatusCode);

        var result = await secondResponse.Content.ReadFromJsonAsync<ServiceResponse<ShopOwnerRegisterResponse>>();
        Assert.NotNull(result);
        Assert.False(result.Success);
        Assert.Contains("already registered", result.Message, StringComparison.OrdinalIgnoreCase);
    }

    // =========================================================================
    // 4. DATABASE & ENTITY INTEGRITY TESTS
    // =========================================================================

    [Fact]
    public async Task RegisterShopOwner_CreatesUserAndShopWithCorrectDefaults()
    {
        // Arrange
        var request = ValidRequest();
        var email = request["Email"]!.ToString()!;

        // Act
        var response = await Register(request);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        using var scope = _factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        var user = await db.Set<User>().FirstOrDefaultAsync(x => x.Email == email);
        Assert.NotNull(user);
        Assert.Equal(UserRole.ShopOwner, user.Role);
        Assert.True(user.IsActive);

        var shop = await db.Set<Shop>().FirstOrDefaultAsync(x => x.ShopOwnerId == user.Id);
        Assert.NotNull(shop);
        Assert.Equal(request["ShopName"]!.ToString(), shop.Name);
        Assert.Equal(request["ShopAddress"]!.ToString(), shop.Address);
        Assert.True(shop.IsOpen);
        Assert.True(shop.IsActive);
    }

    [Fact]
    public async Task RegisterShopOwner_ShouldProperlyHashPassword()
    {
        // Arrange
        var request = ValidRequest();
        var email = request["Email"]!.ToString()!;
        var rawPassword = request["Password"]!.ToString()!;

        // Act
        var response = await Register(request);

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        using var scope = _factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        var user = await db.Set<User>().FirstOrDefaultAsync(x => x.Email == email);
        Assert.NotNull(user);
        Assert.NotEqual(rawPassword, user.PasswordHash);

        var hasher = new PasswordHasher<User>();
        var verifyResult = hasher.VerifyHashedPassword(user, user.PasswordHash, rawPassword);
        Assert.Equal(PasswordVerificationResult.Success, verifyResult);
    }
}
