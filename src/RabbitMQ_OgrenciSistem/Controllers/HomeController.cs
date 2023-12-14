using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client.Events;
using RabbitMQ_OgrenciSistem.Models;
using RabbitMQ_OgrenciSistem.RabbitMQ;
using System.Diagnostics;
using System.Text;

namespace RabbitMQ_OgrenciSistem.Controllers
{
  public class HomeController : Controller
  {
    private readonly ILogger<HomeController> _logger;
    private readonly IRabbitMQProducer _rabbitMQ;

    public HomeController(ILogger<HomeController> logger, IRabbitMQProducer rabbitMQProducer)
    {
      _logger = logger;
      _rabbitMQ = rabbitMQProducer;
    }

    public IActionResult Index()
    {
      var ogr = new Ogrenci { FirstName = "Bayram", LastName = "Tatlı" };
      _rabbitMQ.SendMessage<Ogrenci>(ogr);
      return View();
    }
    void Receiver(object model, BasicDeliverEventArgs eventargs)
    {
    }

    public IActionResult Ekle(Ogrenci ogr)
    {
      _rabbitMQ.SendMessage<Ogrenci>(ogr);
      return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
  }
}