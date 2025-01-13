using RabbitMQ.Client;
using System.Text;

// Bağlantı oluşturma
ConnectionFactory factory = new()
{
    Uri = new Uri("amqps://rbqcvqzw:TDmVtIYjrYvoGNwdhavdhgFXOHxZ_Crr@woodpecker.rmq.cloudamqp.com/rbqcvqzw")
};

// Bağlantıyı aktifleştirme ve kanal açma
using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

//Queue Oluşturma
channel.QueueDeclare(queue: "example-queue",exclusive:false);

//Queue Mesaj gönderme

//RabbitMQ kuyruğa atacağı mesajları byte türünden kabul etmektedir. Haliyle mesajları byte dönüştürmemiz gerekmektedir.

for (int i = 0; i < 100; i++)
{
    byte[] message = Encoding.UTF8.GetBytes("Merhaba" + i);
    channel.BasicPublish(exchange: "", routingKey: "example-queue", body: message);

}


Console.Read();