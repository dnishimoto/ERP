using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.EntityFramework;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using lssWebApi2.POQuoteDomain;

namespace lssWebApi2.Controllers
{
    [Route("api/[controller]")]
    public class POQuoteController : Controller
    {

[HttpPost]
        [Route("View")]
        [ProducesResponseType(typeof(POQuoteView), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddPOQuote([FromBody]POQuoteView view)
        {
            POQuoteModule invMod = new POQuoteModule();

            NextNumber nnPOQuote = await invMod.POQuote.Query().GetNextNumber();

            view.PoquoteNumber = nnPOQuote.NextNumberValue;

            Poquote poQuote = await invMod.POQuote.Query().MapToEntity(view);

            invMod.POQuote.AddPOQuote(poQuote).Apply();

            POQuoteView newView = await invMod.POQuote.Query().GetViewByNumber(view.PoquoteNumber);


            return Ok(newView);

        }

        [HttpDelete]
        [Route("View")]
        [ProducesResponseType(typeof(POQuoteView), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeletePOQuote([FromBody]POQuoteView view)
        {
            POQuoteModule invMod = new POQuoteModule();
            Poquote poQuote = await invMod.POQuote.Query().MapToEntity(view);
            invMod.POQuote.DeletePOQuote(poQuote).Apply();

            return Ok(view);
        }

        [HttpPut]
        [Route("View")]
        [ProducesResponseType(typeof(POQuoteView), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdatePOQuote([FromBody]POQuoteView view)
        {
            POQuoteModule invMod = new POQuoteModule();

            Poquote poQuote = await invMod.POQuote.Query().MapToEntity(view);


            invMod.POQuote.UpdatePOQuote(poQuote).Apply();

            POQuoteView retView = await invMod.POQuote.Query().GetViewById(poQuote.PoquoteId);


            return Ok(retView);

        }

        [HttpGet]
        [Route("View/{POQuoteId}")]
        [ProducesResponseType(typeof(POQuoteView), StatusCodes.Status200OK)]

        public async Task<IActionResult> GetPOQuoteView(long poQuoteId)
        {
            POQuoteModule invMod = new POQuoteModule();

            POQuoteView view = await invMod.POQuote.Query().GetViewById(poQuoteId);
            return Ok(view);
        }
        }
	}
        