using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.API;
using Discord.Commands;

namespace Zombot
{
    class Main
    {
        DiscordClient discord;

        public Main()
        {
            discord = new DiscordClient(z =>
            {
                z.LogLevel = LogSeverity.Info;
                z.LogHandler = log;
            });

            discord.ExecuteAndWait(async () =>
            {
                await discord.Connect("MzI5NDgxMDE4NzAyNTYxMjgz.DDTnvA.L0Ad9SCmqZmldTQCAVcqoS0Hti0", TokenType.Bot);
            });



        }
        private void log(object sender, LogMessageEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
