using UnityEngine;

public interface IInventoryObserver
{
    void OnResourceChanged(ResourceType type, int newTotal);
    void OnTroopsChanged();
}
