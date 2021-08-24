using Black.Bot.Models;
using Black.Bot.Utilities;
using Black.Bot.Validations;
using MongoDB.Driver;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using TgBotFramework;
using TgBotFramework.Attributes;
using TgBotFramework.WrapperExtensions;

namespace Black.Bot.Commands
{
    [DeveloperFeature]
    [Command("key")]
    public class KeyCommand : CommandBase<BlackBotContext>
    {
        private readonly IMongoCollection<KeyModel> _keyCollection;

        public KeyCommand(IMongoDatabase db)
        {
            _keyCollection = db.GetCollection<KeyModel>("Keys");
        }

        public override async Task HandleAsync(BlackBotContext context, UpdateDelegate<BlackBotContext> next, string[] args, CancellationToken cancellationToken)
        {
            var chat = context.Update.GetChat();

            if (args.Length < 1)
            {
                await context.Client.SendTextMessageAsync(chat, "Você deve especificar a quantidade de dias que a key terá.", cancellationToken: cancellationToken);
                return;
            }

            if (!int.TryParse(args[0], out int days) || days <= 0)
            {
                await context.Client.SendTextMessageAsync(chat, "Você deve inserir um valor inteiro númerico de dias maior que 0.", cancellationToken: cancellationToken);
                return;
            }

            var newKey = new KeyModel(days);
            await _keyCollection.InsertOneAsync(newKey, cancellationToken: cancellationToken);

            await context.Client.SendTextMessageAsync(chat, $"Key gerada com sucesso: {newKey.Key}", cancellationToken: cancellationToken);
        }
    }
}
