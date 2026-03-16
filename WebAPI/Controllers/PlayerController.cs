using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

public class PlayerController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}