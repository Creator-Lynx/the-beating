using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class AxeToolController : MonoBehaviour
{
    //WALK ANIMATION BLOCK
    InputAction movingAction;
    InputAction attackAction;
    Animator axeAnimator;




    void Awake()
    {
        //animation initialize
        movingAction = InputSystem.actions.FindAction("Move");
        attackAction = InputSystem.actions.FindAction("Attack");
        axeAnimator = gameObject.GetComponent<Animator>();
    }


    void Update()
    {
        //walk animation performed
        if(Time.frameCount % 4 == 0)
        axeAnimator.SetBool("IsWalk", movingAction.IsInProgress());    
    }
}
