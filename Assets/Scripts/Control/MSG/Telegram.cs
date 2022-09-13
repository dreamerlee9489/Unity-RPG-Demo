using System;
using UnityEngine;

namespace Control.MSG
{
    public class Telegram : IComparable
    {
        public GameObject sender = null;
        public GameObject receiver = null;
        public Message message = Message.Hello;
        public float dispatchTime = 0f;
        public object extraInfo = null;

        public Telegram(GameObject sender, GameObject receiver, Message msg)
        {
            this.sender = sender;
            this.receiver = receiver;
            this.message = msg;
        }

        public int CompareTo(object obj)
        {
            if (dispatchTime < ((Telegram)obj).dispatchTime)
                return -1;
            else if (dispatchTime > ((Telegram)obj).dispatchTime)
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