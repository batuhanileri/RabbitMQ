//Bağlantı Oluşturma
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

ConnectionFactory connectionFactory = new();

connectionFactory.Uri = new("amqps://rbqcvqzw:TDmVtIYjrYvoGNwdhavdhgFXOHxZ_Crr@woodpecker.rmq.cloudamqp.com/rbqcvqzw");

//Bağlantıyı aktifleştirme ve kanal açma

using IConnection connection = connectionFactory.CreateConnection();
using IModel channel = connection.CreateModel();


//Queue oluşturma
channel.QueueDeclare(queue: "example-queue", exclusive: false); //Consumerdaki kuyruk publisher ile
                                                                //birebir aynı yapılandırmada tanımlanmalıdır

//Queue'den Mesaj okuma

EventingBasicConsumer consumer = new(channel);
channel.BasicConsume(queue: "example-queue",autoAck:false, consumer);

consumer.Received += (sender, e) =>
{
    //Kuyruğa gelen mesajın işlendiği yerdir
    //e.Body : Kuyruktaki mesajın verisini getirecektir.
    //e.Body.Span veya e.Body.ToArray() : Kuyruktaki mesajın byte verisini getirecektir.
    Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));

    channel.BasicAck(deliveryTag: e.DeliveryTag, multiple: false); // multiple: false sadece gelen mesaja dair silme işlemini yapcaktır. Silmeyi consumera bırakacaktır.
                                                                   // eğer true dersek kuyruktaki tüm hepsini silecektir.
};

Console.Read();
