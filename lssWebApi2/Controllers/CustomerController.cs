using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.EntityFramework;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using lssWebApi2.CustomerDomain;

namespace lssWebApi2.Controllers
{
    [Route("api/[controller]")]
    public class CustomerController : Controller
    {

[HttpPost]
        [Route("View")]
        [ProducesResponseType(typeof(CustomerView), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddCustomer([FromBody]CustomerView view)
        {
            CustomerModule invMod = new CustomerModule();

            NextNumber nnCustomer = await invMod.Customer.Query().GetNextNumber();

            view.CustomerNumber = nnCustomer.NextNumberValue;

            Customer customer = await invMod.Customer.Query().MapToEntity(view);

            invMod.Customer.AddCustomer(customer).Apply();

            CustomerView newView = await invMod.Customer.Query().GetViewByNumber(view.CustomerNumber);


            return Ok(newView);

        }

        [HttpDelete]
        [Route("View")]
        [ProducesResponseType(typeof(CustomerView), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteCustomer([FromBody]CustomerView view)
        {
            CustomerModule invMod = new CustomerModule();
            Customer customer = await invMod.Customer.Query().MapToEntity(view);
            invMod.Customer.DeleteCustomer(customer).Apply();

            return Ok(view);
        }

        [HttpPut]
        [Route("View")]
        [ProducesResponseType(typeof(CustomerView), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateCustomer([FromBody]CustomerView view)
        {
            CustomerModule invMod = new CustomerModule();

            Customer customer = await invMod.Customer.Query().MapToEntity(view);


            invMod.Customer.UpdateCustomer(customer).Apply();

            CustomerView retView = await invMod.Customer.Query().GetViewById(customer.CustomerId);


            return Ok(retView);

        }

        [HttpGet]
        [Route("View/{CustomerId}")]
        [ProducesResponseType(typeof(CustomerView), StatusCodes.Status200OK)]

        public async Task<IActionResult> GetCustomerView(long customerId)
        {
            CustomerModule invMod = new CustomerModule();

            CustomerView view = await invMod.Customer.Query().GetViewById(customerId);
            return Ok(view);
        }
        }
	}
        