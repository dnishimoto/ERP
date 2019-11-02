using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.EntityFramework;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using lssWebApi2.PayRollDomain;
using ERP_Core2.PayRollDomain;

namespace lssWebApi2.Controllers
{
    [Route("api/[controller]")]
    public class PayRollTransactionsByEmployeeController : Controller
    {

[HttpPost]
        [Route("View")]
        [ProducesResponseType(typeof(PayRollTransactionsByEmployeeView), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddPayRollTransactionsByEmployee([FromBody]PayRollTransactionsByEmployeeView view)
        {
            PayRollTransactionsByEmployeeModule invMod = new PayRollTransactionsByEmployeeModule();

            NextNumber nnPayRollTransactionsByEmployee = await invMod.PayRollTransactionsByEmployee.Query().GetNextNumber();

            view.PayRollTransactionsByEmployeeNumber = nnPayRollTransactionsByEmployee.NextNumberValue;

            PayRollTransactionsByEmployee payRollTransactionsByEmployee = await invMod.PayRollTransactionsByEmployee.Query().MapToEntity(view);

            invMod.PayRollTransactionsByEmployee.AddPayRollTransactionsByEmployee(payRollTransactionsByEmployee).Apply();

            PayRollTransactionsByEmployeeView newView = await invMod.PayRollTransactionsByEmployee.Query().GetViewByNumber(view.PayRollTransactionsByEmployeeNumber);


            return Ok(newView);

        }

        [HttpDelete]
        [Route("View")]
        [ProducesResponseType(typeof(PayRollTransactionsByEmployeeView), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeletePayRollTransactionsByEmployee([FromBody]PayRollTransactionsByEmployeeView view)
        {
            PayRollTransactionsByEmployeeModule invMod = new PayRollTransactionsByEmployeeModule();
            PayRollTransactionsByEmployee payRollTransactionsByEmployee = await invMod.PayRollTransactionsByEmployee.Query().MapToEntity(view);
            invMod.PayRollTransactionsByEmployee.DeletePayRollTransactionsByEmployee(payRollTransactionsByEmployee).Apply();

            return Ok(view);
        }

        [HttpPut]
        [Route("View")]
        [ProducesResponseType(typeof(PayRollTransactionsByEmployeeView), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdatePayRollTransactionsByEmployee([FromBody]PayRollTransactionsByEmployeeView view)
        {
            PayRollTransactionsByEmployeeModule invMod = new PayRollTransactionsByEmployeeModule();

            PayRollTransactionsByEmployee payRollTransactionsByEmployee = await invMod.PayRollTransactionsByEmployee.Query().MapToEntity(view);


            invMod.PayRollTransactionsByEmployee.UpdatePayRollTransactionsByEmployee(payRollTransactionsByEmployee).Apply();

            PayRollTransactionsByEmployeeView retView = await invMod.PayRollTransactionsByEmployee.Query().GetViewById(payRollTransactionsByEmployee.PayRollTransactionsByEmployeeId);


            return Ok(retView);

        }

        [HttpGet]
        [Route("View/{PayRollTransactionsByEmployeeId}")]
        [ProducesResponseType(typeof(PayRollTransactionsByEmployeeView), StatusCodes.Status200OK)]

        public async Task<IActionResult> GetPayRollTransactionsByEmployeeView(long payRollTransactionsByEmployeeId)
        {
            PayRollTransactionsByEmployeeModule invMod = new PayRollTransactionsByEmployeeModule();

            PayRollTransactionsByEmployeeView view = await invMod.PayRollTransactionsByEmployee.Query().GetViewById(payRollTransactionsByEmployeeId);
            return Ok(view);
        }
        }
	}
        