using MongoDB.Driver;
using TgBotFramework;

namespace Black.Bot
{
    public class BlackBotContext : BaseUpdateContext
    {
        public IMongoDatabase Db { get; set; }

        public BlackBotContext(IMongoDatabase db)
        {
            Db = db;
        }
    }
}
