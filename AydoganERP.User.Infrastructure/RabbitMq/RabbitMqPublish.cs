using AydoganERP.Base.Application.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace AydoganERP.User.Infrastructure.RabbitMq;

public class RabbitMqPublish : BaseConnection
{
    //Option Monitor allow appsetting change
    public RabbitMqPublish(IOptionsMonitor<RabbitMqOptions> options)
        : base(options.CurrentValue)
    {
    }

    public void Publish(string queueName, object data)
    {
        using (IModel channel = _connection.CreateModel())
        {
            //Kuyruk oluşturma
            channel.QueueDeclare(queue: queueName,
                durable: false, //Data fiziksel olarak mı yoksa memoryde mi tutulsun
                exclusive: false, //Başka connectionlarda bu kuyruğa ulaşabilsin mi
                autoDelete: true, //Eğer kuyruktaki son mesaj ulaştığında kuyruğun silinmesini istiyorsak kullanılır.
                arguments: null);//Exchangelere verilecek olan parametreler tanımlamak için kullanılır.

            string message = JsonConvert.SerializeObject(data);
            var body = Encoding.UTF8.GetBytes(message);

            //Queue ya atmak için kullanılır.
            channel.BasicPublish(exchange: "",//mesajın alınıp bir veya birden fazla queue ya konmasını sağlıyor.
                routingKey: queueName, //Hangi queue ya atanacak.
                body: body);//Mesajun içeriği
        }
    }

}
