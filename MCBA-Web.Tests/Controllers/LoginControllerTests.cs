using MCBA_Web.Controllers;
using MCBA_Web.Models;
using MCBA_Web.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NSubstitute;
using System;
using System.Net.Http;
using Xunit;


namespace MCBA_Web.Tests.Controllers;

public class LoginControllerTests
{
    private readonly LoginController _controller;
    private readonly ILoginService _mockLoginService;
    private readonly HttpContext _httpContext;

    public LoginControllerTests()
    {
        _mockLoginService = Substitute.For<ILoginService>();
        _controller = new LoginController(_mockLoginService);

        _httpContext = new DefaultHttpContext();
        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = _httpContext
        };
    }

    [Fact]
    public void Index_ReturnsView_WhenSessionIsNotSet()
    {
        // Arrange
        _httpContext.Session.SetInt32("CustomerID", 2100);
        
        // Act
        var result = _controller.Index();

        // Assert
        Assert.IsType<ViewResult>(result);
        Assert.Equal("Login", (result as ViewResult).ViewName);
    }

    [Fact]
    public void Index_RedirectsToCustomerIndex_WhenSessionIsSet()
    {
        // Arrange
        _httpContext.Session.SetInt32(nameof(Customer.CustomerID), 1);

        // Act
        var result = _controller.Index();

        // Assert
        var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal("Index", redirectToActionResult.ActionName);
        Assert.Equal("Customer", redirectToActionResult.ControllerName);
        Assert.Equal(1, redirectToActionResult.RouteValues["customerId"]);
    }

    [Fact]
    public void Login_ReturnsViewWithModelError_WhenLoginFails()
    {
        // Arrange
        _mockLoginService.AuthenticateCustomer(1, "password").Returns((Login)null);

        // Act
        var result = _controller.Login(1, "password");

        // Assert
        Assert.IsType<ViewResult>(result);
        Assert.Equal("Login", (result as ViewResult).ViewName);
        Assert.False(_controller.ModelState.IsValid);
        Assert.Equal("Login failed, please try again.", _controller.ModelState["LoginFailed"].Errors[0].ErrorMessage);
    }

    [Fact]
    public void Login_ReturnsViewWithModelError_WhenAccountIsLocked()
    {
        // Arrange
        _mockLoginService.AuthenticateCustomer(1, "password").Returns(new Login { LoginID = 1 });
        _mockLoginService.IsLocked(1).Returns(true);

        // Act
        var result = _controller.Login(1, "password");

        // Assert
        Assert.IsType<ViewResult>(result);
        Assert.Equal("Login", (result as ViewResult).ViewName);
        Assert.False(_controller.ModelState.IsValid);
        Assert.Equal("Account locked. Please contact us", _controller.ModelState["LoginFailed"].Errors[0].ErrorMessage);
    }

}