using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour {
    public Light sun;
    public float timeScale = 2f;
    public float secondsPerDay = 1800f;
    public int days = 1;
    public float sunInitialIntensity;
    public float sunIntensityMultiplier;
    public Text timeCounter;
    public Text dayNumber;

    public float currentTime;

    public int currentHour;
    public int currentMinute;
    // Use this for initialization
    void Start() {
        currentTime = 0;
        sunInitialIntensity = sun.intensity;
    }

    // Update is called once per frame
    void Update() {
        UpdateSun();
        currentTime += (Time.deltaTime / secondsPerDay) * timeScale;
        currentHour = Mathf.RoundToInt(24 * currentTime);
        currentMinute = Mathf.RoundToInt(60 * (24 * currentTime - Mathf.Floor(24 * currentTime)));
        updateTime();
        if (currentTime >= 1) {
            days += 1;
            currentTime = 0;
            dayNumber.text = "Day " + days;
        }
    }

    void UpdateSun() {
        sun.transform.localRotation = Quaternion.Euler((currentTime * 360f) - 90, 170, 0);

        if (currentTime <= 0.23f || currentTime >= 0.75f) {
            sunIntensityMultiplier = 0;
        }
        else if (currentTime <= 0.25f) {
            sunIntensityMultiplier = Mathf.Clamp01((currentTime - 0.23f) * (1 / 0.02f));
        }
        else if (currentTime >= 0.73f) {
            sunIntensityMultiplier = Mathf.Clamp01(1 - ((currentTime - 0.73f) * (1 / 0.02f)));
        }

        sun.intensity = sunInitialIntensity * sunIntensityMultiplier;
    }

    void updateTime() {
        if (currentHour >= 13) {
            if (currentHour - 12 < 10) {
                timeCounter.text = "0" + (currentHour - 12) + ":";
                if(currentMinute < 10) {
                    timeCounter.text += "0" + currentMinute + "PM";
                }
                else {
                    timeCounter.text += currentMinute + "PM";
                }
            }
            else {
                timeCounter.text = (currentHour - 12) + ":";
                if (currentMinute < 10) {
                    timeCounter.text += "0" + currentMinute + "PM";
                }
                else {
                    timeCounter.text += currentMinute + "PM";
                }
            }
        }
        else {
            if (currentHour < 10) {
                timeCounter.text = "0" + (currentHour) + ":";
                if (currentMinute < 10) {
                    timeCounter.text += "0" + currentMinute + "AM";
                }
                else {
                    timeCounter.text += currentMinute + "AM";
                }
            }
            else {
                timeCounter.text = (currentHour) + ":";
                if (currentMinute < 10) {
                    timeCounter.text += "0" + currentMinute + "AM";
                }
                else {
                    timeCounter.text += currentMinute + "AM";
                }
            }
        }
    }
}
