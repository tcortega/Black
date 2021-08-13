using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using TgBotFramework;

namespace Black.Bot.Handlers
{
    public class GlobalExceptionHandler : IUpdateHandler<BlackBotContext>
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async Task HandleAsync(BlackBotContext context, UpdateDelegate<BlackBotContext> next, CancellationToken cancellationToken)
        {
            try
            {
                await next(context, cancellationToken);
                _logger.LogInformation("Update {0}, no errors", context.Update.Id);
            }
            catch (Exception e)
            {
                _logger.LogInformation("Update {0}, has errors {1}", context.Update.Id, e);
            }
        }
    }
}
