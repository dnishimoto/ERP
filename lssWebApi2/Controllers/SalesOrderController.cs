using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using lssWebApi2.SalesOrderDetailDomain;
using lssWebApi2.SalesOrderDomain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace lssWebApi2.Controllers
{
    [Route("api/[controller]")]
    public class SalesOrderController : Controller
    {
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
        [HttpPut]
        [Route("View")]
        [ProducesResponseType(typeof(SalesOrderView), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateSalesOrder([FromBody]SalesOrderView view)
        {
            SalesOrderModule salesOrderMod = new SalesOrderModule();

            SalesOrder salesOrder = await salesOrderMod.SalesOrder.Query().MapToEntity(view);

            IList<SalesOrderDetail> salesOrderDetails = await salesOrderMod.SalesOrderDetail.Query().MapToEntity((view.SalesOrderDetailViews).ToList<SalesOrderDetailView>());

            salesOrder = salesOrderMod.SalesOrder.Query().SumAmounts(salesOrder, salesOrderDetails);

            salesOrderMod.SalesOrderDetail.UpdateSalesOrderDetails(salesOrderDetails).Apply();

            salesOrderMod.SalesOrder.UpdateSalesOrder(salesOrder).Apply();

            return Ok(view);

        }
        [HttpDelete]
        [Route("View/{salesOrderId}")]
        [ProducesResponseType(typeof(SalesOrderView), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteSalesOrder(long salesOrderId)
        {
            SalesOrderModule salesOrderMod = new SalesOrderModule();

            List<SalesOrderDetail> salesOrderDetails = (await salesOrderMod.SalesOrderDetail.Query().GetDetailsBySalesOrderId(salesOrderId)).ToList<SalesOrderDetail>();
            IList<SalesOrderDetailView> listDetailViews = await salesOrderMod.SalesOrderDetail.Query().GetDetailViewsBySalesOrderId(salesOrderId);

            salesOrderMod.SalesOrderDetail.DeleteSalesOrderDetails(salesOrderDetails).Apply();

            SalesOrder salesOrder = await salesOrderMod.SalesOrder.Query().GetEntityById(salesOrderId);

            salesOrderMod.SalesOrder.DeleteSalesOrder(salesOrder).Apply();

            SalesOrderView view = await salesOrderMod.SalesOrder.Query().MapToView(salesOrder);
            view.SalesOrderDetailViews = listDetailViews;

            return Ok(view);

        }
        [HttpPost]
        [Route("View")]
        [ProducesResponseType(typeof(SalesOrderView), StatusCodes.Status200OK)]
        public async Task<IActionResult> AddSalesOrder([FromBody]SalesOrderView view)
        {
            SalesOrderModule salesOrderMod = new SalesOrderModule();

            List<SalesOrderDetailView> detailViews = view.SalesOrderDetailViews.ToList<SalesOrderDetailView>();

            Udc orderType = await salesOrderMod.SalesOrder.Query().GetUdc("ORDER_TYPE", SalesOrderEnum.CASH_SALES.ToString());
            Udc paymentTerms = await salesOrderMod.SalesOrder.Query().GetUdc("PAYMENTTERMS", PaymentTermsEnum.Net_2_10_30.ToString());
            NextNumber nnSalesOrder = await salesOrderMod.SalesOrder.Query().GetNextNumber();

            view.OrderNumber = nnSalesOrder.NextNumberValue.ToString();

            SalesOrder salesOrder = await salesOrderMod.SalesOrder.Query().MapToEntity(view);

            salesOrderMod.SalesOrder.AddSalesOrder(salesOrder).Apply();

            SalesOrder newSalesOrder = await salesOrderMod.SalesOrder.Query().GetEntityByNumber(view.OrderNumber);

            detailViews.ForEach(m => m.SalesOrderId = newSalesOrder.SalesOrderId);

            List<SalesOrderDetail> salesOrderDetails = (await salesOrderMod.SalesOrderDetail.Query().MapToEntity(detailViews)).ToList<SalesOrderDetail>();

            salesOrderMod.SalesOrderDetail.AddSalesOrderDetails(salesOrderDetails).Apply();

            newSalesOrder = salesOrderMod.SalesOrder.Query().SumAmounts(newSalesOrder,salesOrderDetails);

            salesOrderMod.SalesOrder.UpdateSalesOrder(newSalesOrder).Apply();
 
            SalesOrderView newSalesOrderView = await salesOrderMod.SalesOrder.Query().MapToView(newSalesOrder);

            List<SalesOrderDetailView> listDetailViews = (await salesOrderMod.SalesOrderDetail.Query().GetDetailViewsBySalesOrderId(newSalesOrder.SalesOrderId)).ToList<SalesOrderDetailView>();

            newSalesOrderView.SalesOrderDetailViews = listDetailViews;

            return Ok(newSalesOrderView);
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
