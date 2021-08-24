using Black.Bot.Models;
using MongoDB.Driver;
using System.Threading;
using System.Threading.Tasks;
using TgBotFramework.WrapperExtensions;

namespace TgBotFramework.Data.MongoDB
{
    public class UserMapper<TContext> : IUpdateHandler<TContext> where TContext : IUpdateContext
    {
        private readonly IMongoCollection<UserModel> _db;
        public UserMapper(IMongoDatabase db)
        {
            _db = db.GetCollection<UserModel>("Users");
        }

        public async Task HandleAsync(TContext context, UpdateDelegate<TContext> next, CancellationToken cancellationToken)
        {
            var userId = context.Update.GetSenderId();
            if (userId != 0)
            {
                var userModel = (await _db.FindAsync(x => x.TelegramId == userId, cancellationToken: cancellationToken)).FirstOrDefault();
                if (userModel == null)
                {
                    userModel = new UserModel { TelegramId = userId };
                    await _db.InsertOneAsync(userModel, cancellationToken: cancellationToken);
                }
            }

            await next(context, cancellationToken);
        }
    }
}