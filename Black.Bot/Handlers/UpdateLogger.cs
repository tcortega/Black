using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TgBotFramework;
using TgBotFramework.WrapperExtensions;

namespace Black.Bot.Handler
{
    public class UpdateLogger : IUpdateHandler<BlackBotContext>
    {
        private readonly ILogger<UpdateLogger> _logger;

        public UpdateLogger(ILogger<UpdateLogger> logger)
        {
            _logger = logger;
        }

        public async Task HandleAsync(BlackBotContext context, UpdateDelegate<BlackBotContext> next, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Update {0}, contents:\n{1}", context.Update.Id, context.Update.ToJsonString());
            await next(context, cancellationToken);
        }
    }
}
