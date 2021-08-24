using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Black.Bot.Utilities
{
    public class KeyUtils
    {
        private static Random s_random = new Random();

        public static string RandomString(int length = 12)
        {
            const string chars = "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[s_random.Next(s.Length)]).ToArray());
        }
    }
}
