using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PathologicalGames;

public enum TeamType
{
    Good,
    Evil,
}
public abstract class Unit : MonoBehaviour {
    [Header("Team")]
    public TeamType Team = TeamType.Good;

    public int _hp = 100;
    public abstract int MaxHp { get; }
    public int Hp
    {
        get 
        {
            return Mathf.Min(_hp, MaxHp);
        }
        set
        {
            _hp = Mathf.Clamp(value, 0, MaxHp);
            if (_hp == 0)
                OnDeath();
            else
                _hpBar.UpdateHpBar();
        }
    }

    [SerializeField]
    protected bool _canHpRecovery = true;
    [SerializeField]
    protected int _hpRecoveryRate = 1;

    private int _mp;
    public abstract int MaxMp { get; }
    public int Mp
    {
        get
        {
            return Mathf.Min(_mp, MaxMp);
        }
        set
        {
            _mp = Mathf.Clamp(value, 0, MaxMp);
        }
    }

    [SerializeField]
    protected bool _canMpRecovery = true;
    [SerializeField]
    protected int _mpRecoveryRate = 1;
    public abstract int Defence { get; }
    public abstract int Attack { get; }

    [HideInInspector]
    public Collider TriggerCollider;


    [Header("Target")]
    public Unit Target = null;  //攻击目标

    [Header("Skill")]
    public SkillTemplate[] Template;
    public List<Skill> Skills = new List<Skill>();
    protected int _skillCurrent = -1;

    HpBar _hpBar;
    [HideInInspector]
    public List<Unit> _enemies = new List<Unit>();


    protected virtual void Awake()
    {
        TriggerCollider = GetComponentInChildren<Collider>();
        _hpBar = GetComponentInChildren<HpBar>();
    }
    protected virtual void Start()
    {
        InvokeRepeating("Recovery", 1f, 10f);

        foreach (var t in Template)
            Skills.Add(new Skill(t));

    }

    //void Update()
    //{
    //    _state = UpadteState();
    //}

    //protected abstract string UpadteState();
    void Recovery()
    {
        if (Hp == 0)
            CancelInvoke("Recovery");

        if (_canHpRecovery) Hp += _hpRecoveryRate;
        if (_canMpRecovery) Mp += _mpRecoveryRate;
    }

    public float HpPrecent()
    {
        return (Hp != 0 && MaxHp != 0) ? (float)Hp / MaxHp : 0.0f;
    }


    /// <summary>
    /// 朝向目标，水平面旋转，所以Y保持不变
    /// </summary>
    /// <param name="pos">面朝对象的位置</param>
    public void LookAtY(Vector3 pos)
    {
        transform.LookAt(new Vector3(pos.x, transform.position.y, pos.z));
    }

    /// <summary>
    /// 复活
    /// </summary>
    public void Revive()
    {
        Hp = MaxHp;
        Mp = MaxMp;
    }

    /// <summary>
    /// 进入攻击范围
    /// </summary>
    /// <param name="target"></param>
    public abstract void OnAggro(Unit target);


    //public bool CastCheckDistance(Skill skill)
    //{
    //    return Target != null &&
    //        Mathf.Abs(Vector3.Distance(transform.position , Target.transform.position)) <= skill.CastRange;
    //}

    /// <summary>
    /// 检测技能是否可用
    /// </summary>
    /// <param name="skill"></param>
    /// <returns></returns>
    public bool CastCheckSelf(Skill skill)
    {
        return Hp > 0 &&
            skill.IsReady() &&
            Mp >= skill.ManaCast;
    }

    /// <summary>
    /// 检测技能投放对象是否可行
    /// </summary>
    /// <param name="skill"></param>
    /// <returns></returns>
    public bool CastCheckTarget(Skill skill)
    {
        switch(skill.Category)
        {
            case "Attack":
                {
                    return Target != null &&
                        Target != this &&
                        CanAttackType(Target.GetType()) &&
                        Target.Hp > 0 &&
                        Target.Team != this.Team;

                }
            case "Heal":
                {
                    if (Target != null &&
                        Target != this &&
                        Target.GetType() == this.GetType() &&
                        Target.Team == this.Team )
                    {
                        return Target.Hp > 0;
                    }
                    else
                    {
                        Target = this;
                        return true;
                    }
                }
                
            case "Buff":
                {
                    Target = this;
                    return true;
                }
            default:
                {
                    Debug.LogWarning("错误的技能类型");
                    return false;
                }
        }
    }
    public abstract bool CanAttackType(System.Type t);

    public void CastSkill(Skill skill)
    {
        if (CastCheckSelf(skill) && CastCheckTarget(skill))
        {
            Debug.Log("投放技能" + _skillCurrent);
            Mp -= skill.ManaCast;
            switch(skill.Category)
            {
                case "Attack":
                    {
                        if (skill._Projectile == null)
                        {
                            DealDamageAt(Target,  Attack + skill.Damage, skill.AoeRadius);
                        }
                        else
                        {
                            var pm = transform.FindRecursively("ProjectileMount");
                            if (pm != null)
                            {
                                var go = PoolManager.Pools["Projectile"].Spawn(skill._Projectile.gameObject, pm.position, Quaternion.identity);
                                var pro = go.GetComponent<Projectile>();
                                pro.Caster = this;
                                pro.Target = Target;
                                pro.Damage = Attack + skill.Damage;
                                pro.AoeRadius = skill.AoeRadius;
                            }
                            else
                            {
                                Debug.LogWarning("没有发射口，不能进行射击");
                            }
                        }
                    }
                    break;

                case "Heal":
                    {
                        Target.Hp += skill.HealsHP;
                        Target.Mp += skill.HealsMP;
                    }
                    break;

                case "Buff":
                    skill._buffTimeEnd = Time.time + skill.BuffTime;
                    break;
            }
            skill._cooldownEnd = Time.time + skill.CoolDown;
            Skills[_skillCurrent] = skill;
        }
        else
        {
            _skillCurrent = -1;
        }
    }

    public virtual HashSet<Unit> DealDamageAt(Unit target, int demage, float aoeRadius = 0f)
    {
        var enemies = new HashSet<Unit>();
        enemies.Add(target);

        var colliders = Physics.OverlapSphere(target.transform.position, aoeRadius);
        foreach(Collider c in colliders)
        {
            var candidate = c.GetComponent<Unit>();
            if (candidate != null &&
                candidate != this &&
                candidate.Hp > 0 &&
                candidate.Team != this.Team &&
                Vector3.Distance(candidate.transform.position, target.transform.position) < aoeRadius)
            {
                enemies.Add(candidate);
            }
        }

        foreach (var e in enemies)
        {
            var dmg = Mathf.Max(demage - e.Defence, 1);
            e.Hp -= dmg;
            e.transform.GetComponentInChildren<HpBar>(true).ShowHpBar();
            Debug.Log("这里应该显示掉血量和金币数");
        }
        return enemies;
    }

    public abstract void ExitAggro(Unit target);
    protected abstract void OnDeath();
}
