using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WeakEventPatterns
{
    public class EventManager : WeakEventManager
    {
        public static void AddListener(Object source,IWeakEventListener listener)
        {
            CurrentManager.ProtectedAddListener(source, listener);
        }
        public static void RemoveListener(object source,IWeakEventListener listener)
        {
            CurrentManager.ProtectedRemoveListener(source, listener);
        }
        public static EventManager CurrentManager
        {
            get
            {
                var manager = GetCurrentManager(typeof(EventManager)) as EventManager;
                if(manager==null)
                {
                    manager = new EventManager();
                    SetCurrentManager(typeof(EventManager), manager);
                }
                return manager;
            }
        }
        protected override void StartListening(object source)
        {
            (source as MailManager).NewMail += EventManager_NewMail;
        }

        private void EventManager_NewMail(object sender, NewMailEventArgs e)
        {
            DeliverEvent(sender, e);
        }

        protected override void StopListening(object source)
        {
            (source as MailManager).NewMail -= EventManager_NewMail;
        }
    }
    
}

