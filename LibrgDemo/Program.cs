using System;
using System.Threading;
using Librg;

namespace LibrgDemo
{
    class Program
    {
        private static void Server()
        {
            var ctx = new Context();
            ctx.Mode = Mode.Server;
            ctx.MaxConnections = 1000;
            ctx.Init();

            ctx.Start("0.0.0.0", 7777);

            while (true)
            {
                ctx.Tick();
            }

            ctx.Free();
        }

        private static void Client()
        {
            var ctx = new Context();
            ctx.Mode = Mode.Client;
            ctx.Init();

            ctx.Start("localhost", 7777);

            while (true)
            {
                ctx.Tick();
            }

            ctx.Free();
        }

        static void Main(string[] args)
        {
            Console.WriteLine("ENet demo");

            Console.WriteLine(Native.DLL_NAME);

            Console.WriteLine(Option.Get(Option.Options.LIBRG_DEFAULT_DATA_SIZE));

            var server = new Thread(Server);
            server.Start();
            Thread.Sleep(250);
            var client = new Thread(Client);
            client.Start();

            server.Join();
            client.Join();

            Console.WriteLine("Press Enter to exit");
            Console.ReadLine();
        }
    }
}
