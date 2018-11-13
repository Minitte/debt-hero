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

    public ItemSafeFormat itemSav;

    TimeManager time;
    CharacterInventory inv;
    CharacterStats stats;

    public GameDatabase gameDatabase;
    ItemDatabase itemDatabase;

    /// <summary>
    /// Setting up the saveLoadManager variable.
    /// </summary>
    public void Awake() {
        if(saveLoadManager == null) {
            DontDestroyOnLoad(gameObject);
            saveLoadManager = this;
        }else {
            Destroy(gameObject);
        }
    }
    public void Start() {
        inv = playerManager.GetComponent<CharacterInventory>();
        stats = playerManager.GetComponent<CharacterStats>();
        time = eventManager.timeManager;
        itemDatabase = gameDatabase.itemDatabase;
        //Save(0);
        //Load(0);
    }

    public void Save(int slot) {

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
                    itemSav = new ItemSafeFormat(item.properties.itemID, item.properties.quantity, new ItemSlot(i, j));
                    gameData.items.Add(itemSav);
                }
            }
        }

        gameData.currentTime = time.currentTime;
        gameData.currentHour = time.currentHour;
        gameData.currentMinute =time.currentMinute;
        gameData.days = time.days;
        gameData.floorReached = GameState.floorReached;

        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = new FileStream(Application.persistentDataPath + "/save" + slot + ".dat", FileMode.OpenOrCreate);

        bf.Serialize(fs, gameData);
        fs.Close();

    }

    public void Load(int slot) {
        if (File.Exists(Application.persistentDataPath + "/save" + slot + ".dat")) {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream fs = File.Open(Application.persistentDataPath + "/save.dat", FileMode.Open, FileAccess.Read);

            gameData = (GameData)bf.Deserialize(fs);
            fs.Close();

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
            GameState.floorReached = gameData.floorReached;
            foreach (ItemSafeFormat item in gameData.items) {
                ItemSlot itemSlot = item.slot;
                inv.itemRows[itemSlot.row].items[itemSlot.col] = itemDatabase.GetNewItem(item.id, item.qty);
            }

            
        }
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
