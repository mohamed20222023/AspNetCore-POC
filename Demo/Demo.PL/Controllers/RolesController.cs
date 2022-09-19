using Demo.BL.Models;
using Demo.DAL.Extend;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Demo.PL.Controllers
{
    [Authorize(Roles = "Admin")]
    public class RolesController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public RolesController(UserManager<ApplicationUser> userManager , RoleManager<IdentityRole> roleManager )
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }


        public IActionResult Index()
        {
            var roles = roleManager.Roles;
            return View(roles);
        }


        public async Task<IActionResult> Details(string id)
        {
            var role = await roleManager.FindByIdAsync(id);
            return View(role);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RoleVM model)
        {
            try
            {
                var role = new IdentityRole()
                {
                    Name = model.Name,
                };

                var result = await roleManager.CreateAsync(role);

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
                    return View(result);
                }
            }
            catch(Exception ex)
            {
                return View(model);
            }
            
        }



        public async Task<IActionResult> Update(string id)
        {
            var role = await roleManager.FindByIdAsync(id);
            return View(role);
        }

        [HttpPost]
        public async Task<IActionResult> Update(IdentityRole model)
        {
            try
            {

                var role = await roleManager.FindByIdAsync(model.Id);

                role.Name = model.Name;
                role.NormalizedName = model.Name.ToUpper();

                var result = await roleManager.UpdateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                    return View(result);
                }
            }
            catch (Exception ex)
            {
                return View(model);
            }

        }


        public async Task<IActionResult> Delete(string id)
        {
            var role = await roleManager.FindByIdAsync(id); 
            return View(role);
        }


        [HttpPost]
        public async Task<IActionResult> Delete(IdentityRole model)
        {
            var role = await roleManager.FindByIdAsync(model.Id);
            var result = await roleManager.DeleteAsync(role);
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
                return View(role);
            }
        }



        public async Task<IActionResult> AddOrRemoveUsers(string RoleId)
        {
            ViewBag.RoleId = RoleId;

            var role = await roleManager.FindByIdAsync(RoleId);

            var model = new List<UserInRoleVM>();

            foreach (var item in userManager.Users)
            {
                var userInRole = new UserInRoleVM()
                {
                    Id = item.Id,
                    Name = item.UserName
                };

                if(await userManager.IsInRoleAsync(item, role.Name))
                {
                    userInRole.IsSelected = true;
                }
                else
                {
                    userInRole.IsSelected = false;
                }

                model.Add(userInRole);
            }
            return View(model);

        }


        [HttpPost]
        public async Task<IActionResult> AddOrRemoveUsers(List<UserInRoleVM> model, string RoleId)
        {

            var role = await roleManager.FindByIdAsync(RoleId);

            for (int i = 0; i < model.Count; i++)
            {

                var user = await userManager.FindByIdAsync(model[i].Id);

                IdentityResult result = null;

                if (model[i].IsSelected && !(await userManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await userManager.AddToRoleAsync(user, role.Name);
                }
                else if (!model[i].IsSelected && (await userManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }
            }

            return RedirectToAction("Update", new { id = RoleId });
        }

    }
}
