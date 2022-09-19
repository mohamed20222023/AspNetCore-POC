using Demo.DAL.Extend;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Demo.PL.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;

        public UsersController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }


        public IActionResult Index()
        {
            var data = userManager.Users;
            return View(data);
        }


        public async Task<IActionResult> Details(string Id)
        {
            var user = await userManager.FindByIdAsync(Id);
            return View(user);
        }


        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(ApplicationUser obj)
        {
            var user = await userManager.FindByIdAsync(obj.Id);
            var result = await userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(obj);
            }
        }


        [HttpGet]
        public async Task<IActionResult> Update(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ApplicationUser obj)
        {
            var user = await userManager.FindByIdAsync(obj.Id);

            user.UserName = obj.UserName;
            user.NormalizedUserName = obj.UserName.ToUpper();
            user.Email = obj.Email;
            user.NormalizedEmail = obj.Email.ToUpper();
            user.SecurityStamp = obj.SecurityStamp;

            var result = await userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("" , item.Description);
                }
                return View(obj);
            }

        }


    }



}
