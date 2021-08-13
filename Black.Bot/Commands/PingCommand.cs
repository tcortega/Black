using Black.Bot.Validations;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using TgBotFramework;
using TgBotFramework.Attributes;
using TgBotFramework.WrapperExtensions;

namespace Black.Bot.Commands
{
    [DeveloperFeature]
    [Command("ping")]
    public class PingCommand : CommandBase<BlackBotContext>
    {
        public override async Task HandleAsync(BlackBotContext context, UpdateDelegate<BlackBotContext> next, string[] args, CancellationToken cancellationToken)
        {
            await context.Client.SendTextMessageAsync(context.Update.GetChat(), "Pong!", cancellationToken: cancellationToken);
        }
    }
}
