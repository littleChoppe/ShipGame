using UnityEngine;
using System.Collections;
using PathologicalGames;

public class DestroyAfterSpawn : MonoBehaviour {

    public float existTime;
    public void OnSpawned()
    {
        PoolManager.Pools["DeathEffect"].Despawn(this.transform, existTime);
    }
}
