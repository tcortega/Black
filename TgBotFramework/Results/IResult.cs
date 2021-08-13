namespace TgBotFramework.Results
{
    public interface IResult
    {
        string ErrorReason { get; }
        bool IsSuccess { get; }
    }
}
