using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.EntityFramework;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using lssWebApi2.ContractDomain;

namespace lssWebApi2.Controllers
{
    [Route("api/[controller]")]
    public class ContractController : Controller
    {

[HttpPost]
        [Route("View")]
        [ProducesResponseType(typeof(ContractView), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddContract([FromBody]ContractView view)
        {
            ContractModule invMod = new ContractModule();

            NextNumber nnContract = await invMod.Contract.Query().GetNextNumber();

            view.ContractNumber = nnContract.NextNumberValue;

            Contract contract = await invMod.Contract.Query().MapToEntity(view);

            invMod.Contract.AddContract(contract).Apply();

            ContractView newView = await invMod.Contract.Query().GetViewByNumber(view.ContractNumber);


            return Ok(newView);

        }

        [HttpDelete]
        [Route("View")]
        [ProducesResponseType(typeof(ContractView), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteContract([FromBody]ContractView view)
        {
            ContractModule invMod = new ContractModule();
            Contract contract = await invMod.Contract.Query().MapToEntity(view);
            invMod.Contract.DeleteContract(contract).Apply();

            return Ok(view);
        }

        [HttpPut]
        [Route("View")]
        [ProducesResponseType(typeof(ContractView), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateContract([FromBody]ContractView view)
        {
            ContractModule invMod = new ContractModule();

            Contract contract = await invMod.Contract.Query().MapToEntity(view);


            invMod.Contract.UpdateContract(contract).Apply();

            ContractView retView = await invMod.Contract.Query().GetViewById(contract.ContractId);


            return Ok(retView);

        }

        [HttpGet]
        [Route("View/{ContractId}")]
        [ProducesResponseType(typeof(ContractView), StatusCodes.Status200OK)]

        public async Task<IActionResult> GetContractView(long contractId)
        {
            ContractModule invMod = new ContractModule();

            ContractView view = await invMod.Contract.Query().GetViewById(contractId);
            return Ok(view);
        }
        }
	}
        