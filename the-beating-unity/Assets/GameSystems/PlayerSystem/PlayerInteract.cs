using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] float raycastDistance = 3f;
    [SerializeField] LayerMask layerMask;
    [SerializeField] InteractionHint interactionHint;

    
    void FixedUpdate()
    {
        RaycastHit raycastHit;
        if(Physics.Raycast(transform.position, transform.forward, out raycastHit, raycastDistance, layerMask))
        {
            //call hint if new
            //write current interactivable
            if(interactivable == null)
            {
                interactivable = raycastHit.collider.GetComponent<IInteractivable>();
                //call show hint
                interactionHint.ShowHint(interactivable.GetInteractionHint());
            }
        }
        else 
        {
            interactivable = null;
            //call hide of hint
            interactionHint.HideHint();
        }
        
    }

    IInteractivable interactivable = null;
    void Update()
    {
        if(interactInputAction.WasPressedThisFrame()) 
        //call interact of current interactivable if it is
        //if(interactivable != null) 
        interactivable?.Interact();
    }

    InputAction interactInputAction;
    void Awake()
    {
        interactInputAction = InputSystem.actions.FindAction("Interact");
    }
}
