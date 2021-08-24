using Black.Bot.Models;
using MongoDB.Driver;
using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using TgBotFramework;
using TgBotFramework.Attributes;
using TgBotFramework.WrapperExtensions;

namespace Black.Bot.Commands
{
    [Command("ativar")]
    public class AtivarCommand : CommandBase<BlackBotContext>
    {
        private readonly IMongoCollection<KeyModel> _keyCollection;
        private readonly IMongoCollection<UserKeyModel> _userKeyCollection;

        public AtivarCommand(IMongoDatabase db)
        {
            _keyCollection = db.GetCollection<KeyModel>("Keys");
            _userKeyCollection = db.GetCollection<UserKeyModel>("UsersKeys");
        }

        public override async Task HandleAsync(BlackBotContext context, UpdateDelegate<BlackBotContext> next, string[] args, CancellationToken cancellationToken)
        {
            var chat = context.Update.GetChat();
            if (args.Length < 1)
            {
                await context.Client.SendTextMessageAsync(chat, "Você deve especificar a key que irá ativar. \n\nExemplo: /key *Xw7d7Oe1mn*", cancellationToken: cancellationToken, parseMode: ParseMode.Markdown);
                return;
            }

            var key = args[0];

            var keyModel = (await _keyCollection.FindAsync(x => x.Key == key)).FirstOrDefault();
            if (keyModel == null)
            {
                await context.Client.SendTextMessageAsync(chat, "Essa key não existe, tem certeza que digitou certo?", cancellationToken: cancellationToken);
                return;
            }

            // Key activation process
            await ProcessKey(context, keyModel, cancellationToken);

            await context.Client.SendTextMessageAsync(chat, "Key ativada com sucesso! Pode usar o bot :)", cancellationToken: cancellationToken);
        }

        private async Task ProcessKey(BlackBotContext context, KeyModel key, CancellationToken cancellationToken)
        {
            var senderId = context.Update.GetSenderId();

            await SaveUserKeyAsync(senderId, key, cancellationToken);
            await DisableActivatedKey(key, cancellationToken);

            return;
        }

        private async Task SaveUserKeyAsync(long senderId, KeyModel key, CancellationToken cancellationToken)
        {
            var userKeyModel = (await _userKeyCollection.FindAsync(x => x.TelegramId == senderId && !x.Expired, cancellationToken: cancellationToken)).FirstOrDefault();

            var expireDate = DateTimeOffset.Now.AddDays(key.Duration);
            if (userKeyModel != null)
            {
                userKeyModel.Expired = true;
                await _userKeyCollection.FindOneAndReplaceAsync(x => x.Id == userKeyModel.Id, userKeyModel, cancellationToken: cancellationToken);

                expireDate = userKeyModel.ExpireDate.AddDays(key.Duration);
            }

            var userKey = new UserKeyModel(senderId, key.Id, DateTimeOffset.Now, expireDate);

            await _userKeyCollection.InsertOneAsync(userKey, cancellationToken: cancellationToken);
        }

        private async Task DisableActivatedKey(KeyModel key, CancellationToken cancellationToken)
        {
            key.Used = true;
            await _keyCollection.FindOneAndReplaceAsync(x => x.Id == key.Id, key, cancellationToken: cancellationToken);
        }
    }
}
