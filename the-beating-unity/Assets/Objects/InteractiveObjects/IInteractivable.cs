using UnityEngine;

public interface IInteractivable
{
    public void Interact()
    {
        Debug.Log("Default interaction");
    }

    public string GetInteractionHint()
    {
        return "Взаимодействовать";
    }
}
