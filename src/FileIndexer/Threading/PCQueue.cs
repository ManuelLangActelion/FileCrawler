using System.Collections.Generic;
using System.Threading;

namespace CodeProject.Threading
{
    public class PCQueue<T> where T : class
    {

        private readonly object lockObject = new object();
        private readonly T terminationToken = null;
        private readonly int maxSize;
        private readonly Queue<T> itemsQueue = new Queue<T>();

        public PCQueue(int maxSize = 1024)
        {
            this.maxSize = maxSize;
        }


        private bool isAddingCompleted = false;
        public bool IsAddingCompleted
        {
            get
            {
                return isAddingCompleted;
            }
        }
        public void CompleteAdding()
        {
            EnqueueItem(terminationToken);      // signals that we stopped adding items
        }

        public bool EnqueueItem(T item)
        {
            lock (lockObject)
            {
                if (IsAddingCompleted)
                    return false;

                while (itemsQueue.Count >= maxSize)
                {
                    Monitor.Wait(lockObject);
                }

                itemsQueue.Enqueue(item);
                if (item == terminationToken)
                    isAddingCompleted = true;

                if (itemsQueue.Count == 1)
                {
                    // wake up any blocked dequeue
                    Monitor.PulseAll(lockObject);
                }
                return true;
            }
        }
        

        public bool TryTake(out T result)
        {
            lock (lockObject)
            {
                while (itemsQueue.Count == 0)
                {
                    if (IsAddingCompleted)  // queue is empty and no more items can be added: stop the iteration
                    {      
                        result = default(T);
                        return false;
                    }
                    Monitor.Wait(lockObject);
                }
                T item = itemsQueue.Dequeue();
                if (item == terminationToken)
                {
                    result = default(T);
                    return false;
                }

                if (itemsQueue.Count == maxSize - 1)
                {
                    // wake up any blocked enqueue
                    Monitor.PulseAll(lockObject);
                }
                result = item;
                return true;
            }
        }
        
        public IEnumerable<T> GetItems()
        {
            lock (lockObject)
            {
                while (itemsQueue.Count > 0 || !IsAddingCompleted)
                {
                    while (itemsQueue.Count == 0)
                    {
                        if (IsAddingCompleted)      // queue is empty and no more items can be added: stop the iteration
                            yield break;
                        Monitor.Wait(lockObject);
                    }
                    T item = itemsQueue.Dequeue();
                    if (item == terminationToken)
                        yield break;

                    if (itemsQueue.Count == maxSize - 1)
                    {
                        // wake up any blocked enqueue
                        Monitor.PulseAll(lockObject);
                    }
                    yield return item;
                }
            }
        }

    }
}
