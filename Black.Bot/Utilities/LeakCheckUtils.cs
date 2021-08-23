using Black.Bot.DTOs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.InputFiles;

namespace Black.Bot.Utilities
{
    public class LeakCheckUtils
    {
        public static string GetNiceResultMessage(LeakCheckResult response)
        {
            var returnMessage = new StringBuilder();
            returnMessage.AppendLine($"✅ Encontrou: {response.Found}");
            returnMessage.AppendLine();

            foreach (var result in response.Result)
            {
                returnMessage.AppendLine($"🔑 *{result.Line}*");

                if (result.Sources.Length > 0)
                    returnMessage.AppendLine($" ┕🗄 *{string.Join(", ", result.Sources)}*");

                if (!string.IsNullOrEmpty(result.LastBreach))
                    returnMessage.AppendLine($" ┕📅 *{result.LastBreach}*");

                returnMessage.AppendLine();
            }

            return returnMessage.ToString();
        }

        public static Stream GetResultStream(LeakCheckResult response)
        {
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);

            foreach(var result in response.Result)
                writer.WriteLine(result.Line);

            writer.Flush();

            ms.Position = 0;
            return ms;
        }
    }
}
