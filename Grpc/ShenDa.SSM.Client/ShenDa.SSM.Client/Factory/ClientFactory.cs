using Grpc.Net.Client;
using ShenDa.SSM.Common;
using static ShenDa.SSM.Grpc.SsmService;

namespace ShenDa.SSM.Client.Factory
{
    public class ClientFactory
    {
        private static  SsmServiceClient client;

        private static object obj = new object();
        public static SsmServiceClient GetClient()
        {
            lock (obj)
            {
                if (client == null)
                {
                    var serverUrl = ConfigurationHelper.Get<string>("ServerUrl");
                    var channel = GrpcChannel.ForAddress(serverUrl);
                    client = new SsmServiceClient(channel);
                }
                return client;
            }
        }

    }
}
