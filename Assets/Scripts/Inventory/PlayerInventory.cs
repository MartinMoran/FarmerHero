using UnityEngine;
using System.Collections.Generic;

public enum TroopType
{
    Pike,
    Archer,
    Sword
}
public class PlayerInventory : MonoBehaviour
{
    private Dictionary<ResourceType, int> resources = new Dictionary<ResourceType, int>();
    private List<IInventoryObserver> observers = new List<IInventoryObserver>();
    [SerializeField] private int pikeCount = 0;
    public int PikeCount => pikeCount;
    [SerializeField] private int archerCount = 0;
    public int ArcherCount => archerCount;
    [SerializeField] private int swordCount = 0;
    public int SwordCount => swordCount;

    // Cambia la cantidad de tropas vivas por tipo y avisa al Canvas.
    public void ChangeTroopCount(TroopType type, int change)
    {
        switch (type)
        {
            case TroopType.Pike:
                pikeCount += change;
                break;
            case TroopType.Archer:
                archerCount += change;
                break;
            case TroopType.Sword:
                swordCount += change;
                break;
        }

        foreach (IInventoryObserver observer in observers)
        {
            observer.OnTroopsChanged();
        }
    }

    // Suma la cantidad recibida al recurso correspondiente.
    public void AddResource(ResourceType type, int amount)
    {
        if (resources.ContainsKey(type))
        {
            resources[type] += amount;
        }
        else
        {
            resources[type] = amount;
        }

        NotifyObservers(type, resources[type]);
    }

    // Devuelve la cantidad actual del recurso especificado. 
    public int GetResourceAmount(ResourceType type)
    {
        if (resources.ContainsKey(type))
        {
            return resources[type];
        }
        return 0;
    }

    public bool SpendResource (ResourceType type, int amount)
    {
        if (amount > 0 && GetResourceAmount(type) >= amount)
        {
            resources[type] -= amount;
            NotifyObservers(type, resources[type]);
            return true;
        }
        return false;
    }

    public void RegisterObserver(IInventoryObserver observer)
    {
        if (!observers.Contains(observer))
        {
            observers.Add(observer);
        }
    }

    public void UnregisterObserver(IInventoryObserver observer)
    {
        if (observers.Contains(observer))
        {
            observers.Remove(observer);
        }
    }

    private void NotifyObservers(ResourceType type, int newTotal)
    {
        foreach (var observer in observers)
        {
            observer.OnResourceChanged(type, newTotal);
        }
    }

    public void ResetInventory()
    {
        resources.Clear();
        pikeCount = 0;
        archerCount = 0;
        swordCount = 0;
    }

}
