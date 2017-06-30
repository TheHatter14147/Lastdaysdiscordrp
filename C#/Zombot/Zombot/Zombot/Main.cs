using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.API;
using Discord.Commands;
using System.Diagnostics;

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




            bool commandbool;

            var fire = commands.CreateCommand("fire");

            var firesecond = commands.CreateCommand("fire2");
            commands.CreateCommand("shutdown")
                .Do (async (q) =>
                { await discord.Disconnect(); });

                commands.CreateCommand("end attack")
                .Do(async (e) =>
                {
                    var channel = e.Server.FindChannels("red-skull-raiders").FirstOrDefault();

                    await channel.SendMessage("Attack has ended");

                   commandbool = false;


                });

            commands.CreateCommand("register")
             .Do(async (r) =>
            {
                var register = r.Server.FindChannels("bot_register").FirstOrDefault();
                await register.SendMessage("Account created");
                var channel = r.Server.FindChannels("red-skull-raiders").FirstOrDefault();
                await channel.SendMessage("" + r.User.Mention + " Has joined the team ");

                string[] lines = {"100"};
                System.IO.File.WriteAllLines(@"C:\Users\Public\Documents\LastDays\" + r.User.Name + ".txt", lines);

                string[] a = { "20" };
                System.IO.File.WriteAllLines(@"C:\Users\Public\Documents\LastDays\" + r.User.Name + "Primary.txt", a);

                string[] b = { "20" };
                System.IO.File.WriteAllLines(@"C:\Users\Public\Documents\LastDays\" + r.User.Name + "Sec.txt", b);
            });
            commands.CreateCommand("Start")
           .Do(async (e) =>
           {
               var channel = e.Server.FindChannels("red-skull-raiders").FirstOrDefault();
               await channel.SendMessage("Zombies are attacking");

               commandbool = true;

               commands.CreateCommand("stab")
               .Do(async (s) =>
               {
                   if (commandbool)
                   {
                       Random rnd = new Random();
                       int Ran = rnd.Next(1, 10);

                       if (Ran == 1)
                       {
                           await channel.SendMessage("You Stabed a Zombie " + s.User.Mention);
                           return;
                       }
                       else
                       {
                           await channel.SendMessage("You failed " + s.User.Mention);

                           Random dam = new Random();
                           int damage = dam.Next(90, 100);
                           await channel.SendMessage("You take " + damage + " points worth of damage");
                           string text = System.IO.File.ReadAllText(@"C:\Users\Public\Documents\LastDays\" + s.User.Name + ".txt");
                           System.Console.WriteLine("Contents of WriteText.txt = {0}", text);
                           int x = Int32.Parse(text);
                           x = x - damage;


                           text = x.ToString();


                           await channel.SendMessage("You current health is: " + x + "  " + s.User.Mention);

                           string text1 = text;
                           System.IO.File.WriteAllText(@"C:\Users\Public\Documents\LastDays\" + s.User.Name + ".txt", text1);

                           if (x < 0)
                           {
                               await channel.SendMessage("YOU HAVE DIE" + s.User.Mention);
                           }


                           return;
                       }



                   }
               });
                          
                   fire
                   .Do(async (d) =>
                   {
                       if (commandbool)
                       {
                           string ammostr = System.IO.File.ReadAllText(@"C:\Users\Public\Documents\LastDays\" + d.User.Name + "Primary.txt");
                           int ammo = Int32.Parse(ammostr);
                           if (ammo > 0)
                           {
                               ammo = ammo - 1;
                               ammostr = ammo.ToString();
                               string[] b = { ammostr };
                               System.IO.File.WriteAllLines(@"C:\Users\Public\Documents\LastDays\" + d.User.Name + "Primary.txt", b);
                               Random rnd = new Random();
                               int Ran = rnd.Next(1, 10);

                               await channel.SendMessage("You have " + ammostr + " bullets left in your primary  " + d.User.Name);

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
                           }
                           else
                               await channel.SendMessage("You are out of ammo on your primary " + d.User.Mention);
                           Random dam1 = new Random();
                           int damage1 = dam1.Next(10, 95);
                           await channel.SendMessage("You take " + damage1 + " points worth of damage");
                           string text3 = System.IO.File.ReadAllText(@"C:\Users\Public\Documents\LastDays\" + d.User.Name + ".txt");
                           System.Console.WriteLine("Contents of WriteText.txt = {0}", text3);
                           int x1 = Int32.Parse(text3);
                           x1 = x1 - damage1;


                           text3 = x1.ToString();


                           await channel.SendMessage("You current health is: " + x1 + "  " + d.User.Mention);

                           string text2 = text3;
                           System.IO.File.WriteAllText(@"C:\Users\Public\Documents\LastDays\" + d.User.Name + ".txt", text3);

                           if (x1 < 0)
                           {
                               await channel.SendMessage("YOU HAVE DIE" + d.User.Mention);
                           }

                       }
                   });

               firesecond
               .Do(async (h) =>
               {
                   if (commandbool)
                   {
                       string ammostr = System.IO.File.ReadAllText(@"C:\Users\Public\Documents\LastDays\" + h.User.Name + "Sec.txt");
                       int ammo = Int32.Parse(ammostr);
                       if (ammo > 0)
                       {
                           ammo = ammo - 1;
                           ammostr = ammo.ToString();
                           string[] b = { ammostr };
                           System.IO.File.WriteAllLines(@"C:\Users\Public\Documents\LastDays\" + h.User.Name + "Sec.txt", b);



                           Random rnd = new Random();
                           int Ran = rnd.Next(1, 20);
                           await channel.SendMessage("You have " + ammostr + " bullets left  " + h.User.Name);

                           if (Ran == 1)
                           {
                               await channel.SendMessage("You missed " + h.User.Mention);
                               Random dam = new Random();
                               int damage = dam.Next(10, 95);
                               await channel.SendMessage("You take " + damage + " points worth of damage");
                               string text = System.IO.File.ReadAllText(@"C:\Users\Public\Documents\LastDays\" + h.User.Name + ".txt");
                               System.Console.WriteLine("Contents of WriteText.txt = {0}", text);
                               int x = Int32.Parse(text);
                               x = x - damage;


                               text = x.ToString();


                               await channel.SendMessage("You current health is: " + x + "  " + h.User.Mention);

                               string text1 = text;
                               System.IO.File.WriteAllText(@"C:\Users\Public\Documents\LastDays\" + h.User.Name + ".txt", text1);

                               if (x < 0)
                               {
                                   await channel.SendMessage("YOU HAVE DIE" + h.User.Mention);
                               }


                               return;
                           }
                           else
                           {

                               Random random = new Random();
                               int random2 = random.Next(1, 20);

                               if (random2 == 1)
                               {
                                   await channel.SendMessage("You missed " + h.User.Mention);
                                   Random dam = new Random();
                                   int damage = dam.Next(10, 95);
                                   await channel.SendMessage("You take " + damage + " points worth of damage");
                                   string text = System.IO.File.ReadAllText(@"C:\Users\Public\Documents\LastDays\" + h.User.Name + ".txt");
                                   System.Console.WriteLine("Contents of WriteText.txt = {0}", text);
                                   int x = Int32.Parse(text);
                                   x = x - damage;


                                   text = x.ToString();


                                   await channel.SendMessage("You current health is: " + x + "  " + h.User.Mention);

                                   string text1 = text;
                                   System.IO.File.WriteAllText(@"C:\Users\Public\Documents\LastDays\" + h.User.Name + ".txt", text1);

                                   if (x < 0)
                                   {
                                       await channel.SendMessage("YOU HAVE DIE" + h.User.Mention);
                                   }


                                   return;

                               }
                               else
                                   await channel.SendMessage("You hit a Zombie " + h.User.Mention);
                               return;
                           }
                       }
                       await channel.SendMessage("You are out of ammo on your secondary " + h.User.Mention);
                       Random dam1 = new Random();
                       int damage1 = dam1.Next(10, 95);
                       await channel.SendMessage("You take " + damage1 + " points worth of damage");
                       string text3 = System.IO.File.ReadAllText(@"C:\Users\Public\Documents\LastDays\" + h.User.Name + ".txt");
                       System.Console.WriteLine("Contents of WriteText.txt = {0}", text3);
                       int x1 = Int32.Parse(text3);
                       x1 = x1 - damage1;


                       text3 = x1.ToString();


                       await channel.SendMessage("You current health is: " + x1 + "  " + h.User.Mention);

                       string text2 = text3;
                       System.IO.File.WriteAllText(@"C:\Users\Public\Documents\LastDays\" + h.User.Name + ".txt", text3);

                       if (x1 < 0)
                       {
                           await channel.SendMessage("YOU HAVE DIE" + h.User.Mention);
                       }
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
