using com.paralib.Ado;
using FluentMigrator.Runner;
using System;
using System.Linq;
using System.Reflection;

namespace com.paralib.Migrations.Runner
{
    public class CommandProcessor
    {
        protected Assembly _assembly;
        protected MigrationOptions _migrationOptions;
        public bool Devmode;
        public string Database;

        public static string Help { get; } = "commands:\n\tdevmode\n\trefresh [db]\n\tdrop [db]\n\tup|down [db] [version]\n\trollback [db] [steps]\n\tgen [db]\n\tlist\n\tq(uit)\n";

        public static void ConsoleMain(string[] args)
        {
            Paralib.Initialize();


            CommandProcessor commandProcessor = new CommandProcessor(Assembly.GetEntryAssembly(), new MigrationOptions());

            commandProcessor.Devmode = Paralib.Migrations.Devmode;
            commandProcessor.Database = Paralib.Migrations.Database;

            if ((args==null) || (args.Length==0))
            {
                args = Paralib.Migrations.Commands?.Split(',');
            }

            commandProcessor.Start(
                () => { Console.Write("command: "); return Console.ReadLine(); },
                s => { ConsoleColor oldColor = Console.ForegroundColor; Console.ForegroundColor = ConsoleColor.Green; Console.WriteLine(s); Console.ForegroundColor = oldColor; },
                s => { ConsoleColor oldColor = Console.ForegroundColor; Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine(s); Console.ForegroundColor = oldColor; }, 
                () => Environment.Exit(0), args);

        }

        public CommandProcessor(Assembly assembly, MigrationOptions migrationOptions)
        {
            _assembly = assembly;
            _migrationOptions = migrationOptions;
        }

        protected bool NotDevmode(Action<string> sayError)
        {
            if (!Devmode)
            {
                sayError("must be in devmode");
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Start(Func<string> prompt, Action<string> say, Action<string> sayError, Action quit, string[] commands=null)
        {

            say("-------------------------------------------------------------------------------");
            say(Help);
            say("-------------------------------------------------------------------------------");
            say($"Default database is [{Paralib.Dal.Databases.Default}]");
            say($"devmode is [{Devmode}]");
            say("-------------------------------------------------------------------------------");

            int i = 0;

            while (true)
            {
                string command = "";

                if (commands != null && i < commands.Length)
                {
                    command = commands[i].Trim();
                    say($"executing: {command}");
                    ++i;
                }
                else
                {
                    command = prompt().Trim();
                }

                string[] parts = command.Split(' ');

                if (parts.Length > 0)
                {
                    string db = null;
                    string n = null;

                    if (parts.Length > 1)
                    {
                        if (parts[0] == "up" || parts[0] == "down" || parts[0] == "migrate")
                        {
                            //is arg2 a string
                            if ((from c in parts[1].ToCharArray() where !char.IsNumber(c) select c).Count() == 0)
                            {
                                db = parts[1];

                                if (parts.Length==3)
                                {
                                    n = parts[2];
                                }
                            }
                            else
                            {
                                n = parts[1];
                            }
                        }
                        else
                        {
                            db = parts[1];
                        }

                    }


                    if (command=="q" || command=="quit")
                    {
                        quit();
                    }

                    db = db ?? Database;

                    Database database = null;

                    if (Paralib.Dal.Databases.Exists(db))
                    {
                        database = Paralib.Dal.Databases[db];
                    }
                    else
                    {
                        sayError($"Database {db} doesn't exist.");
                        continue;
                    }


                    long? v=null;
                    long tempV;
                    if (long.TryParse(n, out tempV)) v = tempV;

                    MigrationRunner migrationRunner = RunnerFactory.Create(_assembly, _migrationOptions, database);

                    try
                    {

                        switch (parts[0])
                        {

                            case "list":
                                migrationRunner.ListMigrations();
                                break;

                            case "refresh":
                                if (NotDevmode(sayError)) continue;
                                migrationRunner.MigrateDown(long.MinValue);
                                migrationRunner.MigrateUp();
                                break;

                            case "drop":
                                if (NotDevmode(sayError)) continue;
                                say("not implemented yet");
                                break;

                            case "up":
                                if (v.HasValue)
                                {
                                    migrationRunner.MigrateUp(v.Value);
                                }
                                else
                                {
                                    migrationRunner.MigrateUp();
                                }
                                break;

                            case "down":
                                if (v.HasValue)
                                {
                                    migrationRunner.MigrateDown(v.Value);
                                }
                                else
                                {
                                    if (NotDevmode(sayError)) continue;
                                    migrationRunner.MigrateDown(long.MinValue);
                                }
                                break;

                            case "rollback":
                                if (v.HasValue)
                                {
                                    migrationRunner.Rollback((int)v.Value);
                                }
                                else
                                {
                                    if (NotDevmode(sayError)) continue;
                                    migrationRunner.MigrateDown(long.MinValue);
                                }
                                break;

                            case "gen":
                                sayError($"not implemented ");
                                break;

                            case "devmode":
                                Devmode = !Devmode;
                                say($"devmode is [{Devmode}]");
                                break;

                            default:
                                sayError($"Unknown command {parts[0]}");
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        sayError($"Exception {ex.Message} occurred.");
                    }

                }

            }


        }

    }
}
