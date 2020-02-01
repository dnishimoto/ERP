using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.EntityFramework;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using lssWebApi2.NextNumberDomain;

namespace lssWebApi2.Controllers
{
    [Route("api/[controller]")]
    public class NextNumberController : Controller
    {

[HttpPost]
        [Route("View")]
        [ProducesResponseType(typeof(NextNumberView), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddNextNumber([FromBody]NextNumberView view)
        {
            NextNumberModule invMod = new NextNumberModule();

            NextNumber nnNextNumber = await invMod.NextNumber.Query().GetNextNumber();

            view.NextNumberNumber = nnNextNumber.NextNumberValue;

            NextNumber nextNumber = await invMod.NextNumber.Query().MapToEntity(view);

            invMod.NextNumber.AddNextNumber(nextNumber).Apply();

            NextNumberView newView = await invMod.NextNumber.Query().GetViewByNumber(view.NextNumberNumber);


            return Ok(newView);

        }

        [HttpDelete]
        [Route("View")]
        [ProducesResponseType(typeof(NextNumberView), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteNextNumber([FromBody]NextNumberView view)
        {
            NextNumberModule invMod = new NextNumberModule();
            NextNumber nextNumber = await invMod.NextNumber.Query().MapToEntity(view);
            invMod.NextNumber.DeleteNextNumber(nextNumber).Apply();

            return Ok(view);
        }

        [HttpPut]
        [Route("View")]
        [ProducesResponseType(typeof(NextNumberView), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateNextNumber([FromBody]NextNumberView view)
        {
            NextNumberModule invMod = new NextNumberModule();

            NextNumber nextNumber = await invMod.NextNumber.Query().MapToEntity(view);


            invMod.NextNumber.UpdateNextNumber(nextNumber).Apply();

            NextNumberView retView = await invMod.NextNumber.Query().GetViewById(nextNumber.NextNumberId);


            return Ok(retView);

        }

        [HttpGet]
        [Route("View/{NextNumberId}")]
        [ProducesResponseType(typeof(NextNumberView), StatusCodes.Status200OK)]

        public async Task<IActionResult> GetNextNumberView(long nextNumberId)
        {
            NextNumberModule invMod = new NextNumberModule();

            NextNumberView view = await invMod.NextNumber.Query().GetViewById(nextNumberId);
            return Ok(view);
        }
        }
	}
        