using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseClass : MonoBehaviour {

    /// <summary>
    /// Initial stats for selecting the class
    /// </summary>
    private float _bonusHP;
    private float _bonusMP;
    private float _bonusPhysAtt;
    private float _bonusMagAtt;
    private float _bonusPhysDef;
    private float _bonusMagDef;
    private float _bonusCrit;
    private float _bonusCritDmg;

    /// <summary>
    /// Stat increases per level for the class
    /// </summary>
    private float _perLevelHP;
    private float _perLevelMP;
    private float _perLevelPhysAtt;
    private float _perLevelMagAtt;
    private float _perLevelPhysDef;
    private float _perLevelMagDef;
    private float _perLevelCrit;
    private float _perLevelCritDmg;

    /// <summary>
    /// Skills
    /// </summary>
    private Skill _skill1;
    private Skill _skill2;
    private Skill _skill3;
    private Skill _skill4;

    /// <summary>
    /// Not implemented yet
    /// Will be used for leveling
    /// </summary>
    public void GainStats()
    {
        
    }
}
