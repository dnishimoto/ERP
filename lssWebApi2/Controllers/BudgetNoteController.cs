using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.EntityFramework;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using lssWebApi2.BudgetNoteDomain;

namespace lssWebApi2.Controllers
{
    [Route("api/[controller]")]
    public class BudgetNoteController : Controller
    {

[HttpPost]
        [Route("View")]
        [ProducesResponseType(typeof(BudgetNoteView), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddBudgetNote([FromBody]BudgetNoteView view)
        {
            BudgetNoteModule invMod = new BudgetNoteModule();

            NextNumber nnBudgetNote = await invMod.BudgetNote.Query().GetNextNumber();

            view.BudgetNoteNumber = nnBudgetNote.NextNumberValue;

            BudgetNote budgetNote = await invMod.BudgetNote.Query().MapToEntity(view);

            invMod.BudgetNote.AddBudgetNote(budgetNote).Apply();

            BudgetNoteView newView = await invMod.BudgetNote.Query().GetViewByNumber(view.BudgetNoteNumber);


            return Ok(newView);

        }

        [HttpDelete]
        [Route("View")]
        [ProducesResponseType(typeof(BudgetNoteView), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteBudgetNote([FromBody]BudgetNoteView view)
        {
            BudgetNoteModule invMod = new BudgetNoteModule();
            BudgetNote budgetNote = await invMod.BudgetNote.Query().MapToEntity(view);
            invMod.BudgetNote.DeleteBudgetNote(budgetNote).Apply();

            return Ok(view);
        }

        [HttpPut]
        [Route("View")]
        [ProducesResponseType(typeof(BudgetNoteView), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateBudgetNote([FromBody]BudgetNoteView view)
        {
            BudgetNoteModule invMod = new BudgetNoteModule();

            BudgetNote budgetNote = await invMod.BudgetNote.Query().MapToEntity(view);


            invMod.BudgetNote.UpdateBudgetNote(budgetNote).Apply();

            BudgetNoteView retView = await invMod.BudgetNote.Query().GetViewById(budgetNote.BudgetNoteId);


            return Ok(retView);

        }

        [HttpGet]
        [Route("View/{BudgetNoteId}")]
        [ProducesResponseType(typeof(BudgetNoteView), StatusCodes.Status200OK)]

        public async Task<IActionResult> GetBudgetNoteView(long budgetNoteId)
        {
            BudgetNoteModule invMod = new BudgetNoteModule();

            BudgetNoteView view = await invMod.BudgetNote.Query().GetViewById(budgetNoteId);
            return Ok(view);
        }
        }
	}
        