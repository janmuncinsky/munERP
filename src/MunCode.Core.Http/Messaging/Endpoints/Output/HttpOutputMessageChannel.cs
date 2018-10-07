namespace MunCode.Core.Messaging.Endpoints.Output
{
    using System.Net.Http;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Options;

    using MunCode.Core.Guards;
    using MunCode.Core.Messaging;
    using MunCode.Core.Messaging.Messages;

    public class HttpOutputMessageChannel : IOutputMessageChannel
    {
        private readonly HttpClient client;
        private readonly string baseUri;

        public HttpOutputMessageChannel(HttpClient client, IOptions<HttpMessageBusConfig> config)
        {
            Guard.NotNull(config, nameof(config));
            Guard.NotNull(client, nameof(client));
            this.client = client;
            this.baseUri = config.Value.BaseUri;
        }

        public Task Send<TCommand>(SendContext<TCommand> context)
            where TCommand : class, ICommand
        {
            return this.Post<TCommand, EmptyResponse>(context);
        }

        public Task Publish<TEvent>(SendContext<TEvent> context)
            where TEvent : class, IEvent
        {
            return this.Post<TEvent, EmptyResponse>(context);
        }

        public Task<TResponse> Request<TRequest, TResponse>(SendContext<TRequest> context)
            where TRequest : class, IRequest<TResponse> 
            where TResponse : class
        {
            return this.Post<TRequest, TResponse>(context);
        }

        public Task Respond<TResponse>(SendContext<TResponse> context)
            where TResponse : class
        {
            return this.Post<TResponse, EmptyResponse>(context);
        }

        private async Task<TResponse> Post<TRequest, TResponse>(SendContext<TRequest> context)
        {
            Guard.NotNull(context, nameof(context));
            var requestUri = $"{this.baseUri}/{typeof(TRequest).Name}";
            using (var httpResponse = await this.client.PostAsJsonAsync(requestUri, context.Message))
            {
                httpResponse.EnsureSuccessStatusCode();
                return await httpResponse.Content.ReadAsAsync<TResponse>();
            }
        }
    }
}