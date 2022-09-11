using UnityEngine;

namespace Control.CMD
{
    public interface ICmdReceiver
    {
        void ExecuteAction(Vector3 point);
        void ExecuteAction(Transform target);
        void CancelAction();
    }
}