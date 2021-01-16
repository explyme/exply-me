using ExplyMe.Modules.Core.Areas.Account.ViewModels;
using ExplyMe.Modules.Core.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ExplyMe.Modules.Core.Areas.Account.Controllers
{
    [Area("account")]
    [Route("account")]
    public class AccountController : Controller
    {
        public UserManager<User> UserManager { get; }
        public SignInManager<User> SignInManager { get; }

        public AccountController(
            UserManager<User> userManager,
            SignInManager<User> signInManager)
        {
            UserManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            SignInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            //TODO: Verificar modelstate

            var user = await UserManager.FindByEmailAsync(model.Email);

            if (await UserManager.CheckPasswordAsync(user, model.Password) == false)
            {
                // TODO: Retornar erro
                ModelState.AddModelError("message", "Usuário ou senha inválidos");
                return View();
            }

            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, true, true);

            if (result.Succeeded)
            {
                //TODO: adicionar claims await UserManager.AddClaimAsync(user, new Claim("UserRole", "Admin"));
                return Redirect("/");
            }
            else if (result.IsLockedOut)
            {
                ModelState.AddModelError("message", "Sua conta está bloqueada temporariamente");
                return View();
            }
            else
            {
                ModelState.AddModelError("message", "Erro ao realizar login");
                return Ok(model);
            }
        }
    }
}
