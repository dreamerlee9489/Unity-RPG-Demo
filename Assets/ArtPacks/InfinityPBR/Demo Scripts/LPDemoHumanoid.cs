using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LPDemoHumanoid : MonoBehaviour
{
    
    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }
    
    public void Locomotion(float newValue){
        animator.SetFloat ("Locomotion", newValue);
    }

    public void PlayAudio()
    {
        
    }

    public void StartLoop()
    {
        
    }

    public void StopLoop()
    {
        
    }

    public void StartCastGeneric1()
    {
        
    }
    
    public void StopCastGeneric1()
    {
        
    }
    
    public void StartCastGeneric2()
    {
        
    }
    
    public void StopCastGeneric2()
    {
        
    }
    
    public void StartCastGeneric3()
    {
        
    }
    
    public void StopCastGeneric3()
    {
        
    }
    
    public void StartCastBarbarian1()
    {
        
    }
    
    public void StopCastBarbarian1()
    {
        
    }
}
