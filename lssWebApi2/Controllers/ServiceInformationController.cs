using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.EntityFramework;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using lssWebApi2.ServiceInformationDomain;
namespace lssWebApi2.Controllers
{
    [Route("api/[controller]")]
    public class ServiceInformationController : Controller
    {

[HttpPost]
        [Route("View")]
        [ProducesResponseType(typeof(ServiceInformationView), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddServiceInformation([FromBody]ServiceInformationView view)
        {
            ServiceInformationModule invMod = new ServiceInformationModule();

            NextNumber nnServiceInformation = await invMod.ServiceInformation.Query().GetNextNumber();

            view.ServiceInformationNumber = nnServiceInformation.NextNumberValue;

            ServiceInformation serviceInformation = await invMod.ServiceInformation.Query().MapToEntity(view);

            invMod.ServiceInformation.AddServiceInformation(serviceInformation).Apply();

            ServiceInformationView newView = await invMod.ServiceInformation.Query().GetViewByNumber(view.ServiceInformationNumber);


            return Ok(newView);

        }

        [HttpDelete]
        [Route("View")]
        [ProducesResponseType(typeof(ServiceInformationView), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteServiceInformation([FromBody]ServiceInformationView view)
        {
            ServiceInformationModule invMod = new ServiceInformationModule();
            ServiceInformation serviceInformation = await invMod.ServiceInformation.Query().MapToEntity(view);
            invMod.ServiceInformation.DeleteServiceInformation(serviceInformation).Apply();

            return Ok(view);
        }

        [HttpPut]
        [Route("View")]
        [ProducesResponseType(typeof(ServiceInformationView), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateServiceInformation([FromBody]ServiceInformationView view)
        {
            ServiceInformationModule invMod = new ServiceInformationModule();

            ServiceInformation serviceInformation = await invMod.ServiceInformation.Query().MapToEntity(view);


            invMod.ServiceInformation.UpdateServiceInformation(serviceInformation).Apply();

            ServiceInformationView retView = await invMod.ServiceInformation.Query().GetViewById(serviceInformation.ServiceId);


            return Ok(retView);

        }

        [HttpGet]
        [Route("View/{ServiceInformationId}")]
        [ProducesResponseType(typeof(ServiceInformationView), StatusCodes.Status200OK)]

        public async Task<IActionResult> GetServiceInformationView(long serviceInformationId)
        {
            ServiceInformationModule invMod = new ServiceInformationModule();

            ServiceInformationView view = await invMod.ServiceInformation.Query().GetViewById(serviceInformationId);
            return Ok(view);
        }
        }
	}
        