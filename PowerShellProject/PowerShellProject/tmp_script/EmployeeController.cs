using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.EntityFramework;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using lssWebApi2.AddressBookDomain;
using ERP_Core2.AddressBookDomain;

namespace lssWebApi2.Controllers
{
    [Route("api/[controller]")]
    public class EmployeeController : Controller
    {

[HttpPost]
        [Route("View")]
        [ProducesResponseType(typeof(EmployeeView), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddEmployee([FromBody]EmployeeView view)
        {
            EmployeeModule invMod = new EmployeeModule();

            NextNumber nnEmployee = await invMod.Employee.Query().GetNextNumber();

            view.EmployeeNumber = nnEmployee.NextNumberValue;

            Employee employee = await invMod.Employee.Query().MapToEntity(view);

            invMod.Employee.AddEmployee(employee).Apply();

            EmployeeView newView = await invMod.Employee.Query().GetViewByNumber(view.EmployeeNumber);


            return Ok(newView);

        }

        [HttpDelete]
        [Route("View")]
        [ProducesResponseType(typeof(EmployeeView), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteEmployee([FromBody]EmployeeView view)
        {
            EmployeeModule invMod = new EmployeeModule();
            Employee employee = await invMod.Employee.Query().MapToEntity(view);
            invMod.Employee.DeleteEmployee(employee).Apply();

            return Ok(view);
        }

        [HttpPut]
        [Route("View")]
        [ProducesResponseType(typeof(EmployeeView), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateEmployee([FromBody]EmployeeView view)
        {
            EmployeeModule invMod = new EmployeeModule();

            Employee employee = await invMod.Employee.Query().MapToEntity(view);


            invMod.Employee.UpdateEmployee(employee).Apply();

            EmployeeView retView = await invMod.Employee.Query().GetViewById(employee.EmployeeId);


            return Ok(retView);

        }

        [HttpGet]
        [Route("View/{EmployeeId}")]
        [ProducesResponseType(typeof(EmployeeView), StatusCodes.Status200OK)]

        public async Task<IActionResult> GetEmployeeView(long employeeId)
        {
            EmployeeModule invMod = new EmployeeModule();

            EmployeeView view = await invMod.Employee.Query().GetViewById(employeeId);
            return Ok(view);
        }
        }
	}
        