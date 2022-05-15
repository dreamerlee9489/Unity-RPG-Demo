using System;
using UnityEngine;

namespace App.Control
{
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
