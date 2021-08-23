using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using TgBotFramework.Models;

namespace TgBotFramework.Data.MongoDB
{
    public static class BotBuilderMongoExtension
    {
        public static IBotFrameworkBuilder<TContext> UseMongoDb<TContext>(this IBotFrameworkBuilder<TContext> builder, MongoUrl address) where TContext : IUpdateContext
        {
            var db = new MongoClient(address).GetDatabase(address.DatabaseName);

            var userModel = db.GetCollection<UserModel>("Framework.UserModel");
            builder.Services.AddSingleton(sp => new MongoClient(address).GetDatabase(address.DatabaseName));

            builder.Services.AddScoped<UserStateMapper<TContext>>();
            builder.UpdatePipelineSettings.Middlewares.Add(typeof(UserStateMapper<TContext>));

            return builder;
        }
    }
}