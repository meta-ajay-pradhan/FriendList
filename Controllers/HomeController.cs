using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FriendList.Models;
using FriendList.Areas.Identity.Data;

namespace FriendList.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {

        var User = HttpContext?.User;
        if(User!.Identity!.IsAuthenticated && User.IsInRole("admin")) {
            return RedirectToAction("Index", "Admin");
        }else if (User!.Identity!.IsAuthenticated && User.IsInRole("user")) {
            return RedirectToAction("Index", "FriendList");
        }
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
