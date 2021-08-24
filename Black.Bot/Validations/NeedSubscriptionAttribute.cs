using Black.Bot.Models;
using MongoDB.Driver;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot;
using TgBotFramework;
using TgBotFramework.Attributes;
using TgBotFramework.Results;
using TgBotFramework.WrapperExtensions;

namespace Black.Bot.Validations
{
    public class NeedSubscriptionAttribute : CommandValidationAttribute
    {
        public override async Task<ValidationResult> CheckPermissionsAsync(IUpdateContext context, string[] args)
        {
            var senderId = context.Update.GetSenderId();

            IMongoDatabase db = context.Services.GetService(typeof(IMongoDatabase)) as IMongoDatabase;
            var userKeyCollection = db.GetCollection<UserKeyModel>("UsersKeys");

            var userKeyModel = (await userKeyCollection.FindAsync(x => x.TelegramId == senderId && !x.Expired)).FirstOrDefault();

            if (userKeyModel != null)
                return ValidationResult.FromSuccess();

            return ValidationResult.FromError("Você não tem nenhuma key ativa :(.");
        }

        public override async Task NotEnoughPermissions(IUpdateContext context, string errorReason)
        {
            await context.Client.SendTextMessageAsync(context.Update.GetChat(), errorReason);
        }
    }
}
