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
    public class DeveloperFeatureAttribute : Attribute, ICommandValidationAttribute<BlackBotContext>
    {
        public Task<ValidationResult> CheckPermissionsAsync(BlackBotContext context, string[] args)
        {
            var options = context.Bot.Options;
            var senderId = context.Update.GetSenderId().ToString();

            if (options.Value.DevelopersId.Contains(senderId))
                return Task.FromResult(ValidationResult.FromSuccess());

            return Task.FromResult(ValidationResult.FromError("Esse comando é apenas para desenvolvedores."));
        }

        public async Task NotEnoughPermissions(BlackBotContext context, string errorReason)
        {
            await context.Client.SendTextMessageAsync(context.Update.GetChat(), errorReason);
        }
    }
}
