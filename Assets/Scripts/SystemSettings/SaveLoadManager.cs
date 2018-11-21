﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;



public class SaveLoadManager : MonoBehaviour {

    /// <summary>
    /// Using Singleton design pattern to ensure only 1 instance.
    /// </summary>
    public static SaveLoadManager instance;

    /// <summary>
    /// eventManager to get the Time manager to pull the tiem from
    /// </summary>
    public EventManager eventManager;

    /// <summary>
    /// Item database
    /// </summary>
    public ItemDatabase itemDatabase;

    /// <summary>
    /// Setting up the saveLoadManager variable.
    /// </summary>
    public void Awake() {
        if(instance == null) {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }else {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Saves the file to the slot
    /// </summary>
    /// <param name="slot"></param>
    public void Save(int slot) {
        CharacterStats stats = PlayerManager.instance.GetComponent<CharacterStats>();
        CharacterInventory inv = PlayerManager.instance.GetComponent<CharacterInventory>();
        TimeManager time = eventManager.timeManager;

        GameData gameData = new GameData();

        gameData.playerCurrenthp = stats.currentHp;
        gameData.playerMaxhp = stats.maxHp;
        gameData.playerCurrentmp = stats.currentMp;
        gameData.playerMaxmp = stats.maxMp;
        gameData.playerCurrentmp = stats.maxMp;
        gameData.playerPhysatk = stats.physAtk;
        gameData.playerPhysdef = stats.physDef;
        gameData.playerMagicatk = stats.magicAtk;
        gameData.playerMagicdef = stats.magicDef;
        gameData.playerExp = stats.exp;
        gameData.playerLevel = stats.level;
        gameData.playerGold = inv.gold;
       
        gameData.items = new List<ItemSafeFormat>();

        //Saves the player's Inventory.
        for (int i = 0; i < inv.itemRows.Length; i++) {
            for (int j = 0; j < inv.itemRows[i].items.Length; j++) {
                ItemBase item = inv.itemRows[i].items[j];

                if (item != null) {
                    ItemSafeFormat itemSav = new ItemSafeFormat(item.properties.itemID, item.properties.quantity, new ItemSlot(i, j));
                    gameData.items.Add(itemSav);
                }
            }
        }

        gameData.currentTime = time.currentTime;
        gameData.currentHour = time.currentHour;
        gameData.currentMinute =time.currentMinute;
        gameData.days = time.days;
        gameData.floorReached = PlayerProgress.floorReached;

        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = new FileStream(Application.persistentDataPath + "/save" + slot + ".dat", FileMode.OpenOrCreate);

        bf.Serialize(fs, gameData);
        fs.Close();
    }

    /// <summary>
    /// Attempts to load the save file from the slot
    /// </summary>
    /// <param name="slot"></param>
    /// <returns>True=Successfully loaded False=Failed to load</returns>
    public bool LoadGameState(int slot) {
        GameData gameData = LoadGameData(slot);

        if (gameData == null) {
            return false;
        }        

        CharacterStats stats = PlayerManager.instance.GetComponent<CharacterStats>();
        CharacterInventory inv = PlayerManager.instance.GetComponent<CharacterInventory>();
        TimeManager time = eventManager.timeManager;

        stats.currentHp = gameData.playerCurrenthp;
        stats.maxHp = gameData.playerMaxhp;
        stats.currentMp = gameData.playerCurrentmp;
        stats.maxMp = gameData.playerMaxmp;
        stats.maxMp = gameData.playerCurrentmp;
        stats.physAtk = gameData.playerPhysatk;
        stats.physDef = gameData.playerPhysdef;
        stats.magicAtk = gameData.playerMagicatk;
        stats.magicDef = gameData.playerMagicdef;
        stats.exp = gameData.playerExp;
        stats.level = gameData.playerLevel;

        inv.gold = gameData.playerGold;

        time.currentTime = gameData.currentTime;
        time.currentHour = gameData.currentHour;
        time.currentMinute = gameData.currentMinute;
        time.days = gameData.days;
        PlayerProgress.floorReached = gameData.floorReached;

        foreach (ItemSafeFormat item in gameData.items) {
            ItemSlot itemSlot = item.slot;
            inv.itemRows[itemSlot.row].items[itemSlot.col] = itemDatabase.GetNewItem(item.id, item.qty);
        }

        return true;
    }

    /// <summary>
    /// Attempts to load the game data
    /// </summary>
    /// <param name="slot"></param>
    /// <returns></returns>
    public GameData LoadGameData(int slot) {
        if (File.Exists(Application.persistentDataPath + "/save" + slot + ".dat")) {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = File.Open(Application.persistentDataPath + "/save" + slot + ".dat", FileMode.Open, FileAccess.Read);

            GameData data = (GameData)bf.Deserialize(fs);
            fs.Close();

            return data;
        }

        return null;
    }
	
}

/// <summary>
/// class used to handle Player Inventory.
/// </summary>
[Serializable]
public class ItemSafeFormat {

    public ItemSafeFormat(int id, int qty, ItemSlot slot) {
        this.id = id;
        this.qty = qty;
        this.slot = slot;
    }

    /// <summary>
    /// Id of the item. 
    /// </summary>
    public int id;

    /// <summary>
    /// Quantity of the item.
    /// </summary>
    public int qty;

    /// <summary>
    /// Location of the item in Player's Inventory.
    /// </summary>
    public  ItemSlot slot;
}

/// <summary>
/// The save/load game data.
/// </summary>
[Serializable]
public class GameData {

    /// <summary>
    /// The Player's Current Heath.
    /// </summary>
    public float playerCurrenthp;

    /// <summary>
    /// The Player's Maximum Heath.
    /// </summary>
    public float playerMaxhp;

    /// <summary>
    /// The Player's Current Mana.
    /// </summary>
    public float playerCurrentmp;

    /// <summary>
    /// The Player's Maximum Mana.
    /// </summary>
    public float playerMaxmp;

    /// <summary>
    /// The Player's Physical Attack.
    /// </summary>
    public float playerPhysatk;

    /// <summary>
    /// The Player's Physical Defense.
    /// </summary>
    public float playerPhysdef;

    /// <summary>
    /// The Player's Magic Attack.
    /// </summary>
    public float playerMagicatk;

    /// <summary>
    /// The Player's Magic Defense.
    /// </summary>
    public float playerMagicdef;

    /// <summary>
    /// The Player's Experience.
    /// </summary>
    public float playerExp;

    /// <summary>
    /// The Player's Level.
    /// </summary>
    public int playerLevel;

    /// <summary>
    /// The Player's Gold.
    /// </summary>
    public int playerGold;

    /// <summary>
    /// The Player's Inventory Items.
    /// </summary>
    public List<ItemSafeFormat> items;

    /// <summary>
    /// The in-game time.
    /// </summary>
    public float currentTime;

    /// <summary>
    /// The in-game Hour.
    /// </summary>
    public int currentHour;

    /// <summary>
    /// The in-game Minute.
    /// </summary>
    public int currentMinute;

    /// <summary>
    /// The in-game Days.
    /// </summary>
    public int days;

    /// <summary>
    /// The Floor Reached.
    /// </summary>
    public int floorReached;
  
  
    
}
