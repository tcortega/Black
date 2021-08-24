using Black.Bot.Models;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Black.Bot.Services
{
    public class KeyUpdaterService : BackgroundService
    {
        private readonly IMongoCollection<UserKeyModel> _db;

        public KeyUpdaterService(IMongoDatabase db)
        {
            _db = db.GetCollection<UserKeyModel>("UsersKeys");
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _ = new Timer(UpdateKeys, null, TimeSpan.Zero, TimeSpan.FromMinutes(3));

            return Task.CompletedTask;
        }

        public async void UpdateKeys(object state) => await UpdateKeysAsync();
        public async Task UpdateKeysAsync()
        {
            var userKeys = (await _db.FindAsync(x => !x.Expired)).ToList();

            foreach(var userKey in userKeys)
            {
                if (DateTimeOffset.Now > userKey.ExpireDate)
                {
                    userKey.Expired = true;

                    await _db.ReplaceOneAsync(x => x.Id == userKey.Id, userKey);
                }
            }
        }
    }
}
