using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A35Mge.IDS.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            ViewBag.ReturnUrl = Request.Query["ReturnUrl"];
            return View();
        }
    }
}
