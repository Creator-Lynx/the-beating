using UnityEngine;

public class TreeBehaviour : MonoBehaviour
{
    [SerializeField] bool haveMeat = false;
    [SerializeField] GameObject wholeTree;
    [SerializeField] GameObject bottomTree;
    [SerializeField] GameObject topTree;
    [SerializeField] MeatInteractiveBehaviour[] meats;

    public void Death()
    {
        wholeTree.SetActive(false);
        bottomTree.SetActive(true);
        topTree.SetActive(true);
        if (haveMeat)
        {
            foreach (MeatInteractiveBehaviour meat in meats)
            {
                meat.EnableDropState();
            }
        }
    }
}
