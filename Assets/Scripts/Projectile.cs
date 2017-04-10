using UnityEngine;
using System.Collections;
using PathologicalGames;

public class Projectile : MonoBehaviour {

    [SerializeField]
    float _speed = 10f;

    [HideInInspector]
    public Unit Target;
    [HideInInspector]
    public Unit Caster;
    [HideInInspector]
    public int Damage;
    [HideInInspector]
    public float AoeRadius;

    void OnSpawned()
    {
        Update();
        //StartCoroutine(Shooting());
    }
    void Update()
    {
        if (Target != null && Caster != null)
        {
            var goal = Target.GetComponentInChildren<Collider>().bounds.center;
            transform.LookAt(goal);
            transform.position = Vector3.MoveTowards(transform.position, goal, _speed);

            if (transform.position == goal)
            {
                if (Target.Hp > 0)
                {
                    Caster.DealDamageAt(Target, Damage);

                    PoolManager.Pools["Projectile"].Despawn(this.transform);
                }
            }
        }
    }

    //IEnumerator Shooting()
    //{
    //    while (Target != null && Caster != null)
    //    {
    //        var goal = Target.GetComponentInChildren<Collider>().bounds.center;
    //        transform.LookAt(goal);
    //        transform.position = Vector3.MoveTowards(transform.position, goal, _speed);

    //        if (transform.position == goal)
    //        {
    //            if (Target.Hp > 0)
    //            {
    //                yield return StartCoroutine(DealDamege());

    //                PoolManager.Pools["Projectile"].Despawn(this.transform);
    //            }
    //        }
    //    }

    //}

    //IEnumerator DealDamege()
    //{
    //    Caster.DealDamageAt(Target, Damage);
    //    yield return null;
    //}

}
