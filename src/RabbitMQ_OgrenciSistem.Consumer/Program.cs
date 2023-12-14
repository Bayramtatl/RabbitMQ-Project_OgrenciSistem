using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace RabbitMQ_OgrenciSistem.Consumer
{
  internal class Program
  {
    static void Main(string[] args)
    {
      var factory = new ConnectionFactory
      {
        HostName = "localhost",
      };
      var connection = factory.CreateConnection();
      var channel = connection.CreateModel();

      channel.QueueDeclare("student", exclusive:false, autoDelete:false);

      var consumer = new EventingBasicConsumer(channel);
      consumer.Received += Receiver;
      channel.BasicConsume(queue: "student",consumer:consumer);
      System.Console.ReadLine();

      void Receiver(object model, BasicDeliverEventArgs eventargs){
        var body  = eventargs.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);
        System.Console.WriteLine($"Received : {message}");       
      }
    }
  }
}
