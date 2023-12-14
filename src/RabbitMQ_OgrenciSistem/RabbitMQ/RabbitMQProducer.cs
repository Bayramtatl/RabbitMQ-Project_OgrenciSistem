using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;
using System.Text;
using System.Text.Json.Serialization;

namespace RabbitMQ_OgrenciSistem.RabbitMQ
{
  public class RabbitMQProducer : IRabbitMQProducer
  {
    private readonly IConfiguration _configuration;

    public RabbitMQProducer( IConfiguration configuration)
    {
      _configuration = configuration;
    }

    public void SendMessage<T>(T message)
    {
      var connectionHost = _configuration.GetSection("RabbitMQConfiguration:Connection").Value;
      var factory = new ConnectionFactory
      {
        HostName = connectionHost,
      };
      var connection = factory.CreateConnection();
      using var channel = connection.CreateModel();
      channel.QueueDeclare("student",exclusive:false,autoDelete:false); //exclusive girmezsek özel mesaj oalrak gider ve portalda gözükmez.
      var json = JsonConvert.SerializeObject(message);
      var body = Encoding.UTF8.GetBytes(json); // byte dizisine çevirdik.

      channel.BasicPublish(exchange:"",routingKey:"student",body:body);
    }
  }
}
