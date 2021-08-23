using System.Threading.Tasks;
using Telegram.Bot;
using TgBotFramework;
using TgBotFramework.Attributes;
using TgBotFramework.Results;
using TgBotFramework.WrapperExtensions;

namespace Black.Bot.Validations
{
    public class LeakCheckFeatureAttribute : CommandValidationAttribute
    {
        public override Task<ValidationResult> CheckPermissionsAsync(IUpdateContext context, string[] args)
        {
            var options = context.Bot.Options;
            if (context.Update.GetSenderId().ToString() == options.Value.DeveloperId)
                return Task.FromResult(ValidationResult.FromSuccess());

            return Task.FromResult(ValidationResult.FromError("Esse comando é apenas para desenvolvedores."));
        }

        public override async Task NotEnoughPermissions(IUpdateContext context, string errorReason)
        {
            await context.Client.SendTextMessageAsync(context.Update.GetChat(), errorReason);
        }
    }
}
