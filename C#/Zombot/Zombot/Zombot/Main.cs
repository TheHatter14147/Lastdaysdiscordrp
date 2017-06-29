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




            commands.CreateCommand("register")
             .Do(async (r) =>
            {
                var register = r.Server.FindChannels("bot_register").FirstOrDefault();
                await register.SendMessage("Account created");
                var channel = r.Server.FindChannels("red-skull-raiders").FirstOrDefault();
                await channel.SendMessage("" + r.User.Mention + " Has joined the team ");

                string[] lines = {"100"};
                System.IO.File.WriteAllLines(@"C:\Users\Public\Documents\LastDays\" + r.User.Name + ".txt", lines);

            });
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
                           string text = System.IO.File.ReadAllText(@"C:\Users\Public\Documents\LastDays\" + d.User.Name + ".txt");
                           System.Console.WriteLine("Contents of WriteText.txt = {0}", text);
                           int x = Int32.Parse(text);
                           x = x - damage;


                           text = x.ToString();


                           await channel.SendMessage("You current health is: " + x + "  " + d.User.Mention);

                           string text1 = text;
                           System.IO.File.WriteAllText(@"C:\Users\Public\Documents\LastDays\" + d.User.Name + ".txt", text1);

                           if (x < 0)
                           {
                               await channel.SendMessage("YOU HAVE DIE" + d.User.Mention);
                           }


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
