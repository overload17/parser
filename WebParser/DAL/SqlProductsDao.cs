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

    public void AddProduct(Product product)
    {
      using (var context = new OnlineShopEntities())
      {
        var productDb = (Products) ConvertToStoredProduct(product);
        context.Products.Add(productDb);
        context.SaveChanges();
      }
    }

    public void AddPriceCard(PriceCard priceCard)
    {
      using (var context = new OnlineShopEntities())
      {
        var priceCardDb = (Prices) ConvertToStoredPriceCard(priceCard);
        context.Prices.Add(priceCardDb);
        context.SaveChanges();
      }
    }

    public Product GetProductById(int id)
    {
      using (var context = new OnlineShopEntities())
      {
        return context.Products.Where(p => p.ID == id).Select(p => p).ToList().Select(ConvertToProduct).ToList().First();
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
        return context.Products.Select(p => p).ToList().Select(ConvertToProduct).ToList();
      }
    }

    public List<PriceCard> GetAllPriceCards()
    {
      using (var context = new OnlineShopEntities())
      {
        return context.Prices.Select(p => p).ToList().Select(ConvertToPriceCard).ToList();
      }
    }

    public List<PriceCard> GetAllPriceCardsByProductID(int id)
    {
      using (var context = new OnlineShopEntities())
      {
        return context.Prices.Where(q => q.ID_Product == id).Select(p => p).ToList().Select(ConvertToPriceCard).ToList();
      }
    }

    public void DeleteProductById(int id)
    {
      using (var context = new OnlineShopEntities())
      {
        context.Products.Remove(context.Products.Where(x => x.ID == id).Select(x => x).FirstOrDefault());
      }
    }

    public void DeletePriceCardById(int id)
    {
      using (var context = new OnlineShopEntities())
      {
        context.Prices.Remove(context.Prices.Where(x => x.ID == id).Select(x => x).FirstOrDefault());
      }
    }

    public void EditProduct(Product product)
    {
      using (var context = new OnlineShopEntities())
      {
        var productDb = context.Products.First(n => n.ID == product.ID);
        productDb.Description = product.Description;
        productDb.Title = product.Title;
        productDb.Link = product.Link;
        productDb.ImageBase64 = product.ImageBase64;
      }
    }

    public void EditPriceCard(PriceCard priceCard)
    {
      using (var context = new OnlineShopEntities())
      {
        var priceCardDb = context.Prices.First(n => n.ID == priceCard.ID);
        priceCardDb.ID_Product = priceCard.ID_Product;
        priceCardDb.PublishDate = priceCard.PublishDate;
        priceCardDb.Price = priceCard.Price;
      }
    }

    public object ConvertToStoredProduct(Product product)
    {
      var result = new Products
      {
        ID = product.ID,
        Title = product.Title,
        Description = product.Description,
        ImageBase64 = product.ImageBase64,
        Link = product.Link
      };
      return result;
    }

    public object ConvertToStoredPriceCard(PriceCard product)
    {
      var result = new Prices
      {
        ID = product.ID,
        ID_Product = product.ID_Product,
        Price = product.Price,
        PublishDate = product.PublishDate
      };
      return result;
    }

    public PriceCard ConvertToPriceCard(Prices product)
    {
      return new PriceCard
      {
        ID = product.ID,
        ID_Product = product.ID_Product,
        Price = product.Price,
        PublishDate = product.PublishDate
      };
    }

    public Product ConvertToProduct(Products product)
    {
      return new Product
      {
        ID = product.ID,
        Title = product.Title,
        Description = product.Description,
        ImageBase64 = product.ImageBase64,
        Link = product.Link
      };
    }

    public Product GetProductByLink(string link)
    {
      using (var context = new OnlineShopEntities())
      {
        return context.Products.Where(p => p.Link == link).ToList().Select(ConvertToProduct).ToList().FirstOrDefault();
      }
    }

    public PriceCard GetLastPriceByProductID(int id)
    {
      using (var context = new OnlineShopEntities())
      {
        return
          context.Prices.Where(q => q.ID_Product == id)
            .Select(ConvertToPriceCard)
            .ToList()
            .OrderByDescending(p => p.PublishDate)
            .ToList()
            .First();
      }
    }

    public static IProductDao GetInstance()
    {
      return _uniqueNotificationsDao ?? (_uniqueNotificationsDao = new SqlProductsDao());
    }
  }
}