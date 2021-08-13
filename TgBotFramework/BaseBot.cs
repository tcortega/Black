using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace TgBotFramework
{
    public class BaseBot
    {
        public TelegramBotClient Client { get; }
        public string Username { get; }

        public BaseBot(IOptions<BotSettings> options)
        {
            Client = new TelegramBotClient(options.Value.ApiToken, baseUrl: options.Value.BaseUrl);
            Username = options.Value.Username;
        }

        public bool CanHandleCommand(string commandName, Message message)
        {
            if (string.IsNullOrWhiteSpace(commandName))
                throw new ArgumentException("Invalid command name", nameof(commandName));
            if (commandName.StartsWith("/"))
                throw new ArgumentException("Command name must not start with '/'.", nameof(commandName));

            if (message is null)
                return false;

            {
                bool isTextMessage = message.Text != null;
                if (!isTextMessage)
                    return false;
            }

            {
                bool isCommand = message.Entities?.FirstOrDefault()?.Type == MessageEntityType.BotCommand;
                if (!isCommand)
                    return false;
            }

            return Regex.IsMatch(
                message.EntityValues.First(),
                $@"^/{commandName}(?:@{Username})?$",
                RegexOptions.IgnoreCase
            );
        }
    }
}