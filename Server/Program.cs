using System;
using System.Threading.Tasks;
using Definition;
using Grpc.Core;
using MagicOnion.Client;
using MagicOnion.Server;

namespace Server
{
    public class Program
    {
        static void Main(string[] args)
        {
            GrpcEnvironment.SetLogger(new Grpc.Core.Logging.ConsoleLogger());

            var service = MagicOnionEngine.BuildServerServiceDefinition(isReturnExceptionStackTraceInErrorDetail: true);

            var server = new global::Grpc.Core.Server
            {
                Services = { service },
                Ports = { new ServerPort("localhost", 12345, ServerCredentials.Insecure) }
            };
        
            // launch gRPC Server.
            server.Start();
            
            // sample, launch server/client in same app.
            Task.Run(() =>
            {
                ClientImpl();
            });
            
            Console.ReadLine();
        }
        
        // Blank, used by next section 
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
