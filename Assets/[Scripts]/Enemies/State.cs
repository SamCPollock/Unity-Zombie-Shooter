using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour
{
    protected StateMachine stateMachine;

    public float UpDateinterval { get; protected set; } = 1f;

    protected State(StateMachine _stateMachine)
    {
        stateMachine = _stateMachine;
    }

    public virtual void Start()
    {

    }
}
