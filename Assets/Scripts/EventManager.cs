using System;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {

    public static EventManager instance;

    // Time manager
    public TimeManager timeManager;

    // Amount of debt owed
    public int debtOwed = 0;

    // Total debt paid. THIS IS THE SCORE
    public int debtPaid = 0;

    // How often should the debt update?
    public int daysPerPayment = 7;

    // How many days do they have to pay their debt before the 
    // debt collectors show up? 
    public int daysToPay = 3;

    // Should we spawn debt collectors?
    public bool spawnDebtCollectors = false;


    // Set Event Manager instance
    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        else {
            Debug.Log("Found two EventManager Instances.. Destorying new one");
            Destroy(this.gameObject);
            return;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    // Use this for initialization
    void Start() {
        timeManager.DayChange += CheckDebt;
        timeManager = TimeManager.instance;
    }

    // Update is called once per frame
    void Update() {

    }

    // Check debt
    void CheckDebt() {
        if (timeManager.days % daysPerPayment == 0 && timeManager.days != 1) {
            debtOwed += DebtCurve(timeManager.days);
        }

        if (timeManager.days % (daysPerPayment + daysToPay) == 0 && debtOwed != 0) {
            spawnDebtCollectors = true;
        }
    }

    // Calculates the money needed to be paid back (Linear)
    public int DebtCurve(int days) {
        return (Mathf.RoundToInt((Mathf.Pow((days - 1), 2) + days - 1) / 2 * 100));
    }

    // Pay the debt
    public void PayDebt() {
        PlayerManager.instance.GetComponent<CharacterInventory>().gold -= debtOwed;
        debtPaid += debtOwed;
        debtOwed = 0;
        spawnDebtCollectors = false;

        // check name in case testing in editor
        string name = PlayerProgress.name == null ? "???" : PlayerProgress.name;

        //register score
        Scoreboard.RegisterScore(name, debtPaid);
    }
}