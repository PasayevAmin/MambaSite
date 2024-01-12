using Mamba.Models;
using Mamba.Utilities.Enums;
using Mamba.Utilities.Extentions;
using Mamba.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Packaging.Signing;

namespace Mamba.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManage;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager,SignInManager<AppUser> signInManage,RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManage = signInManage;
            _roleManager = roleManager;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid) return View();
            //if (!registerVM.CheckWords(registerVM.Name)||!registerVM.IsSymbol(registerVM.Name)||!registerVM.Name.IsDigit())
            //{
            //    ModelState.AddModelError("Name", "Name is Incorrect");
            //    return View();
            //}
            //if (!registerVM.CheckWords(registerVM.Surname) || !registerVM.IsSymbol(registerVM.Surname) || !registerVM.Name.IsDigit())
            //{
            //    ModelState.AddModelError("Surname", "Surname is Incorrect");
            //    return View();
            //}
            //if (!registerVM.CheckEmail(registerVM.Email))
            //{
            //    ModelState.AddModelError("Email", "Email is Incorrect");
            //    return View();
            //}
            AppUser appUser = new AppUser
            {
                Name = registerVM.Name.CapitalizeName(),
                Surname = registerVM.Surname.CapitalizeName(),
                Gender = registerVM.Gender,
                UserName = registerVM.UserName,
                Email = registerVM.Email,
                
            };
            IdentityResult result =await _userManager.CreateAsync(appUser,registerVM.Password);
            if (!result.Succeeded)
            {
                foreach (IdentityError item in result.Errors)
                {
                    ModelState.AddModelError(String.Empty,item.Description);
                }
                return View();
            }
            //await _userManager.AddToRoleAsync(appUser,UserRole.Member.ToString());
            await _signInManage.SignInAsync(appUser, false);
            return RedirectToAction("Index","Home");
        }
        public IActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LogIn(LoginVM loginVM,string? returnurl)
        {
            if (ModelState.IsValid) return View();
            AppUser appUser = await _userManager.FindByNameAsync(loginVM.UserNameOrEmail);
            if (appUser == null)
            {
                appUser = await _userManager.FindByEmailAsync(loginVM.UserNameOrEmail);
                if (appUser == null)
                {
                    ModelState.AddModelError(String.Empty, "Username,Email or Password is incorrect");
                    return View();
                }
            }
            var value = await _signInManage.PasswordSignInAsync(appUser, loginVM.Password, loginVM.IsRemembered, true);
            if (value.IsLockedOut)
            {
                ModelState.AddModelError(String.Empty,"you are bloced");
                return View();
            }
            if (!value.Succeeded)
            {
                ModelState.AddModelError(String.Empty, "Username,Email or Password is incorrect");

            }
            if (returnurl is null)
            {
                return RedirectToAction("Index", "Home");
            }
            return Redirect(returnurl);
        }
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");


        }
    }
}
