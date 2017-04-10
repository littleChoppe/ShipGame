using UnityEngine;
using System.Collections;

[System.Serializable]
public struct Skill{
    public string _name;
    public bool _learned;
    public int _level;
    public float _castTimeEnd;
    public float _cooldownEnd;
    public float _buffTimeEnd;

    public Skill(SkillTemplate template)
    {
        _name = template.name;
        _learned = template.LearnedDefault;
        _level = 1;
        _castTimeEnd = _cooldownEnd = _buffTimeEnd = Time.deltaTime;
    }

    public bool TemplateExists()
    {
        return SkillTemplate.Dict.ContainsKey(_name);
    }

    public string Category
    {
        get { return SkillTemplate.Dict[_name].Category; }
    }

    public int Damage
    {
        get { return SkillTemplate.Dict[_name].Levels[_level - 1].Damage; }
    }

    public float CastTime
    {
        get { return SkillTemplate.Dict[_name].Levels[_level - 1].CastTime; }
    }

    public float CoolDown
    {
        get { return SkillTemplate.Dict[_name].Levels[_level - 1].CoolDown; }
    }

    public float CastRange
    {
        get { return SkillTemplate.Dict[_name].Levels[_level - 1].CastRange; }
    }

    public float AoeRadius
    {
        get { return SkillTemplate.Dict[_name].Levels[_level - 1].AoeRadius; }
    }

    public int ManaCast
    {
        get { return SkillTemplate.Dict[_name].Levels[_level - 1].ManaCost; }
    }

    public int HealsHP
    {
        get { return SkillTemplate.Dict[_name].Levels[_level - 1].HealsHP; }
    }

    public int HealsMP
    {
        get { return SkillTemplate.Dict[_name].Levels[_level - 1].HealsMP; }
    }
    public float BuffTime
    {
        get { return SkillTemplate.Dict[_name].Levels[_level - 1].BuffTime; }
    }

    public int BuffsHPMax
    {
        get { return SkillTemplate.Dict[_name].Levels[_level - 1].BuffsHPMax; }
    }

    public int BuffsMPMax
    {
        get { return SkillTemplate.Dict[_name].Levels[_level - 1].BuffsMPMax; }
    }

    public int BuffsDamage
    {
        get { return SkillTemplate.Dict[_name].Levels[_level - 1].BuffsDamage; }
    }

    public int BuffsDefense
    {
        get { return SkillTemplate.Dict[_name].Levels[_level - 1].BuffsDefense; }
    }

    public bool FollowupDefaultAttack
    {
        get { return SkillTemplate.Dict[_name].FollowupDefaultAttack; }
    }

    public int RequireLevel
    {
        get { return SkillTemplate.Dict[_name].Levels[_level].RequireLevel; }
    }

    public int MaxLevel
    {
        get {
            Debug.Log("修改了最大级数的代码");
            return SkillTemplate.Dict[_name].Levels[SkillTemplate.Dict[_name].Levels.Length -1].RequireLevel; }
    }

    public int UpgradeRequireLevel
    {
        get { return _level < MaxLevel ? SkillTemplate.Dict[_name].Levels[_level].RequireLevel : 0; }
    }

    public Projectile _Projectile
    {
        get { return SkillTemplate.Dict[_name].Levels[_level - 1]._Projectile; }
    }

    public string ToolTip()
    {
        string tip = SkillTemplate.Dict[_name].ToolTip;
        Debug.Log("技能介绍还没写完");
        return tip;
    }

    public float CastTimeRemaining()
    {
        return Time.time < _castTimeEnd ? _castTimeEnd - Time.time : 0f;
    }

    public bool IsCasting()
    {
        return CastTimeRemaining() > 0f;
    }

    public float CoolDownRemaining()
    {
        return Time.time < _cooldownEnd ? _cooldownEnd - Time.time : 0f;
    }

    public float BuffTimeRemaining()
    {
        return Time.time < _buffTimeEnd ? _buffTimeEnd - Time.time : 0f;
    }

    public bool IsReady()
    {
        return CoolDownRemaining() == 0f;
    }
}
