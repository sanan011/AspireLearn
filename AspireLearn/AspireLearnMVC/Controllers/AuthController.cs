using AspireLearnMVC.Models;
using AspireLearnMVC.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

public class AuthController : Controller
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(AspireLearnMVC.Models.LoginModel model)
    {
        // ✅ Convert AspireLearnMVC.Models.LoginModel to AspireLearnMVC.Services.LoginModel
        var loginRequest = new AspireLearnMVC.Services.LoginModel
        {
            Email = model.Email,
            Password = model.Password
        };

        var authResponse = await _authService.LoginAsync(loginRequest);
        if (authResponse == null || string.IsNullOrEmpty(authResponse.Token))
        {
            TempData["Error"] = "Invalid email or password.";
            return View();
        }

        HttpContext.Session.SetString("AuthToken", authResponse.Token);
        HttpContext.Session.SetString("UserRole", authResponse.Role);

        return RedirectToAction("Index", "Courses");
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(AspireLearnMVC.Models.RegisterModel model)
    {
        // ✅ Convert AspireLearnMVC.Models.RegisterModel to AspireLearnMVC.Services.RegisterModel
        var registerRequest = new AspireLearnMVC.Services.RegisterModel
        {
            FullName = model.FullName,
            Email = model.Email,
            Password = model.Password
        };

        bool success = await _authService.RegisterAsync(registerRequest);
        if (!success)
        {
            TempData["Error"] = "Registration failed.";
            return View();
        }

        TempData["Success"] = "Registration successful!";
        return RedirectToAction("Login");
    }

    public IActionResult Logout()
    {
        _authService.Logout();
        return RedirectToAction("Login");
    }
}
