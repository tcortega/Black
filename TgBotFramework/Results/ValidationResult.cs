namespace TgBotFramework.Results
{
    public class ValidationResult : IResult
    {
        public string ErrorReason { get; }
        public bool IsSuccess => string.IsNullOrEmpty(ErrorReason);

        protected ValidationResult(string errorReason)
        {
            ErrorReason = errorReason;
        }

        public static ValidationResult FromSuccess()
            => new ValidationResult(null);
        public static ValidationResult FromError(string reason)
            => new ValidationResult(reason);

        public override string ToString() => IsSuccess ? "Success" : $"Error: {ErrorReason}";
    }
}
