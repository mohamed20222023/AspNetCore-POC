using Demo.BL.Helper;
using Demo.BL.Models;
using Demo.DAL.Extend;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;


namespace Demo.PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signinManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signinManager)
        {
            this.userManager = userManager;
            this.signinManager = signinManager;
        }


        #region Registration ( Sign Up)

        [HttpGet]
        public IActionResult Registration()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Registration(RegistrationVM model)
        {
            try
            {
                var user = new ApplicationUser()
                {
                    UserName = model.Email,
                    Email = model.Email
                };

                var result = await userManager.CreateAsync(user , model.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("LogIn");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError( "" , item.Description );
                    }
                    return View(model);
                }
            }catch(Exception ex)
            {
                return View(model);
            }
        }



        #endregion



        #region LogIn ( Sign In)

        [HttpGet]
        public IActionResult LogIn()
        {
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> LogIn(LoginVM model)
        {

            try
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if(user != null)
                {
                    var result = await signinManager.PasswordSignInAsync(user, model.Password,model.RememberMe,false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Account inValid");
                    }
                    return View(model);
                }
                ModelState.AddModelError("", "Account inValid");
                return View(model);
                

            }catch(Exception ex)
            {
                return View(model);
            }

        }


        #endregion



        #region LogOut ( Sign Out)

        [HttpPost]
        public async Task<IActionResult> LogOff()
        {
            await signinManager.SignOutAsync();
            return RedirectToAction("LogIn");
        }


        #endregion



        #region ForgetPassword

        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordVM model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                var token = await userManager.GeneratePasswordResetTokenAsync(user);
                var passwordResetLink = Url.Action("ResetPassword", "Account", new { Email = model.Email, Token = token }, Request.Scheme);

                var email = new MailVM()
                {
                    ToMail = model.Email,
                    Body = passwordResetLink ,
                    Subject = "Reset Password"
                };

               MailSender.SendEmail(email);
                


                return RedirectToAction("ConfirmForgetPassword");

            }
            else
            {
                return RedirectToAction("Login");
            }
            
        }


        [HttpGet]
        public IActionResult ConfirmForgetPassword()
        {
            return View();
        }



        #endregion



        #region ResetPassword
        [HttpGet]
        public IActionResult ResetPassword(string Email, string Token)
        {
            return View();
        }

        [HttpPost] 
        public async Task<IActionResult> ResetPassword(ResetPasswordVM model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);

            if (user != null)
            {
                var result = await userManager.ResetPasswordAsync(user, model.Token, model.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("ConfirmResetPassword");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }

                    return View(model);
                }

            }
            else
            {
                ModelState.AddModelError("", "Invalid Email");
                return View(model); 
            }
        }


        [HttpGet]
        public IActionResult ConfirmResetPassword()
        {
            return View();
        }

        #endregion
    }
}
