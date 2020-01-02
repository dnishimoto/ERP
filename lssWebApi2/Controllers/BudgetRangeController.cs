using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.EntityFramework;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using lssWebApi2.BudgetRangeDomain;

namespace lssWebApi2.Controllers
{
    [Route("api/[controller]")]
    public class BudgetRangeController : Controller
    {

[HttpPost]
        [Route("View")]
        [ProducesResponseType(typeof(BudgetRangeView), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddBudgetRange([FromBody]BudgetRangeView view)
        {
            BudgetRangeModule invMod = new BudgetRangeModule();

            NextNumber nnBudgetRange = await invMod.BudgetRange.Query().GetNextNumber();

            view.BudgetRangeNumber = nnBudgetRange.NextNumberValue;

            BudgetRange budgetRange = await invMod.BudgetRange.Query().MapToEntity(view);

            invMod.BudgetRange.AddBudgetRange(budgetRange).Apply();

            BudgetRangeView newView = await invMod.BudgetRange.Query().GetViewByNumber(view.BudgetRangeNumber);


            return Ok(newView);

        }

        [HttpDelete]
        [Route("View")]
        [ProducesResponseType(typeof(BudgetRangeView), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteBudgetRange([FromBody]BudgetRangeView view)
        {
            BudgetRangeModule invMod = new BudgetRangeModule();
            BudgetRange budgetRange = await invMod.BudgetRange.Query().MapToEntity(view);
            invMod.BudgetRange.DeleteBudgetRange(budgetRange).Apply();

            return Ok(view);
        }

        [HttpPut]
        [Route("View")]
        [ProducesResponseType(typeof(BudgetRangeView), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateBudgetRange([FromBody]BudgetRangeView view)
        {
            BudgetRangeModule invMod = new BudgetRangeModule();

            BudgetRange budgetRange = await invMod.BudgetRange.Query().MapToEntity(view);


            invMod.BudgetRange.UpdateBudgetRange(budgetRange).Apply();

            BudgetRangeView retView = await invMod.BudgetRange.Query().GetViewById(budgetRange.RangeId);


            return Ok(retView);

        }

        [HttpGet]
        [Route("View/{BudgetRangeId}")]
        [ProducesResponseType(typeof(BudgetRangeView), StatusCodes.Status200OK)]

        public async Task<IActionResult> GetBudgetRangeView(long budgetRangeId)
        {
            BudgetRangeModule invMod = new BudgetRangeModule();

            BudgetRangeView view = await invMod.BudgetRange.Query().GetViewById(budgetRangeId);
            return Ok(view);
        }
        }
	}
        