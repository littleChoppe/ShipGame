using UnityEngine;
using System.Collections;
using PathologicalGames;
public class WarShip : Unit {

    [Header("HP")]
    [SerializeField] int _maxHp = 100;
    public override int MaxHp
    {
        get { return _maxHp; }
    }
    public override int MaxMp
    {
        get { return 0; }
    }

    [Header("Demage")]
    [SerializeField]
    int _demage = 2;
    public override int Attack
    {
        get { return _demage; }
    }

    [Header("Defence")]
    [SerializeField]
    int _defence = 1;
    public override int Defence
    {
        get { return _defence; }
    }

    [Header("Movement")]
    [SerializeField]
    float _attackDis = 8.0f;

    [HideInInspector]
    public Transform Goal;  //行走的最终目标

    [Header("Daeth")]
    public GameObject DeathEffect;

    private bool isFollowing = false;
    Transform followTarget = null;

    NavMeshAgent _agent;


    protected override void Awake()
    {
        base.Awake();
        _agent = GetComponent<NavMeshAgent>();
    }



    public void OnSpawned()
    {
        Hp = MaxHp;
        Mp = MaxMp;
        if (Skills.Count > 0) _skillCurrent = 0;
        else Debug.LogWarning(name + "没有技能可用");
    }

    void Update()
    {
        //if (isFollowing)
        //{
        //    if (followTarget == null)
        //    {
        //        isFollowing = false;
        //        StartInvoke();
        //    }

        //    else
        //        FollowTarget();
        //}
        //else if (Goal != null)
        //{
        //    GoToEnemyBase();
        //}

        if (Goal != null)
            GoToEnemyBase();
    }

    //protected override string UpadteState()
    //{
    //    if (State == "IDLE") return UpdateIdle();
    //    if (State == "MOVING") return UpdateMoving();
    //    if (State == "CASTING") return UpdateCasting();
    //    if (State == "DEAD") return UpdateDead();
    //    return  "IDLE";
    //}

    //string UpdateIdle()
    //{
    //    if (EventDied())
    //    {
    //        Debug.Log("死了");
    //        OnDeath();
    //        _skillCurrent = -1;
    //        return "DEAD";
    //    }
    //    if (Goal != null)
    //    {
    //        Agent.stoppingDistance = 10f;
    //        Agent.destination = Goal.GetComponent<Collider>().ClosestPointOnBounds(transform.position);
    //        return "MOVING";
    //    }
    //    return "IDLE";
    //}

    //string UpdateMoving()
    //{
    //    if (EventDied())
    //    {
    //        OnDeath();
    //        _skillCurrent = -1;
    //        return "DEAD";
    //    }
    //    return "MOVING";
    //}

    //string UpdateCasting()
    //{
    //    return "CASTING";
    //}

    //string UpdateDead()
    //{
    //    return "DEAD";
    //}

    //bool EventDied()
    //{
    //    return Hp == 0;
    //}

    public bool IsMoving()
    {
        return (_agent.pathPending ||
            _agent.remainingDistance > _agent.stoppingDistance ||
            _agent.velocity != Vector3.zero);
    }

    protected override void OnDeath()
    {
        Target = null;
        if (DeathEffect != null)
            PoolManager.Pools["DeathEffect"].Spawn(DeathEffect, transform.position, transform.rotation);
        PoolManager.Pools["WarShips"].Despawn(this.transform);
    }

    public override void OnAggro(Unit obj)
    {
        //if (Hp > 0 &&
        //    obj != null &&
        //    obj.Hp > 0 &&
        //    obj.Team != this.Team &&
        //    CanAttackType(obj.GetType()) &&
        //    !_enemies.Contains(obj))
        //{
        //    _enemies.Add(obj);

        //    if (_enemies.Count == 1)
        //        StartInvoke();
        //}
    }

    public void StartInvoke()
    {
        if (IsInvoking("CheckEnemies"))
            return;
        InvokeRepeating("CheckEnemies", 0.1f, Skills[_skillCurrent].CastTime);
    }

    void CheckEnemies()
    {
        Target = GetTarget();
        if (Target == null)
        {
            Debug.Log("对象为空");
            CancelInvoke("CheckEnemies");

            return;
        }
        AutoAttack();
    }

    void FollowTarget()
    {
        if (followTarget == null)
            return;
        _agent.stoppingDistance = 6f;
        _agent.SetDestination(followTarget.GetComponent<Collider>().ClosestPointOnBounds(transform.position));
    }

    void GoToEnemyBase()
    {
        _agent.stoppingDistance = 10f;
        _agent.SetDestination(Goal.GetComponent<Collider>().ClosestPointOnBounds(transform.position));
    }
    Unit GetTarget()
    {
        for (int i = 0; i < _enemies.Count; i++)
        {
            if (_enemies[i].Hp == 0)
                _enemies.Remove(_enemies[i]);
        }
        if (_enemies.Count > 0)
        {
            //排序
            for (int i = 0; i < _enemies.Count; i++)
            {

            }

            if (Vector3.Distance(transform.position, _enemies[0].transform.position) > _attackDis)
            {
                isFollowing = true;
                CancelInvoke("CheckEnemies");
                return null;
            }
            else
                return _enemies[0];
        }
        else
            return null;
    }
    void AutoAttack()
    {
        CastSkill(Skills[_skillCurrent]);
    }

    public override bool CanAttackType(System.Type t)
    {
        return t == typeof(WarShip) || t == typeof(Tower) || t == typeof(Base);
    }

    public override void ExitAggro(Unit target)
    {
        //if (!_enemies.Contains(target))
        //    return;
        //_enemies.Remove(target);
    }
}
