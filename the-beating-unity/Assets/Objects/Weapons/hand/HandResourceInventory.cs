using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandResourceInventory : MonoBehaviour
{
    //global
    public static HandResourceInventory resourceInventory;

    [Header("Meat inventory")]
    [SerializeField] int meatCount = 0;
    [SerializeField] int maxMeatCount = 3;

    [Space(20)]
    [Header("Hand Animatons")]
    [SerializeField] Animator animator;
    [SerializeField] string handInteractionTriggerName = "Grab";
    [SerializeField] string handWalkBoolName = "IsWalk";
    [SerializeField] string handResetTriggerName = "SetEmpty";
    int interactTriggerId, resetTriggerId, walkBoolId;
    //input operate for animation "walk"
    InputAction movingAction;

    [Space(20)]
    [Header("Meat positions")]
    [SerializeField] Transform[] meatPositions = new Transform[3];
    [Header("Meat transition timings")]
    [SerializeField] float transitionDelay = 0.05f;
    [SerializeField] float transitionDuration = 0.4f;


    void Start()
    {
        resourceInventory = this;

        interactTriggerId = Animator.StringToHash(handInteractionTriggerName);
        resetTriggerId = Animator.StringToHash(handResetTriggerName);
        walkBoolId = Animator.StringToHash(handWalkBoolName);
        movingAction = InputSystem.actions.FindAction("Move");
    }

    public bool TryToIncrementMeatCount()
    {
        if(meatCount < maxMeatCount)
        {
            meatCount++;
            animator.SetTrigger(interactTriggerId);
            return true;
        }
        return false;
    }

    public bool TryToIncrementMeatCount(Transform transform)
    {
        if(meatCount < maxMeatCount)
        {
            meatCount++;
            animator.SetTrigger(interactTriggerId);
            return true;
        }
        return false;
    }



    /// <summary>
    /// Clear player resource inventory. 
    /// Returns count of resources that player caried
    /// </summary>
    public int ResourceTransfer()
    {
        int result = meatCount;
        meatCount = 0;
        animator.SetTrigger(resetTriggerId);
        return result;
    }

    void Update()
    {
        //test call of crearing resources
        if(Keyboard.current.tKey.wasPressedThisFrame) ResourceTransfer();
        //operate walk animation
        if(Time.frameCount % 4 == 0)
        animator.SetBool(walkBoolId, movingAction.IsInProgress());  
    }
}
