using UnityEngine;

public interface IInteractable
{
    public void Interaction(PlayerManager player);
}

public interface IHighlightable
{
    public void Highlight();
}
