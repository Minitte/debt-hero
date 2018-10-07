using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorClass : BaseClass {

    // Use this for initialization
    void Start() {
        /// <summary>
        /// Base bonus stat for Warrior
        /// </summary>
        this._bonusHP = 100;
        this._bonusMP = 0;
        this._bonusPhysAtt = 10;
        this._bonusMagAtt = 0;
        this._bonusPhysDef = 10;
        this._bonusMagDef = 0;
        this._bonusCrit = 0;
        this._bonusCritDmg = 0;

        /// <summary>
        /// Stat increases per level for Warrior
        /// </summary>
        this._perLevelHP = 10;
        this._perLevelMP = 5;
        this._perLevelPhysAtt = 0;
        this._perLevelMagAtt = 0;
        this._perLevelPhysDef = 5;
        this._perLevelMagDef = 0;
        this._perLevelCrit = 0;
        this._perLevelCritDmg = 0;
    }
}
