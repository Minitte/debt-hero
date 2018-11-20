using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem : MonoBehaviour
{
    //public int currentLevel;
    public float currentLevelExp;
    //public float nextLevelExp;
    public float remainingExp;
    private GameObject player;
    public ParticleSystem LevelEffect;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<CharacterStats>().level = 1;
        currentLevelExp = 0;
        remainingExp = ExpCurve(2);
        player.GetComponent<CharacterStats>().maxExp = ExpCurve(2);
    }

    // Update is called once per frame
    void Update()
    {
        currentLevelExp = player.GetComponent<CharacterStats>().exp - ExpCurve(player.GetComponent<CharacterStats>().level);
        remainingExp = player.GetComponent<CharacterStats>().maxExp - player.GetComponent<CharacterStats>().exp;
        if(remainingExp <= 0)
        {
            LevelUp();
        }
    }

    // Calculates the Exp needed for a level (Linear)
    float ExpCurve(int level)
    {
        return ((Mathf.Pow((level-1),2) + level-1) / 2 * 100);
    }

    //Used when the player levels up
    void LevelUp()
    {
        LevelEffect = PlayerManager.instance.localPlayer.transform.Find("LevelUp").GetComponent<ParticleSystem>();
        LevelEffect.Play();
        player.GetComponent<CharacterStats>().LevelUp();
        player.GetComponent<CharacterStats>().maxExp = ExpCurve(player.GetComponent<CharacterStats>().level + 1);
        player.GetComponent<BaseClass>().GainStats();
    }
}

    
