using System;
using System.Threading.Tasks;
using Definition;
using Grpc.Core;
using MagicOnion.Client;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            GrpcEnvironment.SetLogger(new Grpc.Core.Logging.ConsoleLogger());
            
            Task.Run(() =>
            {
                ClientImpl();
            });

            Console.ReadLine();
        }

        static async void ClientImpl()
        {
            // standard gRPC channel
            var channel = new Channel("localhost", 12345, ChannelCredentials.Insecure);

            // create MagicOnion dynamic client proxy
            var client = MagicOnionClient.Create<IMyFirstService>(channel);
            // call method.
            var result = await client.SumAsync(100, 200);
            Console.WriteLine("Client Received:" + result);
        }
    }
}
