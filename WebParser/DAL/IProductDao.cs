using System.Collections.Generic;
using DAL.Models;

namespace DAL
{
  public interface IProductDao
  {
    void AddProduct(Product product);
    void AddPriceCard(PriceCard priceCard);
    Product GetProductById(int id);
    PriceCard GetPriceById(int id);
    List<PriceCard> GetPriceByProductId(int id);
    List<Product> GetAllProducts();
    List<PriceCard> GetAllPriceCards();
    List<PriceCard> GetAllPriceCardsByProductID(int id);
    void DeleteProductById(int id);
    void DeletePriceCardById(int id);
    void EditProduct(Product product);
    void EditPriceCardt(PriceCard priceCard);
    object ConvertToStoredProduct(Product product);
    Product ConvertToProduct(Products product);
    object ConvertToStoredPriceCard(PriceCard product);
    PriceCard ConvertToPriceCard(Prices product);
  }
}