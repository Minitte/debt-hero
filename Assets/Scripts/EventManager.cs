﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {

    public TimeManager timeManager;

    public EventSpec[] eventSpecs;

    // Use this for initialization
    void Start () {
        timeManager.DayChange += CheckDayEvents;

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void CheckDayEvents() {
        foreach (EventSpec e in eventSpecs) {
            if (e.recurring) {
                if (timeManager.days % e.day == e.dayOffset && timeManager.days != 1) {
                    StartEvent(e.eventID);
                }
            }
        }


    }

    private void StartEvent(int e) {
        switch (e) {
            case 0:
                PlayerManager.instance.GetComponent<CharacterInventory>().gold -= 100;
                break;
        }
    }
}

[Serializable]
public class EventSpec {
    public float chance;

    public bool recurring;

    public int day;

    public int dayOffset;

    public int time;

    public int eventID;
}