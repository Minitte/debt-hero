using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour {

    public string skillName;
    public float coolDown;
    public string stat;
    public float duration;
    public float amount;
    public int type;

    public Skill()
    {
        skillName = "";
        coolDown = 0;
        stat = "";
        amount = 0;
        duration = 0;
        type = -1;
    }
}
