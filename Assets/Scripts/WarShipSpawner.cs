using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PathologicalGames;

public class WarShipSpawner : MonoBehaviour {

    [SerializeField]
    WarShip[] _warShips;

    [SerializeField]
    float _startTime = 3f;  //开始时间

    [SerializeField]
    float _waveTime = 50f;   //每波兵的间隔时间

    [SerializeField]
    float _interval = 2f;   //生成每个兵之间的间隔时间

    [SerializeField]
    Transform _goal;    //目标终点

    [SerializeField]
    string NavMeshAreaPreferred = ""; // 路线

	void Start () 
    {
        StartCoroutine(Spawn());
	}

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(_startTime);
        while(true)
        {
            for (int i = 0; i < _warShips.Length; i++)
            {
                Transform go = PoolManager.Pools["WarShips"].Spawn( _warShips[i].gameObject, transform.position, Quaternion.identity);
                go.name = _warShips[i].name;
                go.GetComponent<WarShip>().Goal = _goal;
                go.GetComponentInChildren<HpBar>().HideHpBar();

                var index = NavMesh.GetAreaFromName(NavMeshAreaPreferred);
                if (index != -1)
                {
                    go.GetComponentInChildren<NavMeshAgent>().SetAreaCost(index, 1);
                }
                yield return new WaitForSeconds(_interval);
            }
            yield return new WaitForSeconds(_waveTime);
        }

    }
}
