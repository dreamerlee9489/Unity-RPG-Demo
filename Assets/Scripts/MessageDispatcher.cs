using System.Collections.Generic;
using UnityEngine;

namespace App
{
    public enum Message { Hello, ByeBye }

    public interface IMsgReceiver
    {
        bool HandleMessage(Telegram telegram);
    }

    public class MessageDispatcher
    {
        SortedSet<Telegram> queue = new SortedSet<Telegram>();
        static MessageDispatcher instance = new MessageDispatcher();
        public static MessageDispatcher Instance => instance;

        void Discharge(Telegram telegram)
        {
            if (!telegram.receiver.GetComponent<IMsgReceiver>().HandleMessage(telegram))
                Debug.Log("Message not handled!");
        }

        public void Dispatch(Telegram telegram, float delay = 0)
        {
            if (delay <= 0)
            {
                Discharge(telegram);
            }
            else
            {
                telegram.dispatchTime = Time.unscaledTime + delay;
                queue.Add(telegram);
            }
        }

        public void DispatchDelay()
        {
            while (queue.Count > 0 && queue.Max.dispatchTime < Time.unscaledTime)
            {
                Discharge(queue.Max);
                queue.Remove(queue.Max);
            }
        }
    }
}
