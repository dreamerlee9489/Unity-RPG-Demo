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
        public Message message = Message.Hello;

        public Telegram(GameObject sender, IMsgReceiver receiver, Message msg, float delayTime = 0f)
        {
            this.sender = sender;
            this.receiver = receiver;
            this.message = msg;
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
        public static MessageDispatcher Instance => instance;

        void Discharge(IMsgReceiver receiver, Telegram telegram)
        {
            if (!receiver.HandleMessage(telegram))
                Debug.Log("Message not handled!");
        }

        public void Dispatch(Telegram telegram, float delay = 0)
        {
            if (delay <= 0)
            {
                Discharge(telegram.receiver, telegram);
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
                Discharge(queue.Max.receiver, queue.Max);
                queue.Remove(queue.Max);
            }
        }
    }

    public static class MessageTranslator
    {
        public static string Translate(Message message)
        {
            switch (message)
            {
                case Message.Hello:
                    return "Hello";
                case Message.ByeBye:
                    return "Bye Bye";
                default:
                    return "";
            }
        }
    }
}
