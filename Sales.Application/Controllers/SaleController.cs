using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Sales.Domain.Aggregates;
using Sales.Infrastructure;
using System.Collections.Generic;

namespace Sales.Application.Controllers
{

    [Route("api")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private SaleEventRepository _repository = null;
        public SaleController()
        {
            this._repository = new SaleEventRepository("SalesStream");
        }

        [HttpGet]
        [Route("director/getAllSales")]
        [Authorize(Roles = "director")]
        public ActionResult<IEnumerable<JObject>> GetAllSales()
        {
            return Ok(_repository.GetSales());
        }

        [HttpGet]
        [Authorize(Roles = "director")]
        [Route("director/getTotalAmount")]
        public ActionResult<double> GetSalesTotalPrice()
        {
            return Ok(_repository.GetSalesTotalPrice());
        }

        [HttpGet]
        [Route("InventoryManager/getAll")]
        [Authorize(Roles = "manager")]
        public ActionResult<IEnumerable<JToken>> GetInventory()
        {
            return Ok(_repository.GetInventory());
        }

        [HttpPost]
        [Authorize(Roles = "saleman")]
        [Route("saleman/addSale")]
        public OkObjectResult AddSale([FromBody] Sale sale)
        {
            var savedSale = new Sale(sale.Quantity, sale.Product.Name, sale.Price);
            _repository.Save(savedSale);
            return Ok(savedSale);
        }
    }
}
