namespace RabbitMQ_OgrenciSistem.RabbitMQ
{
  public interface IRabbitMQProducer
  {
    public void SendMessage<T>(T message);
  }
}
