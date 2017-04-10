using UnityEngine;
using System.Collections;

public class Tower : Unit {


    [Header("Hp")]
    [SerializeField]
    int _hpMax = 100;
    public override int MaxHp
    {
        get { return _hpMax; }
    }

    public override int MaxMp
    {
        get { return 0; }
    }
    [Header("Defence")]
    [SerializeField]
    private int _baseDefence = 1;
    public override int Defence
    {
        get { return _baseDefence; }
    }
    [Header("Attack")]
    [SerializeField]
    private int _baseAttack = 1;
    public override int Attack
    {
        get { return _baseAttack; }
    }

    protected override void Awake()
    {
        base.Awake();
        Hp = MaxHp;
        Mp = MaxMp;
    }

    //protected override string UpadteState()
    //{
    //    if (State == "IDLE") return UpdateIdle();
    //    if (State == "CASTING") return UpdateCasting();
    //    if (State == "DEAD") return UpdateDead();
    //    return "IDLE";
    //}

    //string UpdateIdle()
    //{
    //    if (EventDied())
    //    {
    //        Debug.Log("自己死了");
    //        OnDeath();
    //        _skillCurrent = -1;
    //        return "DEAD";
    //    }

    //    if (EventTargetDied())
    //    {
    //        Debug.Log("对象死亡");
    //        Target = null;
    //        _skillCurrent = -1;
    //        return "IDLE";
    //    }

    //    if (EventTargetTooFarToAttack())
    //    {
    //        Debug.Log("太远攻击不到");
    //        Target = null;
    //        _skillCurrent = -1;
    //        return "IDLE";
    //    }

    //    if (EventSkillRequest())
    //    {
    //        Debug.Log("准备攻击");
    //        var skill = Skills[_skillCurrent];
    //        if (CastCheckSelf(skill) && CastCheckTarget(skill))
    //        {
    //            skill._castTimeEnd = Time.time + skill.CastTime;
    //            Skills[_skillCurrent] = skill;
    //            return "CASTING";
    //        }
    //        else
    //        {
    //            Target = null;
    //            _skillCurrent = -1;
    //            return "IDLE";
    //        }
    //    }

    //    if (EventAggro())
    //    {
    //        Debug.Log("进入攻击范围");
    //        if (Skills.Count > 0) _skillCurrent = 0;
    //        else Debug.LogWarning(name + "没有技能可用");
    //        return "IDLE";
    //    }
    //    return "IDLE";
    //}

    //string UpdateCasting()
    //{
    //    if (EventDied())
    //    {
    //        OnDeath();
    //        _skillCurrent = -1;
    //        return "DEAD";
    //    }
    //    if (EventTargetDisappeared())
    //    {
    //        Target = null;
    //        _skillCurrent = -1;
    //        return "IDLE";
    //    }

    //    if (EventTargetDied())
    //    {
    //        Debug.Log("对象死亡");
    //        Target = null;
    //        _skillCurrent = -1;
    //        return "IDLE";
    //    }
    //    if (EventSkillFinished())
    //    {
    //        CastSkill(Skills[_skillCurrent]);
    //        if (Target.Hp == 0)
    //        {
    //            Target = null;
    //        }
    //        _skillCurrent = -1;
    //        return "IDLE";
    //    }
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
    //void OnDeath()
    //{
    //    Destroy(gameObject);
    //}

    //bool EventTargetTooFarToAttack()
    //{
    //    return Target != null &&
    //        _skillCurrent >= 0 &&
    //        _skillCurrent < Skills.Count &&
    //        !CastCheckDistance(Skills[_skillCurrent]);
    //}

    //bool EventAggro()
    //{
    //    return Target != null && Target.Hp > 0;
    //}

    /// <summary>
    /// 判断进入攻击范围的对象是否可以成为攻击的目标对象
    /// </summary>
    /// <param name="go"></param>
    //public override void OnAggro(Unit go)
    //{
    //    if (go == null ||
    //        go.Hp <= 0 ||
    //        !CanAttackType(go.GetType()) ||
    //        go.Team == this.Team)
    //        return;

    //    if (Target == null)
    //    {
    //        Target = go;
    //    }
       
    //}

    //bool EventSkillRequest()
    //{
    //    return _skillCurrent >= 0 && _skillCurrent < Skills.Count;
    //}

    //bool EventSkillFinished()
    //{
    //    return _skillCurrent >= 0 &&
    //        _skillCurrent < Skills.Count &&
    //        Skills[_skillCurrent].CastTimeRemaining() == 0f;
    //}

    //bool EventTargetDisappeared()
    //{
    //    return Target == null;
    //}

    //bool EventTargetDied()
    //{
    //    return Target == null || Target.Hp == 0;
    //}

    public override bool CanAttackType(System.Type t)
    {
        return t == typeof(WarShip);
    }


    # region    新尝试

    protected override void Start()
    {
        base.Start();
        if (Skills.Count > 0) _skillCurrent = 0;
        else Debug.LogWarning(name + "没有技能可用");
    }
    Unit GetTarget()
    {
        for (int i = 0; i < _enemies.Count; i++)
        {
            if (_enemies[i].Hp == 0)
                _enemies.Remove(_enemies[i]);
        }
        if (_enemies.Count > 0)
            return _enemies[0];
        else
            return null;
    }

    public override void OnAggro(Unit go)
    {
        if (go != null &&
             go.Hp > 0 &&
             CanAttackType(go.GetType()) &&
             go.Team != this.Team  &&
            !_enemies.Contains(go))
            _enemies.Add(go);
        if (_enemies.Count == 1)
            StartInvoke();
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

    void AutoAttack()
    {
        CastSkill(Skills[_skillCurrent]);
    }

    public override void ExitAggro(Unit target)
    {
        if (!_enemies.Contains(target))
            return;
        _enemies.Remove(target);
    }

    protected override void OnDeath()
    {
        Destroy(gameObject);
    }
    #endregion
}
