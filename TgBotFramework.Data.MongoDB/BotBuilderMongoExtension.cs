using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using MongoDB;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;
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