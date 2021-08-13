using System.Threading.Tasks;
using TgBotFramework.Results;

namespace TgBotFramework.Attributes
{
    public interface ICommandValidationAttribute<TContext> where TContext : IUpdateContext
    {
        string ErrorMessage { get; }
        Task<ValidationResult> CheckPermissionsAsync(TContext context);
        Task<ValidationResult> CheckPermissionsAsync(TContext context, string[] args);
        Task NotEnoughPermissions(TContext context, string errorReason);
    }
}
