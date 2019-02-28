using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WeakEventPatterns
{
    public class Fax:IWeakEventListener
    {
        public void FaxMsg(Object sender,NewMailEventArgs e)
        {
            Console.WriteLine($"{e.From}   {e.To}   {e.Subject}");
        }
        public bool ReceiveWeakEvent(Type managerType, object sender, EventArgs e)
        {
            FaxMsg(sender, e as NewMailEventArgs);
            return true;
        } 
    }
}
