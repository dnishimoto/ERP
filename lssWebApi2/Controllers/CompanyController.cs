using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.EntityFramework;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using lssWebApi2.CompanyDomain;

namespace lssWebApi2.Controllers
{
    [Route("api/[controller]")]
    public class CompanyController : Controller
    {

[HttpPost]
        [Route("View")]
        [ProducesResponseType(typeof(CompanyView), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddCompany([FromBody]CompanyView view)
        {
            CompanyModule invMod = new CompanyModule();

            NextNumber nnCompany = await invMod.Company.Query().GetNextNumber();

            view.CompanyNumber = nnCompany.NextNumberValue;

            Company company = await invMod.Company.Query().MapToEntity(view);

            invMod.Company.AddCompany(company).Apply();

            CompanyView newView = await invMod.Company.Query().GetViewByNumber(view.CompanyNumber);


            return Ok(newView);

        }

        [HttpDelete]
        [Route("View")]
        [ProducesResponseType(typeof(CompanyView), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteCompany([FromBody]CompanyView view)
        {
            CompanyModule invMod = new CompanyModule();
            Company company = await invMod.Company.Query().MapToEntity(view);
            invMod.Company.DeleteCompany(company).Apply();

            return Ok(view);
        }

        [HttpPut]
        [Route("View")]
        [ProducesResponseType(typeof(CompanyView), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateCompany([FromBody]CompanyView view)
        {
            CompanyModule invMod = new CompanyModule();

            Company company = await invMod.Company.Query().MapToEntity(view);


            invMod.Company.UpdateCompany(company).Apply();

            CompanyView retView = await invMod.Company.Query().GetViewById(company.CompanyId);


            return Ok(retView);

        }

        [HttpGet]
        [Route("View/{CompanyId}")]
        [ProducesResponseType(typeof(CompanyView), StatusCodes.Status200OK)]

        public async Task<IActionResult> GetCompanyView(long companyId)
        {
            CompanyModule invMod = new CompanyModule();

            CompanyView view = await invMod.Company.Query().GetViewById(companyId);
            return Ok(view);
        }
        }
	}
        