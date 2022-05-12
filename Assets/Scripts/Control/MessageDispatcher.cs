using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Control
{
    public enum Message { Hello, ByeBye }

    public interface IMsgReceiver
    {
        bool HandleMessage(Telegram telegram);
    }

    public class Telegram : IComparable
    {
        public float dispatchTime = 0f;
        public GameObject sender = null;
        public IMsgReceiver receiver = null;
        public Message msg = Message.Hello;

        public Telegram(GameObject sender, IMsgReceiver receiver, Message msg, float delayTime = 0f)
        {
            this.sender = sender;
            this.receiver = receiver;
            this.msg = msg;
            this.dispatchTime = delayTime;
        }

        public int CompareTo(object obj)
        {
            if (dispatchTime < (obj as Telegram).dispatchTime)
                return -1;
            else if (dispatchTime > (obj as Telegram).dispatchTime)
                return 1;
            else
                return 0;
        }
    }

    public class MessageDispatcher
    {
        SortedSet<Telegram> queue = new SortedSet<Telegram>();
        static MessageDispatcher instance = new MessageDispatcher();
        MessageDispatcher() { }
        public static MessageDispatcher Instance => instance;

        void DischargeMessage(Telegram telegram)
        {
            if (!telegram.receiver.HandleMessage(telegram))
                Debug.Log("Message not handled!");
        }

        public void DispatchMessage(Telegram telegram, float delay = 0)
        {
            if (delay <= 0)
            {
                DischargeMessage(telegram);
            }
            else
            {
                telegram.dispatchTime = Time.unscaledTime + delay;
                queue.Add(telegram);
            }
        }

        public void DispatchDelayMessage()
        {
            while (queue.Count > 0 && queue.Max.dispatchTime < Time.unscaledTime)
            {
                DischargeMessage(queue.Max);
                queue.Remove(queue.Max);
            }
        }
    }
}
