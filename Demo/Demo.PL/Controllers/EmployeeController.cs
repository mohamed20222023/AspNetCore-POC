using AutoMapper;
using Demo.BL.Helper;
using Demo.BL.Interface;
using Demo.BL.Models;
using Demo.DAL.Entity;
using Demo.DAL.Migrations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Employee = Demo.DAL.Entity.Employee;

namespace Demo.PL.Controllers
{
    [Authorize(Roles = "Admin , Customer")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRep emp;
        private readonly IDepartmentRep dept;
        private readonly IMapper mapper;
        private readonly ICityRep city;
        private readonly IDistrictRep district;

        public EmployeeController(IEmployeeRep emp,IDepartmentRep dept, IMapper mapper , ICityRep city , IDistrictRep district)
        {
            this.emp = emp;
            this.dept = dept;
            this.mapper = mapper;
            this.city = city;
            this.district = district;
        }


        public async Task<IActionResult> Index(string? SearchValue)
        {
            if (SearchValue == null)
            {
                var data = await emp.GetAsync(e => e.IsActive == true && e.IsDeleted == false);
                var result = mapper.Map<IEnumerable<EmployeeVM>>(data);
                return View(result);
            }
            else
            {
                var data = await emp.GetAsync(e => e.IsActive == true && e.IsDeleted == false);
                var result = mapper.Map<IEnumerable<EmployeeVM>>(data.Where(e => e.Name.Contains(SearchValue) || e.Email.Contains(SearchValue)));
                return View(result);
            }
            
        }


        public async Task<IActionResult> Details(int id)
        {
            var data = await emp.GetByIdAsync(e => e.IsActive == true && e.Id == id && e.IsDeleted == false);
            var res = mapper.Map<EmployeeVM>(data);

            ViewBag.DepartmentList = res.Department.Name;
            return View(res);
        }


        public async Task<IActionResult> Create()
        {
            var data = await dept.GetAsync();
            ViewBag.DepartmentList = new SelectList(data , "Id" , "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeVM obj)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    obj.CvName = UploderFiles.GetFileName( obj.CV ,"Docs");
                    obj.ImageName = UploderFiles.GetFileName(obj.Image ,"Images");

                    var res = mapper.Map<Employee>(obj);
                    await emp.CreateAsync(res);
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

            var data = await emp.GetByIdAsync(x => x.IsActive == true && x.IsDeleted == false && x.Id == id);
            
            var result = mapper.Map<EmployeeVM>(data);
            var DepartmentData = mapper.Map<IEnumerable<DepartmentVM>>(await dept.GetAsync());
            ViewBag.DepartmentList = new SelectList(DepartmentData, "Id", "Name", result.DepartmentId);
            return View(result);

            
        }


        [HttpPost]
        public async Task<IActionResult> Update(EmployeeVM obj)
        {
            var Deptdata = mapper.Map<IEnumerable<DepartmentVM>>(await dept.GetAsync());
            ViewBag.DepartmentList = new SelectList(Deptdata, "Id", "Name", obj.Id);


            try
            {
                if (ModelState.IsValid)
                {
                    obj.CvName = UploderFiles.GetFileName(obj.CV, "Docs");
                    obj.ImageName = UploderFiles.GetFileName(obj.Image, "Images");


                    var data = mapper.Map<Employee>(obj);

                    await emp.UpdateAsync(data);
                    return RedirectToAction("Index");
                }

                TempData["Msg"] = "Validation Error";
                return View(obj);
            }
            catch (Exception ex)
            {
                TempData["Msg"] = ex.Message;
                return View(obj);
            }


        }


        public async Task<IActionResult> Delete(int id)
        {
            var data = await emp.GetByIdAsync(e => e.IsActive == true && e.IsDeleted == false && e.Id == id);
            var res = mapper.Map<EmployeeVM>(data);

            ViewBag.DepartmentList = res.Department.Name;
            return View(res);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> Delete(EmployeeVM obj)
        {
            try
            {
                var res = mapper.Map<Employee>(obj);
                await emp.RemoveAsync(res.Id);

                UploderFiles.RemoveFile("Docs",obj.CvName);
                UploderFiles.RemoveFile("Images", obj.ImageName);

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ViewData["Msg"] = e.Message;
                return View(obj);
            }
        }


        [HttpPost]
        public async Task<JsonResult> GetCityDataByCountryId(int CntryId)
        {
            var data = await city.GetAsync(a => a.CountryId == CntryId);
            var result = mapper.Map<IEnumerable<CityVM>>(data);
            return Json(result);
        }


        [HttpPost]
        public async Task<JsonResult> GetDistrictDataByCityId(int CtyId)
        {
            var data = await district.GetAsync(a => a.CityId == CtyId);
            var result = mapper.Map<IEnumerable<DistrictVM>>(data);
            return Json(result);
        }

    }
}
