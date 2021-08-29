using Black.Bot.Models;
using MongoDB.Driver;
using System;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using TgBotFramework.Attributes;
using TgBotFramework.Results;
using TgBotFramework.WrapperExtensions;

namespace Black.Bot.Validations
{
    public class NeedSubscriptionAttribute : Attribute, ICommandValidationAttribute<BlackBotContext>
    {
        public async Task<ValidationResult> CheckPermissionsAsync(BlackBotContext context, string[] args)
        {
            var senderId = context.Update.GetSenderId();

            var userKeyCollection = context.Db.GetCollection<UserKeyModel>("UsersKeys");

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
