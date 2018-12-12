using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorClass : BaseClass {
    // Use this for initialization
    void Start() {
        /// <summary>
        ///  Warrior is classID 1
        /// </summary>
        classID = 1;
        /// <summary>
        /// Base bonus stat for Warrior
        /// </summary>
        bonusHP = 0;
        bonusMP = 0;
        bonusPhysAtt = 0;
        bonusMagAtt = 0;
        bonusPhysDef = 0;
        bonusMagDef = 0;
        bonusCrit = 0;
        bonusCritDmg = 0;

        /// <summary>
        /// Stat increases per level for Warrior
        /// </summary>
        perLevelHP = 30;
        perLevelMP = 15;
        perLevelPhysAtt = 4;
        perLevelMagAtt = 1;
        perLevelPhysDef = 3;
        perLevelMagDef = 1;
        perLevelCrit = 0;
        perLevelCritDmg = 0;

        /// <summary>
        /// Skill IDs for Warrior
        /// </summary>
        skill1 = 0;
        skill2 = 1;
        skill3 = 2;
        skill4 = 3;

        InitiateClass();
    }
}
