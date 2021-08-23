using Microsoft.Extensions.Options;
using TgBotFramework;

namespace Black.Bot
{
    public class BlackBot : BaseBot
    {
        public BlackBot(IOptions<BlackBotSettings> options) : base(options)
        {
        }
    }
}
