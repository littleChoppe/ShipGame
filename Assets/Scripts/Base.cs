using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class Base : Unit {

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

    public override int Attack
    {
        get { return 0; }
    }

    private Animator _animator;
    [Header("Death")]
    [SerializeField]
    GameObject _deathEffect;

    protected override void Awake()
    {
        base.Awake();
        _animator = GetComponent<Animator>();
    }

    protected override void Start()
    {
        Hp = MaxHp;
        Mp = MaxMp;
        base.Start();
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
    //        OnDeath();
    //        return "DEAD";
    //    }
    //    return "IDLE";
    //}

    //string UpdateCasting()
    //{
    //    Debug.Log("CASTING");
    //    return "CASTING";
    //}

    //string UpdateDead()
    //{
    //    Debug.Log("DEAD");
    //    return "DEAD";
    //}

    //bool EventDied()
    //{
    //    return Hp == 0;
    //}
     protected override void OnDeath()
    {
        _animator.SetBool("Death", true);
        if (_deathEffect != null)
        {
            Instantiate(_deathEffect, transform.position, Quaternion.identity);
            _deathEffect = null;
        }
    }

    public override void OnAggro(Unit target)
    {
        
    }

    public override bool CanAttackType(System.Type t)
    {
        return t == typeof(WarShip);
    }

    public override void ExitAggro(Unit target)
    {

    }
}
