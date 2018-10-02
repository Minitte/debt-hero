using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour {

    /// <summary>
    /// Damage event template.
    /// </summary>
    /// <param name="physAtkdamge">The amount of physical damage to take.</param>
    /// <param name="magicAtkdamage">The amount of magical damage to take.</param>
    public delegate void DamageEvent(float physAtkdamge, float magicAtkdamage);

    /// <summary>
    /// This damage event is called after the character has taken damage.
    /// </summary>
    public event DamageEvent OnDamageTaken;

    /// <summary>
    /// The character's current health point.
    /// </summary>
    public float currentHp;

    /// <summary>
    /// The character's maximum health point. 
    /// </summary>
    public float maxHp;

    /// <summary>
    /// The character's current mana point.
    /// </summary>
    public float currentMp;


    /// <summary>
    /// The character's maximum mana point.
    /// </summary>
    public float maxMp;

    /// <summary>
    /// The character's physical attack stat.
    /// </summary>
    public float physAtk;


    /// <summary>
    /// The character's magic attack stat.
    /// </summary>
    public float magicAtk;

    /// <summary>
    /// The character's physical defense stat. 
    /// </summary>
    public float physDef;

    /// <summary>
    /// The character's magic defense stat.
    /// </summary>
    public float magicDef;

    /// <summary>
    /// The character's level.
    /// </summary>
    public int level;

    /// <summary>
    /// The character's experience point.
    /// </summary>
    public float exp;

    /// <summary>
    /// Boolean to represent if a character is alive.
    /// </summary>
    public bool isAlive;


    /// <summary>
    /// Basic damage calculation function.
    /// </summary>
    /// <param name="physAtkdamge">The amount of physical damage to take.</param>
    /// <param name="magicAtkdamage">The amount of magical damage to take.</param>
    public void TakeDamage(float physAtkdamge, float magicAtkdamage) {

        if (physDef < physAtkdamge) {
            currentHp = currentHp - (physAtkdamge - physDef);
        }

        if (magicDef < magicAtkdamage) {
            currentHp = currentHp - (magicAtkdamage - magicDef);
        }

        if (currentHp > 0) {
            isAlive = true;
        } else {
            isAlive = false;
        }

        // if damage was taken, trigger OnDamageTaken event
        if (physDef < physAtkdamge || magicDef < magicAtkdamage) {
            if (OnDamageTaken != null) {
                OnDamageTaken(physAtkdamge, magicAtkdamage);
            }
        }
    }



}
