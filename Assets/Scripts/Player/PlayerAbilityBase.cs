using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerAbilityBase : MonoBehaviour
{
    protected PlayerController player;

    protected Inputs inputs;

    private void OnValidate()
    { 
        if (player == null)
            player = GetComponent<PlayerController>();
    }

    private void Start()
    {
        inputs = new Inputs();
        inputs.Enable();

        Init();
        OnValidate();
        RegisterListener();
    }

    private void OnEnable()
    {
        if(inputs != null)
        inputs.Enable();
    }

    private void OnDisable()
    {
        inputs.Disable();
    }

    private void OnDestroy()
    {
        RemoveListener();
    }

    protected virtual void Init()
    { 
    
    
    }

    protected virtual void RegisterListener()
    { 
    
    
    }

    protected virtual void RemoveListener()
    {


    }   
}
