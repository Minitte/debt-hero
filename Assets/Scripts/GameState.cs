
public class GameState {
    
    /// <summary>
    ///  state of the game
    /// </summary>
    public enum State {
        PLAYING,
        MENU
    }

    /// <summary>
    /// highest floor reached
    /// </summary>
    public static int floorReached;

    /// <summary>
    /// Current floor
    /// </summary>
    public static int currentFloor;

    /// <summary>
    /// Current game state
    /// </summary>
    public static State currentState;

    /// <summary>
    /// Blocked default constructor
    /// </summary>
    private GameState() {}
}