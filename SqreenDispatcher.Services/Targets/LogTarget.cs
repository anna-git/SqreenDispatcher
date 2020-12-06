using Microsoft.Extensions.Logging;
using SqreenDispatcher.Services.Model;
using System;
using System.Collections.Generic;

namespace SqreenDispatcher.Services.Targets
{
    public class LogTarget : ITarget
    {
        private readonly ILogger<SqreenMessage> _logger;

        public LogTarget(ILogger<SqreenMessage> logger) => _logger = logger;
        public void Notify(IEnumerable<SqreenMessage> messages)
        {
            foreach (var message in messages)
            {
                _logger.LogWarning($"[{message.Type}] Alert received at {message.DateCreated}: {message.Message.HumanizedDescription}. \n Api version: {message.ApiVersion}. \n Event category {message.Message?.EventCategory} \n Message id:{message.Message?.Id}");

            }
        }
    }
}
