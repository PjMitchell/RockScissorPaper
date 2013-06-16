using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockScissorPaper.BLL
{
    public class GameEventManager
    {
        
        private ConcurrentDictionary<object, string> _subscriptions = new ConcurrentDictionary<object, string>();

        public GameEventManager()
        {
            _subscriptions = new ConcurrentDictionary<object, string>();
        }

        public void Subscribe<TMessage>(Action<TMessage> handler)
        {
            _subscriptions.TryAdd(handler, typeof(TMessage).FullName);
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
