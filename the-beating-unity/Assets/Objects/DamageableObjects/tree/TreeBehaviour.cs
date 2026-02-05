using UnityEngine;

public class TreeBehaviour : MonoBehaviour
{
    bool haveMeat = false;
    [SerializeField] GameObject wholeTree;
    [SerializeField] GameObject bottomTree;
    [SerializeField] GameObject topTree;

    public void Death()
    {
        wholeTree.SetActive(false);
        bottomTree.SetActive(true);
        topTree.SetActive(true);
        if (!haveMeat)
        {
            
        }
    }
}
