using System;
using System.ComponentModel.DataAnnotations;

namespace Black.Bot.Models
{
    public class UserKeyModel : BaseModel
    {
        [Required]
        public long TelegramId { get; set; }

        [Required]
        public string KeyId { get; set; }

        [Required]
        public DateTimeOffset DateUsed { get; set; }

        [Required]
        public DateTimeOffset ExpireDate { get; set; }

        [Required]
        public bool Expired { get; set; }

        public UserKeyModel(long senderId, string keyId, DateTimeOffset dateUsed, DateTimeOffset expireDate)
        {
            TelegramId = senderId;
            KeyId = keyId;
            DateUsed = dateUsed;
            ExpireDate = expireDate;
            Expired = false;
        }
    }
}
