using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;



public class SaveLoadManager : MonoBehaviour {
   
    /// <summary>
    /// PlayerManager used to read Player information.
    /// </summary>
    public PlayerManager playerManager;

    /// <summary>
    /// EventManager is used to read Time information.
    /// </summary>
    public EventManager eventManager;

    /// <summary>
    /// Using Singleton design pattern to ensure only 1 instance.
    /// </summary>
    private SaveLoadManager saveLoadManager;

    /// <summary>
    /// Class containing all game data.
    /// </summary>
    private GameData gameData = new GameData();


    /// <summary>
    /// Setting up the saveLoadManager variable.
    /// </summary>
    public void Awake() {
        if(saveLoadManager == null) {
            DontDestroyOnLoad(gameObject);
            saveLoadManager = this;
        }
    }

    public void Save() {

        CharacterStats stats = playerManager.GetComponent<CharacterStats>();
        CharacterInventory inv = playerManager.GetComponent<CharacterInventory>();
        TimeManager time = eventManager.GetComponent<TimeManager>();

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
       
        //Saves the player's Inventory.
        for (int i = 0; i < inv.itemRows.Length; i++) {
            for (int j = 0; j < inv.itemRows[i].items.Length; i++) {
                ItemSafeFormat itemSav = new ItemSafeFormat( inv.itemRows[i].items[j].properties.itemID
                                                           , inv.itemRows[i].items[j].properties.quantity
                                                           , new ItemSlot(i,j));
                gameData.items.Add(itemSav);
            }
        }

        gameData.currentTime = time.currentTime;
        gameData.currentHour = time.currentHour;
        gameData.currentMinute = time.currentMinute;
        gameData.days = time.days;

        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = new FileStream(Application.persistentDataPath + "/save.dat", FileMode.OpenOrCreate);

        bf.Serialize(fs, gameData);
        fs.Close();

    }
	
}

/// <summary>
/// Struct used to handle Player Inventory.
/// </summary>
public struct ItemSafeFormat {

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
  
  
    
}
