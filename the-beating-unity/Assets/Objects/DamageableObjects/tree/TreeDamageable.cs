using UnityEngine;

public class TreeDamageable : MonoBehaviour, IDamageable
{
    [SerializeField] int hp = 5;

    public void GetDamage()
    {
        if(hp <= 0) return;
        hp--;
        if(hp <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        Debug.Log("Tree Destrioyed");
    }

}
