using UnityEngine;

public class HandResourceInventory : MonoBehaviour
{
    [SerializeField] int meatCount = 0;
    [SerializeField] int maxMeatCount = 3;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static HandResourceInventory resourceInventory;

    void Start()
    {
        resourceInventory = this;
    }

    public bool TryToIncrementMeatCount()
    {
        if(meatCount < maxMeatCount)
        {
            meatCount++;
            return true;
        }
        return false;
    }
}
