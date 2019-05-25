using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.EntityFramework;
using lssWebApi2.SalesOrderManagementDomain;
using lssWebApi2.SalesOrderManagementDomain.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace lssWebApi2.Controllers
{
    [Route("api/[controller]")]
    public class SalesOrderDetailController : Controller
    {
        [HttpPost]
        [Route("View")]
        [ProducesResponseType(typeof(SalesOrderDetailView), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddSalesOrderDetail([FromBody]SalesOrderDetailView view)
        {
            SalesOrderModule sodMod = new SalesOrderModule();

            NextNumber nnSalesOrderDetail = await sodMod.SalesOrderDetail.Query().GetNextNumber();

            view.SalesOrderDetailNumber = nnSalesOrderDetail.NextNumberValue;

            SalesOrderDetail SalesOrderDetail = await sodMod.SalesOrderDetail.Query().MapToEntity(view);

            sodMod.SalesOrderDetail.AddSalesOrderDetail(SalesOrderDetail).Apply();

            SalesOrderDetailView newView = await sodMod.SalesOrderDetail.Query().GetViewByNumber(view.SalesOrderDetailNumber);


            return Ok(newView);

        }

        [HttpDelete]
        [Route("View")]
        [ProducesResponseType(typeof(SalesOrderDetailView), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteSalesOrderDetail([FromBody]SalesOrderDetailView view)
        {
            SalesOrderModule sodMod = new SalesOrderModule();
            SalesOrderDetail SalesOrderDetail = await sodMod.SalesOrderDetail.Query().MapToEntity(view);
            sodMod.SalesOrderDetail.DeleteSalesOrderDetail(SalesOrderDetail).Apply();

            return Ok(view);
        }

        [HttpPut]
        [Route("View")]
        [ProducesResponseType(typeof(SalesOrderDetailView), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateSalesOrderDetail([FromBody]SalesOrderDetailView view)
        {
            SalesOrderModule sodMod = new SalesOrderModule();

            SalesOrderDetail SalesOrderDetail = await sodMod.SalesOrderDetail.Query().MapToEntity(view);


            sodMod.SalesOrderDetail.UpdateSalesOrderDetail(SalesOrderDetail).Apply();

            SalesOrderDetailView updateView = await sodMod.SalesOrderDetail.Query().GetViewById(SalesOrderDetail.SalesOrderDetailId);

            SalesOrderDetailView retView = await sodMod.SalesOrderDetail.Query().GetViewById(SalesOrderDetail.SalesOrderDetailId);

            return Ok(retView);

        }

        [HttpGet]
        [Route("View/{SalesOrderDetailId}")]
        [ProducesResponseType(typeof(SalesOrderDetailView), StatusCodes.Status200OK)]

        public async Task<IActionResult> GetSalesOrderDetailView(long SalesOrderDetailId)
        {
            SalesOrderModule sodMod = new SalesOrderModule();

            SalesOrderDetailView view = await sodMod.SalesOrderDetail.Query().GetViewById(SalesOrderDetailId);
            return Ok(view);
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
