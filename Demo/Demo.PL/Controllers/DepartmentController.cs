
using AutoMapper;
using Demo.BL.Interface;
using Demo.BL.Models;
using Demo.BL.Repository;
using Demo.DAL.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Demo.PL.Controllers
{
    [Authorize(Roles ="Admin , Customer")]
    public class DepartmentController : Controller
    {
        // Dependency Injection
        private readonly IDepartmentRep dept;
        private readonly IMapper mapper;
        public DepartmentController(IDepartmentRep dept, IMapper mapper)
        {
            this.dept = dept;
            this.mapper = mapper;
        }

 
        public async Task<IActionResult> Index(string? SearchValue)
        {
            if (SearchValue == null)
            {
                var data = await dept.GetAsync();
                var result = mapper.Map<IEnumerable<DepartmentVM>>(data);
                return View(result);
            }
            else
            {
                var data = await dept.GetAsync();
                var result = mapper.Map<IEnumerable<DepartmentVM>>(data.Where(e => e.Name.Contains(SearchValue)));
                return View(result);
            }
            
        }

        public async Task<IActionResult> Details(int id)
        {
            var data = await dept.GetByIdAsync(id);
            var res = mapper.Map<DepartmentVM>(data);
            return View(res);
        }

        public IActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(DepartmentVM obj)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var res = mapper.Map<Department>(obj);
                    await dept.CreateAsync(res);
                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    ViewData["Msg"] = e.Message;
                    return View(obj);
                }
            }
            else
            {
                return View(obj);
            }
            
            
        }



        public async Task<IActionResult> Update(int id)
        {
            var data = await dept.GetByIdAsync(id);
            var res = mapper.Map<DepartmentVM>(data);
            return View(res);
        }

        [HttpPost]
        public async Task<IActionResult> Update(DepartmentVM obj)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var res = mapper.Map<Department>(obj);
                    await dept.UpdateAsync(res);
                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    ViewData["Msg"] = e.Message;
                    return View(obj);
                }
            }
            else
            {
                return View(obj);
            }
        }


        public async Task<IActionResult> Delete(int id)
        {
            var data = await dept.GetByIdAsync(id);
            var res = mapper.Map<DepartmentVM>(data);
            return View(res);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> Delete(DepartmentVM obj)
        {
            try
                {
                    var res = mapper.Map<Department>(obj);
                    await dept.RemoveAsync(res.Id);
                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    ViewData["Msg"] = e.Message;
                    return View(obj);
                }
        }
    }
}
