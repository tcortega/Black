using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using TgBotFramework;
using TgBotFramework.WrapperExtensions;

namespace Black.Bot.Commands
{
    public class StartCommand : CommandBase<BlackBotContext>
    {
        public override async Task HandleAsync(BlackBotContext context, UpdateDelegate<BlackBotContext> next, string[] args, CancellationToken cancellationToken)
        {
            await context.Client.SendTextMessageAsync(context.Update.GetChat(), "Estou funcionando!", cancellationToken: cancellationToken);
        }
    }
}
