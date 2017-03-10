using System;
using DAL;
using DAL.Models;
using Newtonsoft.Json;
using NUnit.Framework;

namespace UnitTests
{
  public class UnitTest1
  {
    private static readonly IProductDao Dao = SqlProductsDao.GetInstance();

    [TestCase("Samsung S4", "Phone", "https://www.google.com.ua/SamsungS4", "123")]
    [TestCase("Apple Watch", "Watch", "https://www.google.com.ua/AppleWatch", "123")]
    [TestCase("Ipad 4", "Tablet", "https://www.google.com.ua/Ipad4", "123")]
    [TestCase("Samsung S8", "Phone", "https://www.google.com.ua/SamsungS8", "123")]
    public void TestAddProduct(string title, string descr, string link, string pic)
    {
      var expected = Dao.GetAllProducts().Count+1;
      Dao.AddProduct(new Product(title, descr, link, pic));
      Assert.AreEqual(expected, Dao.GetAllProducts().Count);
    }

    [TestCase(227, 10000, 1488801932)]
    [TestCase(225, 10000, 1488801932)]
    [TestCase(220, 10000, 1488801932)]
    [TestCase(221, 10000, 1488801932)]
    public void TestAddPrice(int id, int price, int time)
    {
      var expected = Dao.GetAllPriceCards().Count + 1;
      Dao.AddPriceCard(new PriceCard(id, price, time));
      Assert.AreEqual(expected, Dao.GetAllPriceCards().Count);
    }

    [TestCase("Samsung S4", "Phone", "https://www.google.com.ua/SamsungS4", "123", 234)]
    [TestCase("Apple Watch", "Watch", "https://www.google.com.ua/AppleWatch", "123", 235)]
    [TestCase("Ipad 4", "Tablet", "https://www.google.com.ua/Ipad4", "123", 236)]
    [TestCase("Samsung S8", "Phone", "https://www.google.com.ua/SamsungS8", "123", 237)]
    public void TestGetProductById(string title, string descr, string link, string pic, int id)
    {
      var expected = JsonConvert.SerializeObject(new Product(id,title, descr, link, pic));
      var actual = JsonConvert.SerializeObject(Dao.GetProductById(id));
      Assert.AreEqual(expected, actual);
    }

    [TestCase("Samsung S4", "Phone", "https://www.google.com.ua/SamsungS4", "123", 234)]
    [TestCase("Apple Watch", "Watch", "https://www.google.com.ua/AppleWatch", "123", 235)]
    [TestCase("Ipad 4", "Tablet", "https://www.google.com.ua/Ipad4", "123", 236)]
    [TestCase("Samsung S8", "Phone", "https://www.google.com.ua/SamsungS8", "123", 237)]
    public void TestGetProductByLink(string title, string descr, string link, string pic, int id)
    {
      var expected = JsonConvert.SerializeObject(new Product(id, title, descr, link, pic));
      var actual = JsonConvert.SerializeObject(Dao.GetProductByLink(link));
      Assert.AreEqual(expected, actual);
    }

    [TestCase(227, 10000, 1488801932, 196)]
    [TestCase(225, 10000, 1488801932, 197)]
    [TestCase(220, 10000, 1488801932, 198)]
    [TestCase(221, 10000, 1488801932, 199)]
    public void TestGetPriceById(int idprod, int price, int time, int id)
    {
      var expected = JsonConvert.SerializeObject(new PriceCard(id, idprod, price, time));
      var actual = JsonConvert.SerializeObject(Dao.GetPriceById(id));
      Assert.AreEqual(expected, actual);
    }
}
}
