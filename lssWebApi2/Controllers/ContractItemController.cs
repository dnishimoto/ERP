using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.EntityFramework;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using lssWebApi2.ContractItemDomain;

namespace lssWebApi2.Controllers
{
    [Route("api/[controller]")]
    public class ContractItemController : Controller
    {

[HttpPost]
        [Route("View")]
        [ProducesResponseType(typeof(ContractItemView), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddContractItem([FromBody]ContractItemView view)
        {
            ContractItemModule invMod = new ContractItemModule();

            NextNumber nnContractItem = await invMod.ContractItem.Query().GetNextNumber();

            view.ContractItemNumber = nnContractItem.NextNumberValue;

            ContractItem contractItem = await invMod.ContractItem.Query().MapToEntity(view);

            invMod.ContractItem.AddContractItem(contractItem).Apply();

            ContractItemView newView = await invMod.ContractItem.Query().GetViewByNumber(view.ContractItemNumber);


            return Ok(newView);

        }

        [HttpDelete]
        [Route("View")]
        [ProducesResponseType(typeof(ContractItemView), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteContractItem([FromBody]ContractItemView view)
        {
            ContractItemModule invMod = new ContractItemModule();
            ContractItem contractItem = await invMod.ContractItem.Query().MapToEntity(view);
            invMod.ContractItem.DeleteContractItem(contractItem).Apply();

            return Ok(view);
        }

        [HttpPut]
        [Route("View")]
        [ProducesResponseType(typeof(ContractItemView), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateContractItem([FromBody]ContractItemView view)
        {
            ContractItemModule invMod = new ContractItemModule();

            ContractItem contractItem = await invMod.ContractItem.Query().MapToEntity(view);


            invMod.ContractItem.UpdateContractItem(contractItem).Apply();

            ContractItemView retView = await invMod.ContractItem.Query().GetViewById(contractItem.ContractItemId);


            return Ok(retView);

        }

        [HttpGet]
        [Route("View/{ContractItemId}")]
        [ProducesResponseType(typeof(ContractItemView), StatusCodes.Status200OK)]

        public async Task<IActionResult> GetContractItemView(long contractItemId)
        {
            ContractItemModule invMod = new ContractItemModule();

            ContractItemView view = await invMod.ContractItem.Query().GetViewById(contractItemId);
            return Ok(view);
        }
        }
	}
        