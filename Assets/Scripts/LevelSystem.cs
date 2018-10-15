using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem : MonoBehaviour
{
    public int currentLevel;
    public float currentLevelEXP;
    public float nextLevelEXP;
    public float remainingEXP;
    private GameObject player;
    public ParticleSystem LevelEffect;
    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        currentLevel = 1;
        currentLevelEXP = 0;
        remainingEXP = EXPCurve(2);
        nextLevelEXP = EXPCurve(2);
    }

    // Update is called once per frame
    void Update()
    {

        currentLevelEXP = player.GetComponent<CharacterStats>().exp - EXPCurve(currentLevel);
        remainingEXP = nextLevelEXP - currentLevelEXP;
        if(remainingEXP <= 0)
        {
            LevelUp();
        }
    }

    void GainEXP(float exp)
    {
       player.GetComponent<CharacterStats>().exp += exp; 
    }

    float EXPCurve(int level)
    {
        return (level ^ 2 + level) / 2 * 100 - (level * 100);
    }

    void LevelUp()
    {
        LevelEffect.Play();
        currentLevel += 1;
        player.GetComponent<BaseClass>().GainStats();
    }
}

    
