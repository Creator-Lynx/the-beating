using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class AxeToolController : MonoBehaviour
{
    //WALK ANIMATION BLOCK
    InputAction movingAction;
    InputAction attackAction;
    Animator axeAnimator;
    AudioSource axeAttackAudioSource;

    //[Header("damage section")]
    //[SerializeField] 
    //DAMAGE SECTION
    Collider damageTrigger;


    void Awake()
    {
        //animation initialize
        movingAction = InputSystem.actions.FindAction("Move");
        attackAction = InputSystem.actions.FindAction("Attack");
        axeAnimator = GetComponent<Animator>();
        axeAttackAudioSource = GetComponent<AudioSource>();
        damageTrigger = GetComponent<Collider>();
    }


    void Update()
    {
        //walk animation performed
        if(Time.frameCount % 4 == 0)
        axeAnimator.SetBool("IsWalk", movingAction.IsInProgress());  

        //attack animation
        if (attackAction.WasPressedThisFrame()) 
        {
            axeAnimator.SetTrigger("Attack");   
        }
    }

    public void CallAttackSound()
    {
        axeAttackAudioSource!.Play();
    }

    //damage
    //REMEMBER TO SETTING ON AND OFF COLLIDER!!!!!!!!!
    public void EnableDamageTrigger()
    {
        damageTrigger!.enabled = true;
    }

    public void DisableDamaTrigger()
    {
        damageTrigger!.enabled = false;
    }

    //CALL DAMAGE
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<IDamageable>() != null)
        {
            other.gameObject.GetComponent<IDamageable>().GetDamage();
        }
       Debug.Log(other.gameObject.name);
    }
}
