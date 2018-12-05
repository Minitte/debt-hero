using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour {

    public static TimeManager instance;

    // The directional light that represents the sun
    public Light sun;

    /// <summary>
    /// Time scale compared to real life
    /// (i.e. 60f means 60x faster than real life
    /// so 1 hour in game is 1 minute in real life
    /// </summary>
    public float timeScale = 60f;

    // Seconds in a day, currently set to real life seconds
    public float secondsPerDay = 86400f;


    // Sun intensity and multiplier
    public float sunInitialIntensity;
    public float sunIntensityMultiplier;

    // Text fields to update on time change
    public Text timeCounter;
    public Text dayNumber;

    // Current time
    public float currentTime;
    public int currentHour;
    public int currentMinute;

    // Number of days passed since game start.Starts at 1
    public int days = 1;

    // Event for when day changes
    public delegate void OnDayChangeEvent();
    public event OnDayChangeEvent DayChange;

    // Event for when hour changes
    public delegate void OnHourChangeEvent();
    public event OnHourChangeEvent HourChange;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

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

        // When the current time is greater than 1 (24*1), one day has passed, so reset time to 0 and increment days
        if (currentTime >= 1) {
            days += 1;
            currentTime = 0;
            dayNumber.text = "Day " + days;
            DayChange();
        }
    }

    // Updates sun position according to time
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

    // Updates the text fields to display time
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
