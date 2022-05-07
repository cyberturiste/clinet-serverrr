using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace ConsoleClient
{
    class Program
    {
        const int port = 8888;
        const string address = "127.0.0.1";
        static string[] stringArray = new string[1500];

        public static void SendText(object i)
        {
            TcpClient client = null;

            client = new TcpClient(address, port);

            NetworkStream stream = client.GetStream();

            byte[] data = Encoding.Unicode.GetBytes(stringArray[(int)i]);
            // отправка сообщения
            stream.Write(data, 0, data.Length);

            data = new byte[64]; // буфер для получаемых данных
            StringBuilder builder = new StringBuilder();
            int bytes = 0;
            do
            {
                bytes = stream.Read(data, 0, data.Length);
                builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                Console.WriteLine();
                Console.WriteLine(builder.ToString());
                Console.WriteLine();

            }
            while (stream.DataAvailable);

        }
        static void Main(string[] args)
        {

            Console.Write("Введите путь до входных данных: (например: (C:\\Users\\anikulin\\Desktop\\лаба 2\\text): ");

            string pathToFile = Console.ReadLine();

            string[] filePaths = Directory.GetFiles(pathToFile);

            for (int i = 0; i < filePaths.Length; i++)
            {

                StreamReader rdr = new StreamReader(filePaths[i]);

                string message = rdr.ReadToEnd();

                stringArray[i] = message;

                Console.WriteLine("Файл" + " " + filePaths[i] + " " + "Сообщение из файла: " + message);

                rdr.Close();

            }

            try
            {

                for (int i = 0; i < filePaths.Length; i++)
                {

                    Thread myThread = new Thread(new ParameterizedThreadStart(SendText));
                    myThread.Start(i);

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

            }

        }

    }
}