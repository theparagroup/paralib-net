using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using FluentMigrator.Runner;
using com.paralib.Ado;
using com.paralib.Dal;
using com.paralib.Utils;

namespace com.paralib.Migrations.Runner
{
    public class CommandProcessor
    {
        protected Assembly _assembly;
        protected MigrationOptions _migrationOptions;
        public bool Devmode;
        public string Database;


        public static string Help
        {
            get
            {
                string help = "";

                help += "commands:\n";
                help += "\tdevmode\n\trefresh [db]\n\tdrop [db]\n";
                help += "\tup|down [db] [version]\n";
                help += "\trollback [db] [steps]\n";
                help += "\tgen [db]\n";
                help += "\thash [password] [salt]\n";
                help += "\tlist\n";
                help += "\ttables [db]\n";
                help += "\tschema [db] \n";
                help += "\thelp\n";
                help += "\tq(uit)\n";

                return help;
            }


        }
        public static void ConsoleMain(string[] args, Dictionary<string, Action<string[], Action<string>, Action<string>>> customCommands = null) //action(parts, say, sayError)
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
                () => Environment.Exit(0), args, customCommands);

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

        public void Start(Func<string> prompt, Action<string> say, Action<string> sayError, Action quit, string[] commands=null, Dictionary<string, Action<string[], Action<string>, Action<string>>> customCommands =null )
        {

            say("-------------------------------------------------------------------------------");
            say(Help);
            say("-------------------------------------------------------------------------------");
            say($"Default database is [{Paralib.Dal.Databases.Default}]");
            say($"Assembly is [{_assembly.GetName().Name}]");
            say($"devmode is [{Devmode}]");
            say("-------------------------------------------------------------------------------");
            say("");

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

                    //commands that don't require database
                    string arg2 = null;
                    string arg3 = null;

                    if (parts.Length > 1) arg2 = parts[1];
                    if (parts.Length > 2) arg3 = parts[2];

                    switch (parts[0])
                    {
                        case "quit":
                        case "q":
                            quit();
                            continue;

                        case "help":
                            say(Help);
                            continue;

                        case "devmode":
                            Devmode = !Devmode;
                            say($"devmode is [{Devmode}]");
                            continue;

                        case "hash":

                            string hash = null;

                            if (arg2 == null)
                            {
                                say("password required");
                                continue;
                            }

                            if (arg3 != null)
                            {
                                hash = Crypto.HashPassword(arg2, arg3);
                            }
                            else
                            {
                                hash = Crypto.HashPassword(arg2);
                            }

                            string[] hashParts = hash.Split('|');
                            say($"{hash}");
                            say($"salt => {hashParts[0]}");
                            say($"hash => {hashParts[1]}");

                            continue;

                    }


                    //commands that use a database

                    //(poor attempt at parsing this shit)
                    string db = null;
                    string n = null;
                    if (parts.Length > 1)
                    {
                        if (parts[0] == "up" || parts[0] == "down" || parts[0] == "rollback")
                        {
                            //is arg2 a string
                            if ((from c in parts[1].ToCharArray() where !char.IsNumber(c) select c).Count() == 0)
                            {
                                if (parts.Length == 2)
                                {
                                    n = parts[1];
                                }
                                else if (parts.Length == 3)
                                {
                                    //okay, a numeric database name
                                    db = parts[1];
                                    n = parts[2];
                                }
                            }
                            else
                            {
                                db = parts[1];
                            }
                        }
                        else
                        {
                            db = parts[1];
                        }

                    }


                    db = db ?? Database;

                    Database database = null;

                    if (Paralib.Dal.Databases.Exists(db))
                    {
                        database = Paralib.Dal.Databases[db];
                    }
                    else
                    {
                        sayError($"Database [{db}] doesn't exist.");
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

                            case "tables":

                                using (var con = new Db(database))
                                {
                                    var ts = con.GetTables();

                                    say("");

                                    foreach (var t in ts)
                                    {
                                        say($"  {t.Name}");
                                    }

                                    say("");
                                }

                                break;

                            case "schema":

                                using (var con = new Db(database))
                                {
                                    var ts = con.GetTables();

                                    say("");

                                    foreach (var t in ts.OrderBy(t=>t.Name))
                                    {
                                        say($"  {t.Name}");

                                        foreach (var c in t.Columns.Values)
                                        {
                                            string coldesc = $"   > {c.Name} [{c.DbType}(";

                                            if (c.Length.HasValue)
                                            {
                                                coldesc += $"{c.Length})]";
                                            }
                                            else
                                            {
                                                coldesc += $"{c.Precision}-{c.Scale})]";
                                            }

                                            coldesc += $" [{c.ClrType?.Name}]";

                                            if (c.IsNullable)
                                            {
                                                coldesc += " NULL";
                                            }
                                            else
                                            {
                                                coldesc += " NOT NULL";
                                            }

                                            if (c.IsPrimary)
                                            {
                                                coldesc += " PK";
                                            }

                                            if (c.IsForeign)
                                            {
                                                coldesc += $" FK";
                                            }

                                            //say($"As{c.Name}, {c.DbType}, {c.ClrType}");
                                            //say($"case \"{c.DbType}\": c.ClrType = typeof({c.ClrType}); break;");

                                            say(coldesc);

                                            if (c.Properties!=null)
                                            {
                                                say($"      Props: {c.Properties.ParaType}, '{c.Properties.Description}'");
                                            }

                                        }

                                        if (t.ForeignKeys.Length > 0)
                                        {
                                            say("");
                                            say("      foreign keys:");
                                            foreach (var r in t.ForeignKeys)
                                            {
                                                say($"        {r.OnColumn} -> {r.OtherTable}.{r.OtherColumn}");
                                            }
                                        }

                                        if (t.References.Length > 0)
                                        {
                                            say("");
                                            say("      references:");
                                            foreach (var r in t.References)
                                            {
                                                say($"        {r.OtherTable}.{r.OtherColumn} -> {r.OnColumn}");
                                            }
                                        }

                                        say("");

                                    }

                                    say("");
                                }

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
                                CodeGenerator.Generate(database);
                                sayError($"Complete ");
                                break;

                            default:

                                //custom command?
                                if (customCommands!=null)
                                {
                                    if (customCommands.ContainsKey(parts[0]))
                                    {
                                        customCommands[parts[0]](parts, say, sayError);
                                        break;
                                    }
                                }

                                sayError($"Unknown command {parts[0]}");
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        sayError($"Exception [{ex.GetType().Name}] '{ex.Message}' occurred.\n\n{ex.StackTrace}");
                    }

                }

            }


        }

    }
}
