using StrawberryShake.Transport.WebSockets;
using TransactionsGui;

namespace TransactionsGraphQLClient
{
    // Not working because WebService endpoint of Hasura Cloud Instance cannot be found.
    public class CustomConnectionInterceptor
        : ISocketConnectionInterceptor
    {
        public ValueTask<object?> CreateConnectionInitPayload(
            ISocketProtocol protocol,
            CancellationToken cancellationToken)
        {
            return new ValueTask<object?>(
                new Dictionary<string, string> { [Const.AuthHeaderKey] = Const.AuthHeaderValue });
        }
    }
}
