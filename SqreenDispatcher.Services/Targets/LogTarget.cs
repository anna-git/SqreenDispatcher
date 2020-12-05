using Microsoft.Extensions.Logging;
using SqreenDispatcher.Services.Model;
using System;

namespace SqreenDispatcher.Services.Targets
{
    public class LogTarget : ITarget
    {
        private readonly ILogger<SqreenMessage> _logger;

        public LogTarget(ILogger<SqreenMessage> logger) => _logger = logger;
        public void Notify(SqreenMessage[] messages)
        {
            foreach (var message in messages)
            {
                _logger.LogWarning($"[{message.Type}] Alert received at {message.DateCreated}: {message.Message}. \n Api version is {message.ApiVersion}.");

            }
        }
    }
}
