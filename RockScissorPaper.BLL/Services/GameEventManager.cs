using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockScissorPaper.BLL
{
    public class GameEventManager
    {
        

        private Dictionary<object, string> _subscriptions = new Dictionary<object, string>();

        public GameEventManager()
        {
            _subscriptions = new Dictionary<object, string>();
        }

        public void Subscribe<TMessage>(Action<TMessage> handler)
        {
            _subscriptions.Add(handler, typeof(TMessage).FullName);
            //if(_subscribersList.Any(m=>m.Key.GetType ==TMessage)
        }

        public void Publish<TMessage>(TMessage msg)
        {
            var subs = _subscriptions.Where(e => e.Value == typeof(TMessage).FullName);
            foreach (var sub in subs)
            {
                var action = sub.Key as Action<TMessage>;
                action(msg);
            }
        }
    }
}
