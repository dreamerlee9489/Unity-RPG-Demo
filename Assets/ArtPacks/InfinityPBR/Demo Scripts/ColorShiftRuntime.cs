using UnityEngine;

namespace InfinityPBR
{
    public class ColorShiftRuntime : MonoBehaviour
    {
        public ColorShifterObject colorShifterObject;
        
        public void SetActiveColorSet() => colorShifterObject.SetActiveColorSet();
        public void SetColorSet(int index) =>  colorShifterObject.SetColorSet(index);
        public void SetColorSet(string name) => colorShifterObject.SetColorSet(name);
        public void SetRandomColorSet() => colorShifterObject.SetRandomColorSet();
    }
    
}

