using AutoMapper;
using Demo.BL.Helper;
using Demo.BL.Interface;
using Demo.BL.Models;
using Demo.DAL.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Demo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRep emp;
        private readonly IMapper mapper;

        public EmployeeController(IEmployeeRep emp , IMapper mapper)
        {
            this.emp = emp;
            this.mapper = mapper;
        }


        [HttpGet]
        [Route("~/api/Employee/GetEmployee")]
        public async Task<IActionResult> GetEmployee()
        {
            try
            {
                var data = await emp.GetAsync(e => e.IsActive == true && e.IsDeleted == false);
                var result = mapper.Map<IEnumerable<EmployeeVM>>(data);
                return Ok(new ApiResponse<IEnumerable<EmployeeVM>>
                {
                    Code = "200",
                    Status = "Ok",
                    Message = "Data Found",
                    Data = result  
                });
            }
            catch(Exception ex)
            {
                return NotFound(new ApiResponse<string>
                {
                    Code = "404",
                    Status = "Not Found",
                    Message = "No Data Found",
                    Data = ex.Message
                });
            }

        }


        [HttpGet]
        [Route("~/api/Employee/GetEmployeeById/{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            try
            {
                var data = await emp.GetByIdAsync(e => e.IsActive == true && e.IsDeleted == false && e.Id == id);
                var result = mapper.Map<EmployeeVM>(data);
                return Ok(new ApiResponse<EmployeeVM>
                {
                    Code = "200",
                    Status = "Ok",
                    Message = "Data Found",
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return NotFound(new ApiResponse<string>
                {
                    Code = "404",
                    Status = "Not Found",
                    Message = "No Data Found",
                    Data = ex.Message
                });
            }

        }


        [HttpPost]
        [Route("~/api/Employee/PostEmployee")]
        public async Task<IActionResult> PostEmployee(EmployeeVM obj)
        {
                try
                {

                    var res = mapper.Map<Employee>(obj);
                    await emp.CreateAsync(res);
                    return Ok(new ApiResponse<Employee>
                    {
                        Code = "200",
                        Status = "Ok",
                        Message = "Data Found",
                        Data = res
                    });
            }
                catch (Exception ex)
                {
                    return NotFound(new ApiResponse<string>
                    {
                        Code = "404",
                        Status = "Not Found",
                        Message = "No Data Found",
                        Data = ex.Message
                    });
            }

        }


        [HttpPut]
        [Route("~/api/Employee/PutEmployee")]
        public async Task<IActionResult> PutEmployee(EmployeeVM obj)
        {
            try
            {

                var res = mapper.Map<Employee>(obj);
                await emp.UpdateAsync(res);
                return Ok(new ApiResponse<string>
                {
                    Code = "200",
                    Status = "Update",
                    Message = "Data Updated",
                    Data = "Updated Successfull"
                });
            }
            catch (Exception ex)
            {
                return NotFound(new ApiResponse<string>
                {
                    Code = "402",
                    Status = "Not Updated",
                    Message = "No Data Updated",
                    Data = ex.Message
                });
            }

        }



        [HttpDelete]
        [Route("~/api/Employee/DeleteEmployee/{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            try
            {
                await emp.RemoveAsync(id);
                return Ok(new ApiResponse<string>
                {
                    Code = "200",
                    Status = "Deleted",
                    Message = "Data Deleted",
                    Data = "Deleted Successfull"
                });
            }
            catch (Exception ex)
            {
                return NotFound(new ApiResponse<string>
                {
                    Code = "404",
                    Status = "Not Deleted",
                    Message = "No Data Deleted",
                    Data = ex.Message
                });
            }

        }


    }
}
