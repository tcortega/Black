using System;
using System.Threading.Tasks;
using TgBotFramework.Results;

namespace TgBotFramework.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = true)]
    public abstract class CommandValidationAttribute : Attribute, ICommandValidationAttribute<IUpdateContext>
    {
        public virtual string ErrorMessage { get { return null; } set { } }
        public virtual Task<ValidationResult> CheckPermissionsAsync(IUpdateContext context)
        {
            return CheckPermissionsAsync(context, CommandBase<IUpdateContext>.ParseCommandArgs(context.Update.Message));
        }

        public abstract Task<ValidationResult> CheckPermissionsAsync(IUpdateContext context, string[] args);
        public abstract Task NotEnoughPermissions(IUpdateContext context, string errorReason);
    }
}