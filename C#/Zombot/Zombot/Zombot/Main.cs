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
            discord.UsingCommands(z =>
            {
                z.PrefixChar = ';';
                z.AllowMentionPrefix = true;
            });


            var commands = discord.GetService<CommandService>();



            

            commands.CreateCommand("start")
            .Do(async (e) =>
            {
               
                await e.Channel.SendMessage("Starting Zombies");
                await Zom(e);
            });



           // commands.CreateCommand("test")
                //.Do(async (e) =>
                //{
                    //var rules = e.Server.FindChannels("rules").FirstOrDefault();

                  //  await rules.SendMessage("i can talk on differnet channels cause i'm not retarded");
                   
                //});
            discord.ExecuteAndWait(async () =>
            {
                await discord.Connect("MzI5NDgxMDE4NzAyNTYxMjgz.DDTnvA.L0Ad9SCmqZmldTQCAVcqoS0Hti0", TokenType.Bot);
            });

            

        }
        private void log(object sender, LogMessageEventArgs e)
        {
            Console.WriteLine(e.Message);
        }

        private Task Zom(CommandEventArgs e)
        {
            discord.UsingCommands(z =>
            {
                z.PrefixChar = ';';
                z.AllowMentionPrefix = true;
            });
            var commands = discord.GetService<CommandService>();

            commands.CreateCommand("fire")
          .Do(async (b) =>
          {
              await e.Channel.SendMessage("@Red Skull Raiders Zombies are attacking!!!!");

              again:
              int zombiecout = 1;
              var msg = b.Server.FindChannels("red-skull-raiders").FirstOrDefault();
              await msg.SendMessage(b.User.Name + "Hit a zombie");
             if (zombiecout == 0)
              {
                  await msg.SendMessage("All zombies are dead");
              }
             else
              {
                  goto again;
              }
          });

        }


    }
}
