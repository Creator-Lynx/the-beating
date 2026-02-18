using System.Collections;
using System.Runtime.CompilerServices;
using Unity.Multiplayer.PlayMode;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

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


    [Space(20)]
    [Header("To know when have a meat")]
    [SerializeField]
    UnityEvent FirstMeatTakenEvent;


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
            if (meatCount == 0) FirstMeatTakenEvent.Invoke();
            meatCount++;
            animator.SetTrigger(interactTriggerId);
            return true;
        }
        return false;
    }

    public bool TryToIncrementMeatCount(Transform meat)
    {
        if(meatCount < maxMeatCount)
        {
            if (meatCount == 0) FirstMeatTakenEvent.Invoke();
            StartCoroutine(TranslateObjectToPos(meat, meatCount));
            meatCount++;
            animator.SetTrigger(interactTriggerId);
            return true;
        }
        return false;
    }

    IEnumerator TranslateObjectToPos(Transform meat, int posId)
    {
        yield return new WaitForSeconds(transitionDelay);
        float timer = 0f;
        meat.SetParent(meatPositions[posId]);
        Vector3 startPos = meat.position;
        Vector3 startScale = meat.localScale;
        while(timer < transitionDuration)
        {
            meat.position = Vector3.Lerp(startPos, meatPositions[posId].position, timer/transitionDuration);
            meat.localScale = Vector3.Lerp(startScale, meatPositions[posId].localScale, timer/transitionDuration);
            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;
        }
        meat.position = meatPositions[posId].position;
        meat.localScale = meatPositions[posId].localScale;
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
        foreach(Transform tr in meatPositions)
        {
            if(tr.childCount > 0)
            Destroy(tr.GetChild(0).gameObject);
        }
        return result;
    }

    void Update()
    {
        //test call of crearing resources
        //if(Keyboard.current.tKey.wasPressedThisFrame) ResourceTransfer();
        //operate walk animation
        if(Time.frameCount % 4 == 0)
        animator.SetBool(walkBoolId, movingAction.IsInProgress());  
    }
}
