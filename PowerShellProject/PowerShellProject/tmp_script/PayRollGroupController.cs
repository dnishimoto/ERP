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
    public class PayRollGroupController : Controller
    {

[HttpPost]
        [Route("View")]
        [ProducesResponseType(typeof(PayRollGroupView), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddPayRollGroup([FromBody]PayRollGroupView view)
        {
            PayRollGroupModule invMod = new PayRollGroupModule();

            NextNumber nnPayRollGroup = await invMod.PayRollGroup.Query().GetNextNumber();

            view.PayRollGroupNumber = nnPayRollGroup.NextNumberValue;

            PayRollGroup payRollGroup = await invMod.PayRollGroup.Query().MapToEntity(view);

            invMod.PayRollGroup.AddComment(payRollGroup).Apply();

            PayRollGroupView newView = await invMod.PayRollGroup.Query().GetViewByNumber(view.PayRollGroupNumber);


            return Ok(newView);

        }

        [HttpDelete]
        [Route("View")]
        [ProducesResponseType(typeof(PayRollGroupView), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeletePayRollGroup([FromBody]PayRollGroupView view)
        {
            PayRollGroupModule invMod = new PayRollGrouptModule();
            PayRollGroup payRollGroup = await invMod.PayRollGroup.Query().MapToEntity(view);
            invMod.PayRollGroup.DeleteComment(payRollGroup).Apply();

            return Ok(view);
        }

        [HttpPut]
        [Route("View")]
        [ProducesResponseType(typeof(PayRollGroupView), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdatePayRollGroup([FromBody]PayRollGroupView view)
        {
            PayRollGroupModule invMod = new PayRollGroupModule();

            PayRollGroup payRollGroup = await invMod.PayRollGroup.Query().MapToEntity(view);


            invMod.PayRollGroup.UpdatePayRollGroup(payRollGroup).Apply();

            PayRollGroupView retView = await invMod.PayRollGroup.Query().GetViewById(payRollGroup.PayRollGroupId);


            return Ok(retView);

        }

        [HttpGet]
        [Route("View/{PayRollGroupId}")]
        [ProducesResponseType(typeof(PayRollGroupView), StatusCodes.Status200OK)]

        public async Task<IActionResult> GetPayRollGroupView(long payRollGroupId)
        {
            PayRollGroupModule invMod = new PayRollGroupModule();

            PayRollGroupView view = await invMod.PayRollGroup.Query().GetViewById(payRollGroupId);
            return Ok(view);
        }
        }
        