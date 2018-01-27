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

            Data mydata = new Data();
            mydata.WriteFloat64(2525.5f);
            mydata.WriteInt8(2);
            mydata.WriteUInt32(2424);
            Console.WriteLine(mydata.ReadFloat64());
            Console.WriteLine(mydata.ReadInt8());
            Console.WriteLine(mydata.ReadUInt32());

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
            Console.WriteLine(Native.DLL_NAME);

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
