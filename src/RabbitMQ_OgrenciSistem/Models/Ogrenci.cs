namespace RabbitMQ_OgrenciSistem.Models
{
  public class Ogrenci
  {
    public Ogrenci()
    {
      IsReceived = false;
    }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public bool IsReceived { get; set; }

  }
}
