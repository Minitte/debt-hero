using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCaster : MonoBehaviour {
    /// <summary>
    /// For casting skills, has all the information on what each skill does
    /// Also manages skill cooldowns
    /// </summary>

    // Skill 1 Cooldown
    public int skill1ID;
    private float timeStamp1;
    public float coolDown1;
    public bool canCast1;

    // Skill 2 Cooldown
    public int skill2ID;
    private float timeStamp2;
    public float coolDown2;
    public bool canCast2;

    // Skill 3 Cooldown
    public int skill3ID;
    private float timeStamp3;
    public float coolDown3;
    public bool canCast3;

    // Skill 4 Cooldown
    public int skill4ID;
    private float timeStamp4;
    public float coolDown4;
    public bool canCast4;

    // Use this for initialization
    void Start () {
		canCast1 = canCast2 = canCast3 = canCast4 = true;
        timeStamp1 = timeStamp2 = timeStamp3 = timeStamp4 = Time.time;
}
	
	// Update is called once per frame
	void Update () {

        /// <summary>
        /// If skill time is less than current time, skill1 can be casted again
        /// </summary>
        if (timeStamp1 <= Time.time)
        {
            canCast1 = true;
        }

        /// <summary>
        /// If skill time is less than current time, skill2 can be casted again
        /// </summary>
        if (timeStamp2 <= Time.time)
        {
            canCast2 = true;
        }

        /// <summary>
        /// If skill time is less than current time, skill3 can be casted again
        /// </summary>
        if (timeStamp3 <= Time.time)
        {
            canCast3 = true;
        }

        /// <summary>
        /// If skill time is less than current time, skill4 can be casted again
        /// </summary>
        if (timeStamp4 <= Time.time)
        {
            canCast4 = true;
        }
    }

    /// <summary>
    /// Cast
    /// </summary>
    public void Cast(int skillNum, int skillID)
    {
        bool cast = false;
        switch (skillNum)
        {
            case 1:
                if (canCast1)
                {
                    cast = true;
                }
                break;
            case 2:
                if (canCast2)
                {
                    cast = true;
                }
                break;
            case 3:
                if (canCast3)
                {
                    cast = true;
                }
                break;
            case 4:
                if (canCast4)
                {
                    cast = true;
                }
                break;
        }
        if(cast)
        {
            if (Skill.GetInfo(skillID).type != -1)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>().SetBool("skill"+skillNum, true);
            }
        }
    }



}
