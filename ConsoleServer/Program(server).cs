using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ConsoleServer
{
    class Program
    {
        static int count = 0;
        const int port = 8888;
        static TcpListener listener;

        static void Main(string[] args)
        {
            Console.Write("Для того что бы сервер начал прослушивание введите N=");

            string N = Console.ReadLine();

            int x = Int32.Parse(N);

            ThreadPool.SetMaxThreads(x, x);

            ThreadPool.SetMinThreads(2, 2);
            Metod();
        }
        static int n = 1;
        static void Metod()
        {
            try
            {
                listener = new TcpListener(IPAddress.Parse("127.0.0.1"), port);
                listener.Start();
                Console.WriteLine("Ожидание подключений...");
                Console.WriteLine();
                n++;
                while (true)
                {
                    TcpClient client = listener.AcceptTcpClient();
                    ClientObject clientObject = new ClientObject(client);

                    

                    ThreadPool.QueueUserWorkItem(new WaitCallback(clientObject.Process));

                    

                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {
                Console.ReadLine();
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.ReadLine();
                if (listener != null)
                    listener.Stop();

            }

        }
    }
}