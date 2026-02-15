using UnityEngine;

public class MeatInteractiveBehaviour : MonoBehaviour, IInteractivable
{
    [SerializeField] AudioSource audioSourceMain, audioSourceTake;
    [SerializeField] Collider _collider, _trigger;
    [SerializeField] Rigidbody _rigidbody;
    [SerializeField] float pitchShiftExtremum = 0.1f;

    void OnCollisionEnter(Collision collision)
    {
        audioSourceMain.pitch = 1 + Random.Range(-1f, 1f) * pitchShiftExtremum;
        audioSourceMain.Play();
    }

    public void EnableDropState()
    {
        _collider.enabled = true;
        _trigger.enabled = true;
        _rigidbody.useGravity = true;
    }

    public string GetInteractionHint()
    {
        return "взять";
    }

    public void Interact()
    {
        

        if(HandResourceInventory.resourceInventory.TryToIncrementMeatCount())
        {
            PlayerGetMeat();
        }
        else
        {
            PlayerDontGetMeat();
        }
    }

    void PlayerGetMeat()
    {
        _collider.enabled = false;
        _trigger.enabled = false;
        _rigidbody.useGravity = false;
        _rigidbody.isKinematic = true;
        _rigidbody.Sleep();

        audioSourceTake.pitch = 1 + Random.Range(-1f, 1f) * pitchShiftExtremum;
        audioSourceTake.Play();
    }

    void PlayerDontGetMeat()
    {
        _rigidbody.AddTorque(Vector3.up, ForceMode.Impulse);
        audioSourceMain.pitch = 1 + Random.Range(-1f, 1f) * pitchShiftExtremum;
        audioSourceMain.Play();
    }
}
