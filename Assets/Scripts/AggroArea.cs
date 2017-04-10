using UnityEngine;
using System.Collections;

/// <summary>
/// 获取所有进入范围的对象
/// </summary>
public class AggroArea : MonoBehaviour {

    Unit parent;

    void Awake()
    {
        parent = transform.parent.GetComponent<Unit>();
    }

    void OnTriggerEnter(Collider co)
    {
        Unit target = co.GetComponentInParent<Unit>();
        if (target == null)
            return;
        parent.OnAggro(target);
    }

    void OnTriggerExit(Collider co)
    {
        Unit target = co.GetComponentInParent<Unit>();
        if (target == null)
            return;
        parent.ExitAggro(target);
    }
}
