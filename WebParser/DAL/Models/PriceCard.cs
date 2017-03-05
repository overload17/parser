using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
  public class PriceCard
  {
    public int ID { get; set; }
    public int ID_Product { get; set; }
    public int Price { get; set; }
    public int PublishDate { get; set; }

    public PriceCard(int id, int idProduct, int price, int publishDate)
    {
      ID = id;
      ID_Product = idProduct;
      Price = price;
      PublishDate = publishDate;
    }

    public PriceCard(int idProduct, int price, int publishDate)
    {
      ID_Product = idProduct;
      Price = price;
      PublishDate = publishDate;
    }

    public PriceCard()
    {
    }
  }
}