using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TgBotFramework;

namespace Black.Bot.Handlers
{
    public class MessageHandler : IUpdateHandler<BlackBotContext>
    {
        public async Task HandleAsync(BlackBotContext context, UpdateDelegate<BlackBotContext> next, CancellationToken cancellationToken)
        {
            // code
        }
    }
}
