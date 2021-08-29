using System.Threading.Tasks;
using TgBotFramework.Results;

namespace TgBotFramework.Attributes
{
    public interface ICommandValidationAttribute<TContext> where TContext : IUpdateContext
    {
        virtual string ErrorMessage { get { return null; } set { } }
        virtual Task<ValidationResult> CheckPermissionsAsync(TContext context)
        {
            return CheckPermissionsAsync(context, CommandBase<TContext>.ParseCommandArgs(context.Update.Message));
        }

        Task<ValidationResult> CheckPermissionsAsync(TContext context, string[] args);
        Task NotEnoughPermissions(TContext context, string errorReason);
    }
}
