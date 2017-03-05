using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using DAL;
using DAL.Models;
using HtmlAgilityPack;

namespace WebParser
{
  public static class Processor
  {
    private static readonly IProductDao Dao = SqlProductsDao.GetInstance();

    private static void ParseList(HtmlNodeCollection feed, List<Product> productsList)
    {
      foreach (var feedItems in feed)
      {
        string title;
        string description;
        string link;
        string image;
        string price;
        var publishDate = (int) DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
        using (var client = new WebClient())
        {
          link = feedItems.SelectSingleNode(".//a").Attributes["href"].Value;
          var data = client.DownloadData(link);
          var html = Encoding.UTF8.GetString(data);
          var doc = new HtmlDocument();
          image = GetBase64Image(link);
          doc.LoadHtml(html);
          title = doc.DocumentNode.SelectNodes("//*[@id='primary_block']/h1").First().ChildNodes[0].InnerText;
          description =
            doc.DocumentNode.SelectNodes("//*[@id='short_description_content']").First().ChildNodes[0].InnerText;
          price =
            Regex.Replace(doc.DocumentNode.SelectNodes("//*[@id='our_price_display']").First().ChildNodes[0].InnerText,
              @"\s+", "");
        }
        if (!productsList.Any() || productsList.Count(x => x.Link == link) == 0)
        {
          Dao.AddProduct(new Product(title, description, link, image));
          var product = Dao.GetProductByLink(link);
          Dao.AddPriceCard(new PriceCard(product.ID, int.Parse(price), publishDate));
        }
        else
        {
          var product = Dao.GetProductByLink(link);
          if (Dao.GetLastPriceByProductID(product.ID).Price != int.Parse(price))
            Dao.AddPriceCard(new PriceCard(product.ID, int.Parse(price), publishDate));
        }
      }
    }

    public static void GetPageFromSource(string sourcePath)
    {
      var productsList = Dao.GetAllProducts();
      using (var client = new WebClient())
      {
        var html = client.DownloadString(sourcePath);
        var doc = new HtmlDocument();
        doc.LoadHtml(html);
        var nodes = doc.DocumentNode.SelectNodes("//*[@id='product_list']").First().ChildNodes;
        ParseList(nodes, productsList);
      }
    }

    private static string GetBase64Image(string linkToHtml)
    {
      using (var client = new WebClient())
      {
        client.Headers.Add("user-agent", "MyRSSReader/1.0");
        var base64String = "";
        try
        {
          var html = client.DownloadString(linkToHtml);
          var doc = new HtmlDocument();
          doc.LoadHtml(html);
          var nodes = doc.DocumentNode.SelectNodes("//*[@id='bigpic']");
          if (nodes != null && nodes.Count > 0)
          {
            var imgUrl = nodes[0].Attributes["src"].Value;
            var request = (HttpWebRequest) WebRequest.Create("http://skay.ua");
            try
            {
              request = (HttpWebRequest) WebRequest.Create(imgUrl);
            }
            catch (Exception exc)
            {
              Console.Write(exc.Message + "\r\n imgUrl =" + imgUrl);
            }
            request.UserAgent = "Foo";
            request.Accept = "*/*";
            request.Headers.Add("myheader", "myheader_value");
            var response = (HttpWebResponse) request.GetResponse();

            if ((response.StatusCode == HttpStatusCode.OK ||
                 response.StatusCode == HttpStatusCode.Moved ||
                 response.StatusCode == HttpStatusCode.Redirect) &&
                response.ContentType.StartsWith("image", StringComparison.OrdinalIgnoreCase))
            {
              const string pathToImg = @"c:\Shop\NewsPic";
              var fullPath = pathToImg + "96857" + imgUrl.Substring(imgUrl.LastIndexOf('.'));
              using (var inputStream = response.GetResponseStream())
              {
                using (Stream outputStream = File.OpenWrite(fullPath))
                {
                  var buffer = new byte[4096];
                  int bytesRead;
                  do
                  {
                    // ReSharper disable once PossibleNullReferenceException
                    bytesRead = inputStream.Read(buffer, 0, buffer.Length);
                    outputStream.Write(buffer, 0, bytesRead);
                  } while (bytesRead != 0);
                }
              }

              using (var image = Image.FromFile(fullPath))
              {
                using (var m = new MemoryStream())
                {
                  image.Save(m, image.RawFormat);
                  var imageBytes = m.ToArray();
                  base64String = Convert.ToBase64String(imageBytes);
                }
              }
            }
          }
        }
        catch (Exception ex)
        {
          try
          {
            ShowException(ex.Message);
          }
          catch (Exception e)
          {
            ShowException(e.Message);
          }
        }
        return base64String;
      }
    }

    private static void ShowException(string ex)
    {
      var exception = @"C:\Shop\exceptionNewsService.txt".Replace(@"\bin\Debug", "").Replace(@"\bin\Release", "");
      var append = "\r\n" + "Time" + DateTime.UtcNow + " " + ex;
      if (!File.Exists(exception))
        File.WriteAllText(exception, append);
      File.AppendAllText(exception, append);
    }
  }
}