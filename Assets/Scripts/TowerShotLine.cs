using UnityEngine;
using System.Collections;

public class TowerShotLine : MonoBehaviour {

    LineRenderer _lineRender;
    Transform _startPoint;
    Unit _towerSelf;
    void Awake()
    {
        _lineRender = GetComponent<LineRenderer>();
        _towerSelf = GetComponent<Unit>();
        _startPoint = transform.FindRecursively("ProjectileMount");
    }

    void Update()
    {
        if (_towerSelf.Target == null)
        {
            _lineRender.enabled = false;
            return;
        }
        LineToTarget(_towerSelf.Target);

    }

    void LineToTarget(Unit target)
    {
        _lineRender.enabled = true;
        _lineRender.SetPosition(0, _startPoint.position);
        _lineRender.SetPosition(1, target.transform.position);
    }
}
