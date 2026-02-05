using UnityEngine;

public class TreeDamageable : MonoBehaviour, IDamageable
{
    public void GetDamage()
    {
        Debug.Log("Tree get damage");
    }
}
