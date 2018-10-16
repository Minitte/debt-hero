using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem : MonoBehaviour
{
    //public int currentLevel;
    public float currentLevelEXP;
    public float nextLevelEXP;
    public float remainingEXP;
    private GameObject player;
    public ParticleSystem LevelEffect;
    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<CharacterStats>().level = 1;
        currentLevelEXP = 0;
        remainingEXP = EXPCurve(2);
        nextLevelEXP = EXPCurve(2);
    }

    // Update is called once per frame
    void Update()
    {
        currentLevelEXP = player.GetComponent<CharacterStats>().exp - EXPCurve(player.GetComponent<CharacterStats>().level);
        remainingEXP = nextLevelEXP - player.GetComponent<CharacterStats>().exp;
        if(remainingEXP <= 0)
        {
            LevelUp();
        }
    }

    //Adds EXP to the player
    void GainEXP(float exp)
    {
       player.GetComponent<CharacterStats>().exp += exp; 
    }

    // Calculates the EXP needed for a level (Linear)
    float EXPCurve(int level)
    {
        return ((Mathf.Pow((level-1),2) + level-1) / 2 * 100);
    }

    //Used when the player levels up
    void LevelUp()
    {
        LevelEffect.Play();
        player.GetComponent<CharacterStats>().level += 1;
        nextLevelEXP = EXPCurve(player.GetComponent<CharacterStats>().level + 1);
        player.GetComponent<BaseClass>().GainStats();
    }
}

    
