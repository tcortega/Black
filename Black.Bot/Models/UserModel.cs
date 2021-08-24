using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Black.Bot.Models
{
    public class UserModel : BaseModel
    {
        [Required]
        public long TelegramId { get; set; }
    }
}
