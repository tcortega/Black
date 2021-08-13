using Microsoft.Extensions.Options;
using System;
using TgBotFramework;

namespace Black.Bot
{
    public class BlackBot : BaseBot
    {
        public BlackBot(IOptions<BotSettings> options) : base(options)
        {
        }
    }
}
