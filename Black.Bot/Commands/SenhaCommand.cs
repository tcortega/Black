using Black.Bot.Services;
using Black.Bot.Utilities;
using Black.Bot.Validations;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InputFiles;
using TgBotFramework;
using TgBotFramework.Attributes;
using TgBotFramework.WrapperExtensions;

namespace Black.Bot.Commands
{
    [NeedSubscription]
    [Command("senha")]
    public class SenhaCommand : CommandBase<BlackBotContext>
    {
        private readonly LeakCheckService _leakCheckService;
        public SenhaCommand(LeakCheckService leakCheckService)
        {
            _leakCheckService = leakCheckService;
        }

        public override async Task HandleAsync(BlackBotContext context, UpdateDelegate<BlackBotContext> next, string[] args, CancellationToken cancellationToken)
        {
            if (args.Length < 1)
            {
                await context.Client.SendTextMessageAsync(context.Update.GetChat(), "Você deve especificar o email ou usuário que deseja buscar.", cancellationToken: cancellationToken);
                return;
            }

            var data = args[0];
            var response = await _leakCheckService.Lookup(data, "pass_email");
            if (response.Success)
            {
                if (response.Result.Length <= 15)
                {
                    var niceMessage = LeakCheckUtils.GetNiceResultMessage(response);
                    await context.Client.SendTextMessageAsync(context.Update.GetChat(), niceMessage, cancellationToken: cancellationToken, replyToMessageId: context.Update.Message.MessageId, parseMode: ParseMode.Markdown);
                }
                else
                {
                    using var resultStream = LeakCheckUtils.GetResultStream(response);
                    var file = new InputOnlineFile(resultStream, $"{data}.txt");
                    var caption = $"✅ Encontrou: {response.Found}";

                    await context.Client.SendDocumentAsync(context.Update.GetChat(), file, caption: caption, cancellationToken: cancellationToken, replyToMessageId: context.Update.Message.MessageId);
                }

                return;
            }

            if (response.NotFound)
                await context.Client.SendTextMessageAsync(context.Update.GetChat(), "*🎩 SEM RESULTADOS 🎩*", cancellationToken: cancellationToken, replyToMessageId: context.Update.Message.MessageId, parseMode: ParseMode.Markdown);
            else
                await context.Client.SendTextMessageAsync(context.Update.GetChat(), "*🎩 OCORREU UM ERRO, CONTATE O ADM 🎩*", cancellationToken: cancellationToken, replyToMessageId: context.Update.Message.MessageId, parseMode: ParseMode.Markdown);
        }
    }
}
