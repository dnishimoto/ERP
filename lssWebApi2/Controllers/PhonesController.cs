using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.EntityFramework;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using lssWebApi2.AddressBookDomain;

namespace lssWebApi2.Controllers
{
    [Route("api/[controller]")]
    public class PhonesController : Controller
    {

[HttpPost]
        [Route("View")]
        [ProducesResponseType(typeof(PhoneEntityView), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddPhones([FromBody]PhoneEntityView view)
        {
            PhoneModule invMod = new PhoneModule();

            NextNumber nnPhones = await invMod.Phone.Query().GetNextNumber();

            view.PhoneEntityNumber = nnPhones.NextNumberValue;

            PhoneEntity phones = await invMod.Phone.Query().MapToEntity(view);

            invMod.Phone.AddPhones(phones).Apply();

            PhoneEntityView newView = await invMod.Phone.Query().GetViewByNumber(view.PhoneEntityNumber);


            return Ok(newView);

        }

        [HttpDelete]
        [Route("View")]
        [ProducesResponseType(typeof(PhoneEntityView), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeletePhones([FromBody]PhoneEntityView view)
        {
            PhoneModule invMod = new PhoneModule();
            PhoneEntity phones = await invMod.Phone.Query().MapToEntity(view);
            invMod.Phone.DeletePhones(phones).Apply();

            return Ok(view);
        }

        [HttpPut]
        [Route("View")]
        [ProducesResponseType(typeof(PhoneEntityView), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdatePhones([FromBody]PhoneEntityView view)
        {
            PhoneModule invMod = new PhoneModule();

            PhoneEntity phones = await invMod.Phone.Query().MapToEntity(view);


            invMod.Phone.UpdatePhones(phones).Apply();

            PhoneEntityView retView = await invMod.Phone.Query().GetViewById(phones.PhoneId);


            return Ok(retView);

        }

        [HttpGet]
        [Route("View/{PhonesId}")]
        [ProducesResponseType(typeof(PhoneEntityView), StatusCodes.Status200OK)]

        public async Task<IActionResult> GetPhoneEntityView(long phonesId)
        {
            PhoneModule invMod = new PhoneModule();

            PhoneEntityView view = await invMod.Phone.Query().GetViewById(phonesId);
            return Ok(view);
        }
        }
	}
        