using UnityEngine;

public class GameState {


    #region Events
    /// <summary>
    /// Delegate for state event
    /// </summary>
    /// <param name="curState"></param>
    public delegate void StateEvent();

    /// <summary>
    /// Event for state changed
    /// </summary>
    public static event StateEvent OnStateChanged;
    #endregion

    #region Constants
    /// <summary>
    /// Playing the game. Not in menu
    /// </summary>
    public static readonly int PLAYING = 0;
    
    /// <summary>
    /// In game menu
    /// </summary>
    public static readonly int MENU_INGAME = 1;

    /// <summary>
    /// In shop menu
    /// </summary>
    public static readonly int MENU_SHOP = 2;

    /// <summary>
    /// In a dialog
    /// </summary>
    public static readonly int DIALOG = 3;
    #endregion

    /// <summary>
    /// highest floor reached
    /// </summary>
    public static int floorReached;

    /// <summary>
    /// Current floor
    /// </summary>
    public static int currentFloor;

    /// <summary>
    /// Current game state. 
    /// </summary>
    public static int currentState { 
        get { 
            return _currentState; 
        } 
    }

    /// <summary>
    /// Current game state
    /// </summary>
    private static int _currentState;

    /// <summary>
    /// Blocked default constructor
    /// </summary>
    private GameState() {}

    /// <summary>
    /// Checks if the current state is in any menu or ui;
    /// </summary>
    /// <returns></returns>
    public static bool InAnyUIState() {
        return _currentState == MENU_INGAME || 
            _currentState == MENU_SHOP || 
            _currentState == DIALOG;
    }

    /// <summary>
    /// Sets the game state and fires events 
    /// </summary>
    /// <param name="state"></param>
    public static void SetState(int state) {

        _currentState = state;

        if (OnStateChanged != null) {
            OnStateChanged();
        }
    }
}