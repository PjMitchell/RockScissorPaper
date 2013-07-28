using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RockScissorPaper.Models
{
    public class FixedSizeQueue<T> 
    {
        ConcurrentQueue<T> q = new ConcurrentQueue<T>();

        public ConcurrentQueue<T> Get()
        {
            return q;
        }
        
        public int Size { get; private set; }

        public FixedSizeQueue(int size)
        {
            Size = size;
        }

        public void Resize(int newSize)
        {
            Size = newSize;
            Trim();
        }

        public void Enqueue(T obj)
        {
            q.Enqueue(obj);
            Trim();
        }

        private void Trim()
        {
            lock (q)
            {
                while (q.Count > Size)
                {
                    T outObject;
                    q.TryDequeue(out outObject);
                }
            }
        }
    }
}