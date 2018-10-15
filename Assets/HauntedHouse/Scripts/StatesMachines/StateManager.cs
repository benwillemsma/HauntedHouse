using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public BaseState State;
    private bool m_isPaused;
    public bool IsPaused
    {
        get {return m_isPaused; }
        set { m_isPaused = value; }
    }

    private bool changingState = false;

    [Space(10),SerializeField,Header("Debuging Info")]
    private string CurrentState;

    protected void Start()
    {
        if (State != null)
        {
            StartCoroutine(State.EnterState(null));
            CurrentState = State.ToString();
        }
        else enabled = false;
    }

    protected void Update()
    {
        if (State != null) State.Update();
    }

    protected void LateUpdate()
    {
        if (State != null) State.LateUpdate();
    }

    protected void FixedUpdate()
    {
        if (State != null) State.FixedUpdate();
    }

    //State Functions
    public void ChangeState(BaseState newState)
    {
        if (!changingState)
        {
            changingState = true;
            StartCoroutine(HandleStateTransition(newState));
        }
    }
    protected IEnumerator HandleStateTransition(BaseState newState)
    {
        //Exit State
        State.InTransition = true;
        yield return StartCoroutine(State.ExitState(newState));
        State.InTransition = false;

        //Set New State
        BaseState prevState = State;
        State = newState;

        //Enter State
        State.InTransition = true;
        yield return StartCoroutine(State.EnterState(prevState));
        State.InTransition = false;

        prevState = null;
        CurrentState = State.ToString();
        changingState = false;
    }

    #region Collision

    // Unity Trigger Functions Call Current State Tirgger Functions
    private void OnTriggerEnter(Collider other)
    {
        if (State != null) State.OnTriggerEnter(other);
    }
    private void OnTriggerStay(Collider other)
    {
        if (State != null) State.OnTriggerStay(other);
    }
    private void OnTriggerExit(Collider other)
    {
        if (State != null) State.OnTriggerExit(other);
    }

    // Unity Collision Functions Call Current State Collision Functions
    private void OnCollisionEnter(Collision collision)
    {
        if (State != null) State.OnCollisionEnter(collision);
    }
    private void OnCollisionStay(Collision collision)
    {
        if (State != null) State.OnCollisionStay(collision);
    }
    private void OnCollisionExit(Collision collision)
    {
        if (State != null) State.OnCollisionExit(collision);
    }
    #endregion 
}