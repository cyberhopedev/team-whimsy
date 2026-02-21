using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.ObjectModel;
using System.Linq;
public abstract class Battler : MonoBehaviour
{
    public Battler()
    {
        
    }

    protected virtual void Start()
    {
        
    }

    protected virtual void Update()
    {
        
    }

    protected abstract void StartTurn();
    protected abstract void EndTurn();
}