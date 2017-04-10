using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[CreateAssetMenu(fileName="New Skill", menuName="Skill", order=999)]
public class SkillTemplate : ScriptableObject {

    public string Category;
    public bool FollowupDefaultAttack;
    [TextArea(1, 30)]
    public string ToolTip;
    public Sprite Image;

    [System.Serializable]
    public struct SkillLevel
    {
        public int Damage;
        public float CastTime;
        public float AoeRadius;
        public float CoolDown;
        public float CastRange;
        public int ManaCost;
        public int HealsHP;
        public int HealsMP;
        public float BuffTime;
        public int BuffsHPMax;
        public int BuffsMPMax;
        public int BuffsDamage;
        public int BuffsDefense;
        public Projectile _Projectile;

        public int RequireLevel;
    }

    public SkillLevel[] Levels = new SkillLevel[] { new SkillLevel() };
    public bool LearnedDefault;

    static Dictionary<string, SkillTemplate> Cache = null;
    public static Dictionary<string, SkillTemplate> Dict
    {
        get
        {
            if (Cache == null)
            {
                Cache = Resources.LoadAll<SkillTemplate>("").ToDictionary(
                    //传入两个函数，第一个函数跟第二个函数都传入一个 skillTemplate 对象
                    //第一个函数返回对象名字，第二个函数返回对象本身
                    item => item.name, item => item);   
            }
            return Cache;
        }
    }
}
