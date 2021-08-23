using MongoDB.Driver;
using TgBotFramework;

namespace Black.Bot
{
    public class BlackBotContext : BaseUpdateContext
    {
       public IMongoDatabase DB { get; set; }

        public BlackBotContext(IMongoDatabase db)
        {
            DB = db;
        }
    }
}
