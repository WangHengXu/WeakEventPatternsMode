using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WeakEventPatterns
{
    /// <summary>
    /// https://docs.microsoft.com/en-us/dotnet/framework/wpf/advanced/weak-event-patterns
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
   
            var manager = new MailManager();
            var fax = new Fax();
          //自定义弱事件使用
            //EventManager.AddListener(manager, fax);
            //.net4.5集成后使用
            WeakEventManager<MailManager, NewMailEventArgs>.
           AddHandler(manager, "NewMail", fax.FaxMsg);
            manager.SimulateNewMail("1", "2", "3");
            fax = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            manager.SimulateNewMail("1", "2", "3");
            Console.Read();
        }
    }
}
