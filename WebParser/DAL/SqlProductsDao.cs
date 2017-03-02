using System.Collections.Generic;
using System.Linq;
using DAL.Models;

namespace DAL
{
  public class SqlProductsDao : IProductDao
  {
    private static IProductDao _uniqueNotificationsDao;

    private SqlProductsDao()
    {
    }

    public static IProductDao GetInstance()
    {
      return _uniqueNotificationsDao ?? (_uniqueNotificationsDao = new SqlProductsDao());
    }

    public void AddProduct(Product product)
    {
      using (var context = new OnlineShopEntities())
      {
        var productDb = (Products)ConvertToStoredProduct(product);
        context.Products.Add(productDb);
        context.SaveChanges();
      }
    }

    public void AddPriceCard(PriceCard priceCard)
    {
      using (var context = new OnlineShopEntities())
      {
        var priceCardDb = (Prices)ConvertToStoredPriceCard(priceCard);
        context.Prices.Add(priceCardDb);
        context.SaveChanges();
      }
    }

    public Product GetProductById(int id)
    {
      using (var context = new OnlineShopEntities())
      {
        return ConvertToProduct(context.Products.Find(id));
      }
    }

    public PriceCard GetPriceById(int id)
    {
      using (var context = new OnlineShopEntities())
      {
        return ConvertToPriceCard(context.Prices.Find(id));
      }
    }

    public List<PriceCard> GetPriceByProductId(int id)
    {
      using (var context = new OnlineShopEntities())
      {
        return context.Prices.Where(p => p.ID_Product == id).Select(q => ConvertToPriceCard(q)).ToList();
      }
    }

    public List<Product> GetAllProducts()
    {
      using (var context = new OnlineShopEntities())
      {
        return context.Products.Select(p => ConvertToProduct(p)).ToList();
      }
    }

    public List<PriceCard> GetAllPriceCards()
    {
      using (var context = new OnlineShopEntities())
      {
        return context.Prices.Select(p => ConvertToPriceCard(p)).ToList();
      }
    }

    public List<PriceCard> GetAllPriceCardsByProductID(int id)
    {
      throw new System.NotImplementedException();
    }

    public void DeleteProductById(int id)
    {
      throw new System.NotImplementedException();
    }

    public void DeletePriceCardById(int id)
    {
      throw new System.NotImplementedException();
    }

    public void EditProduct(Product product)
    {
      throw new System.NotImplementedException();
    }

    public void EditPriceCardt(PriceCard priceCard)
    {
      throw new System.NotImplementedException();
    }

    public object ConvertToStoredProduct(Product product)
    {
      throw new System.NotImplementedException();
    }

    public object ConvertToStoredPriceCard(PriceCard product)
    {
      throw new System.NotImplementedException();
    }

    public PriceCard ConvertToPriceCard(Prices product)
    {
      throw new System.NotImplementedException();
    }

    public Product ConvertToProduct(Products product)
    {
      throw new System.NotImplementedException();
    }
  }
}