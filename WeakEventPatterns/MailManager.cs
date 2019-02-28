using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WeakEventPatterns
{
    public class MailManager
    {
        public event EventHandler<NewMailEventArgs> NewMail;

        public virtual void OnNewMail(NewMailEventArgs e)
        {
            e.Raise(this, ref NewMail);
        }
        public void SimulateNewMail(string from,string to,string subject)
        {
            NewMailEventArgs e = new NewMailEventArgs(from, to, subject);
            OnNewMail(e);
        }
    }
    /// <summary>
    /// 定义一个类型容纳所有应该发送给事件通知接受者的附加信息
    /// </summary>
    public class NewMailEventArgs:EventArgs
    {
        private readonly String m_from, m_to, m_subject;
        public NewMailEventArgs(string from,string to,string subject)
        {
            m_from = from;
            m_to = to;
            m_subject = subject;
        }
        public string From { get { return m_from; } }

        public string  To { get { return m_to; } }

        public string  Subject { get { return m_subject; } }
    }

    public static class EventArgEctensions
    {
        public static void Raise<TEventArgs>(this TEventArgs e,Object sender,ref EventHandler<TEventArgs> eventDelegate)
        {
            Volatile.Read(ref eventDelegate)?.Invoke(sender, e);
        }
    }
}
