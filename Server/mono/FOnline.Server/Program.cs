using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using main = FOnline.Main;
using System.Threading;

namespace FOnline
{
    class Program
    {
		static void Init(object sender, EventArgs e)
		{
			Global.Log("Init from mono!");
            
			//Tests.InitRun ();

			Global.Log ("Done with tests.");
		}
        static void Main(string[] args)
        {
			main.Init += Init;
            if(args.Contains("-mono_repl"))
                main.Init += (o, e) => StartREPL();
            main.Start += (o, e) =>
            {
				//Tests.StartRun();
                Global.Log("Start from mono!");  
            };		
        }
        static void StartREPL()
        {
            var thread = new Thread(() =>
            {
                var repl = new REPL();
                repl.Process();
            });
            thread.Start();
        }
    }
}
