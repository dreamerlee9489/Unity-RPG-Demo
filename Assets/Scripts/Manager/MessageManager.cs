using System;
using System.Collections.Generic;
using UnityEngine;

namespace App.Manager
{
    public enum Message { Hello, ByeBye }

    public interface IMsgReceiver
    {
        bool HandleMessage(Telegram telegram);
    }

    public class MessageManager
    {
        SortedSet<Telegram> queue = new SortedSet<Telegram>();
        static MessageManager instance = new MessageManager();
        public static MessageManager Instance => instance;

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

    public class Telegram : IComparable
    {
        public float dispatchTime = 0f;
        public GameObject sender = null, receiver = null;
        public Message message = Message.Hello;

        public Telegram(GameObject sender, GameObject receiver, Message msg)
        {
            this.sender = sender;
            this.receiver = receiver;
            this.message = msg;
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

        public override string ToString()
        {
            switch (message)
            {
                case Message.Hello:
                    return "Hello!!!";
                case Message.ByeBye:
                    return "Bye Bye!!!";
                default:
                    return "";
            }
        }
    }
}
