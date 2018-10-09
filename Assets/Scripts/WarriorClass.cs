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
        bonusHP = 100;
        bonusMP = 0;
        bonusPhysAtt = 10;
        bonusMagAtt = 0;
        bonusPhysDef = 10;
        bonusMagDef = 0;
        bonusCrit = 0;
        bonusCritDmg = 0;

        /// <summary>
        /// Stat increases per level for Warrior
        /// </summary>
        perLevelHP = 10;
        perLevelMP = 5;
        perLevelPhysAtt = 0;
        perLevelMagAtt = 0;
        perLevelPhysDef = 5;
        perLevelMagDef = 0;
        perLevelCrit = 0;
        perLevelCritDmg = 0;
    }
}
