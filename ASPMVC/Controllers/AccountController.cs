using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPMVC.Data;
using ASPMVC.Models;
using ASPMVC.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ASPMVC.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;

        public AccountController(UserManager<IdentityUser> userManager,
                                SignInManager<IdentityUser> signInManager,
                                RoleManager<IdentityRole> roleManager,
                                ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _context = context;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {
            if (ModelState.IsValid)
            {
                var iuser = new IdentityUser{UserName = user.Username};
                var result = await _userManager.CreateAsync(iuser, user.Password);

                if (result.Succeeded)
                {
                    // Add newly registered account to user role if it exists
                    IdentityRole role = await _roleManager.FindByNameAsync("User");
                    if (role != null)
                    {
                        await _userManager.AddToRoleAsync(iuser, role.Name);
                    }

                    await _context.Users.AddAsync(user);
                    await _context.SaveChangesAsync();

                    await _signInManager.SignInAsync(iuser, isPersistent: false);
                    return RedirectToAction("index", "home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(user);
        }
        
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    if(!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                        return Redirect(returnUrl);

                    var loggedUser = await _userManager.FindByNameAsync(model.Username);

                    if(await _userManager.IsInRoleAsync(loggedUser, "Admin"))
                        return RedirectToAction("ListRoles", "Administration");


                    return RedirectToAction("index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Invalid login attempt!");
                
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("index", "home");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }


    }
}
