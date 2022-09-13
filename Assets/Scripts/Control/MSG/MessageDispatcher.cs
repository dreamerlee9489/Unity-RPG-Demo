using System.Collections.Generic;
using UnityEngine;

namespace Control.MSG
{
    public class MessageDispatcher
    {
        private readonly SortedSet<Telegram> _queue = new SortedSet<Telegram>();
        private static readonly MessageDispatcher instance = new MessageDispatcher();
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
                _queue.Add(telegram);
            }
        }

        public void DispatchDelay()
        {
            while (_queue.Count > 0 && _queue.Max.dispatchTime < Time.unscaledTime)
            {
                Discharge(_queue.Max);
                _queue.Remove(_queue.Max);
            }
        }
    }
}