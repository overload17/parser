using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DAL;
using Newtonsoft.Json;

namespace Api.Controllers
{
  public class FeedController : ApiController
  {
    private static IProductDao _dao = SqlProductsDao.GetInstance();

    [HttpGet]
    [Route("product/{ID:int}")]
    public HttpResponseMessage GetProductById(int id)
    {
      _dao = SqlProductsDao.GetInstance();
      HttpResponseMessage rm;
      try
      {
        var product = _dao.GetProductById(id);
        var fullFeed =
          new List<object>
          {
            new
            {
              product.ID,
              product.Title,
              product.Description,
              product.Link,
              product.ImageBase64
            }
          };
        rm = Request.CreateResponse(HttpStatusCode.OK, JsonConvert.SerializeObject(fullFeed));
      }
      catch (Exception)
      {
        rm = Request.CreateErrorResponse(HttpStatusCode.BadRequest, new HttpError());
      }
      rm.Headers.Add("Access-Control-Allow-Origin", "*");
      return rm;
    }

    [HttpGet]
    [Route("product/price/{ID:int}")]
    public HttpResponseMessage GetPricesById(int id)
    {
      _dao = SqlProductsDao.GetInstance();
      var prices = _dao.GetAllPriceCardsByProductID(id);
      var hrm = Request.CreateResponse(HttpStatusCode.OK, JsonConvert.SerializeObject(prices));
      hrm.Headers.Add("Access-Control-Allow-Origin", "*");
      return hrm;
    }

    [HttpGet]
    [Route("product/all")]
    public HttpResponseMessage GetAllProducts()
    {
      _dao = SqlProductsDao.GetInstance();
      var news = _dao.GetAllProducts();
      var hrm = Request.CreateResponse(HttpStatusCode.OK, JsonConvert.SerializeObject(news));
      hrm.Headers.Add("Access-Control-Allow-Origin", "*");
      hrm.Headers.Add("Access-Control-Expose-Headers", "Date");
      return hrm;
    }
  }
}