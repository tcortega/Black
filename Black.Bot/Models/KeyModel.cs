using Black.Bot.Utilities;
using System.ComponentModel.DataAnnotations;

namespace Black.Bot.Models
{
    public class KeyModel : BaseModel
    {

        [Required]
        public string Key  { get; set; }

        [Required]
        public int Duration  { get; set; }

        [Required]
        public bool Used  { get; set; }

        public KeyModel(int duration)
        {
            Key = KeyUtils.RandomString();
            Duration = duration;
            Used = false;
        }
    }
}
