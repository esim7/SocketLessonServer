using System;
using System.Net;
using System.Net.Sockets;

namespace SocketServer
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
            {
                var localIP = IPAddress.Parse("127.0.0.1");
                var port = 8080;
                var endPoint = new IPEndPoint(localIP, port);

                socket.Bind(endPoint);
                socket.Listen(5); // максимальное число соединений в ожидающих в очереди

                Console.WriteLine($"Приложение слушает порт {port}.");
                while(true)
                {
                    var incomingSocket = socket.Accept(); //получаем входящее соединение и главный поток заблокирован пока не получим ссобщение

                    Console.WriteLine($"Получено входящее сообщение.");
                    while (incomingSocket.Available > 0)
                    {
                        var buffer = new byte[incomingSocket.Available];
                        incomingSocket.Receive(buffer);
                        //incomingSocket.Send(); // отправка

                        Console.WriteLine(System.Text.Encoding.UTF8.GetString(buffer));
                    }
                    incomingSocket.Close();
                    Console.WriteLine($"Входящее соединение закрыто.");
                }
                

                
            }

        }
    }
}
