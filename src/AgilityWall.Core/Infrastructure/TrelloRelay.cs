﻿using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AgilityWall.Core.Messages;
using Caliburn.Micro;
using PortableTrello.Client;
using PortableTrello.Client.Requests;

namespace AgilityWall.Core.Infrastructure
{
    public class TrelloRelay : IHandleWithTask<object>
    {
        private readonly ITrelloClient _client;
        private readonly IEventAggregator _eventAggregator;

        public TrelloRelay(ITrelloClient client, IEventAggregator eventAggregator)
        {
            _client = client;
            _eventAggregator = eventAggregator;
        }

        public async Task Handle(object message)
        {
            if (message == null) return;

            var parameters = message.GetType()
                .GetTypeInfo()
                .ImplementedInterfaces
                .Where(i => i.GetTypeInfo().IsGenericType && i.GetGenericTypeDefinition() == typeof(ITrelloRequest<,>))
                .SelectMany(i => i.GenericTypeArguments)
                .ToArray();

            if (parameters.Any())
            {
                var request = (dynamic)message;
                object response = await _client.ExecuteRequest(request);
                var responseType = typeof(ModelResponse<,>).MakeGenericType(parameters);
                object instance = Activator.CreateInstance(responseType, request.Resource, response);
                _eventAggregator.Publish(instance, Execute.BeginOnUIThread);
            }
        }
    }
}