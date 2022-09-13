namespace Control.MSG
{
    public interface IMsgReceiver
    {
        bool HandleMessage(Telegram telegram);
    }
}