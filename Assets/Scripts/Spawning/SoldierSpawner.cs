using UnityEngine;

public class SoldierSpawner : UnitSpawner
{
    [SerializeField] private GameObject[] soldierPrefabs;
    protected override GameObject[] Prefabs => soldierPrefabs;
}

