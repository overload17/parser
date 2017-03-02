using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
  public class Product
  {
    public int ID { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Link { get; set; }
    public string ImageBase64 { get; set; }

    public Product(int id, string title, string description, string link, string imageBase64)
    {
      ID = id;
      Title = title;
      Description = description;
      Link = link;
      ImageBase64 = imageBase64;
    }
  }
}