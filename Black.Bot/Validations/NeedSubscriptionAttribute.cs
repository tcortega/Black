using Black.Bot.Models;
using MongoDB.Driver;
using System;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using TgBotFramework;
using TgBotFramework.Attributes;
using TgBotFramework.Results;
using TgBotFramework.WrapperExtensions;

namespace Black.Bot.Validations
{
    public class NeedSubscriptionAttribute : Attribute, ICommandValidationAttribute<BlackBotContext>
    {
        public async Task<ValidationResult> CheckPermissionsAsync(BlackBotContext context, string[] args)
        {
            var newArgs = CommandBase<BlackBotContext>.ParseCommandArgs(context.Update.Message);

            var senderId = context.Update.GetSenderId();

            IMongoDatabase db = context.Services.GetService(typeof(IMongoDatabase)) as IMongoDatabase;
            var userKeyCollection = db.GetCollection<UserKeyModel>("UsersKeys");

            var userKeyModel = (await userKeyCollection.FindAsync(x => x.TelegramId == senderId && !x.Expired)).FirstOrDefault();

            if (userKeyModel != null)
                return ValidationResult.FromSuccess();

            return ValidationResult.FromError("Você não tem nenhuma key ativa :(.");
        }

        public async Task NotEnoughPermissions(BlackBotContext context, string errorReason)
        {
            await context.Client.SendTextMessageAsync(context.Update.GetChat(), errorReason);
        }
    }
}
