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



            


            var thecommand = commands.CreateCommand("fire");


      

           

            commands.CreateCommand("Start")
           .Do(async (e) =>
           {
               var channel = e.Server.FindChannels("red-skull-raiders").FirstOrDefault();
               await channel.SendMessage("Zombies are attacking");

              
                   thecommand
                   .Do(async (d) =>
                   {

                       Random rnd = new Random();
                       int Ran = rnd.Next(1, 2);


                       if (Ran == 1)
                       {
                            await channel.SendMessage("You missed " + d.User.Mention);
                           Random dam = new Random();
                           int damage = dam.Next(10, 95);
                           await channel.SendMessage("You take " + damage + " points worth of damage");
                           string[] lines = { "First line", "Second line", "Third line" };
                           System.IO.File.WriteAllLines(@"C:\Users\Public\Documents\LastDays\" + d.User.Name + ".txt", lines);
                           return;
                       }
                       else
                       {
                           await channel.SendMessage("You hit a Zombie " + d.User.Mention);
                           return;
                       }
                   });
               
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
