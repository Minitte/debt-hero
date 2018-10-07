using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseClass : MonoBehaviour {

    /// <summary>
    /// Initial stats for selecting the class
    /// </summary>
    public float bonusHP;
    public float bonusMP;
    public float bonusPhysAtt;
    public float bonusMagAtt;
    public float bonusPhysDef;
    public float bonusMagDef;
    public float bonusCrit;
    public float bonusCritDmg;

    /// <summary>
    /// Stat increases per level for the class
    /// </summary>
    public float perLevelHP;
    public float perLevelMP;
    public float perLevelPhysAtt;
    public float perLevelMagAtt;
    public float perLevelPhysDef;
    public float perLevelMagDef;
    public float perLevelCrit;
    public float perLevelCritDmg;

    /// <summary>
    /// Skills
    /// </summary>
    public Skill skill1;
    public Skill skill2;
    public Skill skill3;
    public Skill skill4;

    /// <summary>
    /// Not implemented yet
    /// Will be used for leveling
    /// </summary>
    public void GainStats()
    {
        
    }
}
