using UnityEngine;

public class MeatInteractiveBehaviour : MonoBehaviour, IInteractivable
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] Collider _collider;
    [SerializeField] Rigidbody _rigidbody;

    void OnCollisionEnter(Collision collision)
    {
        audioSource.Play();
    }

    public void EnableDropState()
    {
        _collider.enabled = true;
        _rigidbody.useGravity = true;
    }
}
