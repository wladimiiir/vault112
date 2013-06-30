using Mono.CSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FOnline
{
    public class REPL : IDisposable
    {
        Evaluator eval;
        StringBuilder statement;
        public REPL()
        {
            AllocConsole();
            var stdout = new StreamWriter(Console.OpenStandardOutput());
            stdout.AutoFlush = true;
            Console.SetOut(stdout);
            Console.SetError(stdout);
            Console.SetIn(new StreamReader(Console.OpenStandardInput()));
            Init();
        }
        public void Dispose()
        {
            FreeConsole();
        }
        void Init()
        {
            // workaround to load DLR types before Evaluator would want them
            var r = Microsoft.CSharp.RuntimeBinder.Binder.IsEvent(Microsoft.CSharp.RuntimeBinder.CSharpBinderFlags.BinaryOperationLogical, "gf", typeof(int));
            var settings = new CompilerSettings();
            var printer = new ConsoleReportPrinter();
            eval = new Evaluator(new CompilerContext(settings, printer));
            eval.ReferenceAssembly(typeof(REPL).Assembly);
            eval.Run("using System;");
            eval.Run("using FOnline;");
            eval.Run("using System.Collections.Generic;");
            eval.Run("using System.Linq;");
            statement = new StringBuilder();
        }
        public void Process()
        {
            while (true)
            {
                if (statement.Length > 0)
                    Console.Write("--> ");
                else
                    Console.Write("> ");
                var line = Console.ReadLine();
                if (line.StartsWith("/"))
                {
                    if (line == "/reset")
                    {
                        Console.WriteLine("Resetting...");
                        Init();
                    }
                    else
                    {
                        Console.WriteLine("Unknown command");
                    }
                    continue;
                }
                if (line.EndsWith(";;"))
                {
                    statement.AppendLine(line.Remove(line.Length - 1));
                    try
                    {
                        bool set;
                        object res;
                        var output = eval.Evaluate(statement.ToString(), out res, out set);
                        if (set)
                            Console.WriteLine(res.ToString());
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                        Console.WriteLine("Buffer:");
                        Console.WriteLine(statement.ToString());
                    }
                    finally
                    {
                        statement.Clear();
                    }
                }
                else if (line.Length > 0 && line.EndsWith("?"))
                {
                    var strip = line.Remove(line.Length - 1);
                    string prefix;
                    foreach (var compl in eval.GetCompletions(strip, out prefix))
                    {
                        Console.WriteLine("  {0}{1}", prefix, compl);
                    }
                }
                else
                    statement.AppendLine(line);
            }
        }
        public string[] GetCompletions(string line)
        {
            string prefix;
            return eval.GetCompletions(line, out prefix);
        }

        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern static bool AllocConsole();
        [MethodImpl(MethodImplOptions.InternalCall)]
        public extern static bool FreeConsole(); 
    }
}
