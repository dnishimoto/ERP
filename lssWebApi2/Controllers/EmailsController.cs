using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.EntityFramework;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using lssWebApi2.AddressBookDomain;
using lssWebApi2.EmailDomain;

namespace lssWebApi2.Controllers
{
    [Route("api/[controller]")]
    public class EmailsController : Controller
    {

[HttpPost]
        [Route("View")]
        [ProducesResponseType(typeof(EmailEntityView), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddEmails([FromBody]EmailEntityView view)
        {
            EmailModule invMod = new EmailModule();

            NextNumber nnEmails = await invMod.Email.Query().GetNextNumber();

            view.EmailEntityNumber = nnEmails.NextNumberValue;

            EmailEntity emailEntity = await invMod.Email.Query().MapToEntity(view);

            invMod.Email.AddEmail(emailEntity).Apply();

            EmailEntityView newView = await invMod.Email.Query().GetViewByNumber(view.EmailEntityNumber);


            return Ok(newView);

        }

        [HttpDelete]
        [Route("View")]
        [ProducesResponseType(typeof(EmailEntityView), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteEmails([FromBody]EmailEntityView view)
        {
            EmailModule invMod = new EmailModule();
            EmailEntity email = await invMod.Email.Query().MapToEntity(view);
            invMod.Email.DeleteEmail(email).Apply();

            return Ok(view);
        }

        [HttpPut]
        [Route("View")]
        [ProducesResponseType(typeof(EmailEntityView), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateEmails([FromBody]EmailEntityView view)
        {
            EmailModule invMod = new EmailModule();

            EmailEntity email = await invMod.Email.Query().MapToEntity(view);


            invMod.Email.UpdateEmail(email).Apply();

            EmailEntityView retView = await invMod.Email.Query().GetViewById(email.EmailId);


            return Ok(retView);

        }

        [HttpGet]
        [Route("View/{EmailsId}")]
        [ProducesResponseType(typeof(EmailEntityView), StatusCodes.Status200OK)]

        public async Task<IActionResult> GetEmailsView(long emailId)
        {
            EmailModule invMod = new EmailModule();

            EmailEntityView view = await invMod.Email.Query().GetViewById(emailId);
            return Ok(view);
        }
        }
	}
        