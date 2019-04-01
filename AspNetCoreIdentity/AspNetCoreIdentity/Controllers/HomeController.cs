using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AspNetCoreIdentity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace AspNetCoreIdentity.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<CustomUser> userManager;
        private readonly IUserClaimsPrincipalFactory<CustomUser> claimsPrincipalFactory;
        private readonly SignInManager<CustomUser> signInManager;

        public HomeController(UserManager<CustomUser> userManager,
            IUserClaimsPrincipalFactory<CustomUser> claimsPrincipalFactory,
            SignInManager<CustomUser> signInManager)
        {
            this.userManager = userManager;
            this.claimsPrincipalFactory = claimsPrincipalFactory;
            this.signInManager = signInManager;
        }

        //private readonly UserManager<IdentityUser> userManager;
        //public HomeController(UserManager<IdentityUser> userManager)
        //{
        //    this.userManager = userManager;
        //}

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

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

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(model.UserName);

                if (user == null)
                {
                    user = new CustomUser
                    {
                        Id = Guid.NewGuid().ToString(),
                        UserName = model.UserName
                    };

                    var result = await userManager.CreateAsync(user, model.Password);
                }

                return View("Success");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                //**********************************************************************************************
                //var user = await userManager.FindByNameAsync(model.UserName);

                //if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
                //{
                //    //Identity with core funcionality 
                //    //***************************************************************
                //    //var identity = new ClaimsIdentity("cookies");
                //    //identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id));
                //    //identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
                //    //await HttpContext.SignInAsync("cookies", new ClaimsPrincipal(identity));
                //    //***************************************************************

                //    //Identity with full funcionality 
                //    //***************************************************************
                //    var identity = await claimsPrincipalFactory.CreateAsync(user);
                //    await HttpContext.SignInAsync("Identity.Application", new ClaimsPrincipal(identity));
                //    //***************************************************************
                //    return RedirectToAction("Index");
                //}
                //**********************************************************************************************


                var signInResult = await signInManager.PasswordSignInAsync(model.UserName, model.Password,
                    false, false);

                if (signInResult.Succeeded)
                {
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError("", "Invalid UserName or Password");
            }

            return View();
        }

    }
}
