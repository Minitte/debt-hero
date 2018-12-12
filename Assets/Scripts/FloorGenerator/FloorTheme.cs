using UnityEngine;

public class FloorTheme {
    
    public enum Type {
        
        CAVE,
        FOREST,
        FIRE,
        
        SAFEZONE
        
    }

    /// <summary>
    /// Private constructor
    /// </summary>
    private FloorTheme () {}

    /// <summary>
    /// Gets the current floor theme enum 
    /// </summary>
    /// <returns></returns>
    public static FloorTheme.Type GetCurrentTheme() {
        int floor = PlayerProgress.currentFloor;

        Debug.Log(floor);

        int floorTheme = (int)(floor / 4);

        if ((floor + 1) % 4 == 0 && floor != 0) {
            return Type.SAFEZONE;
        }

        switch (floorTheme) {
            case 0 :
            return Type.CAVE;

            case 1:
            return Type.FOREST;

            case 2:
            return Type.FIRE;

            default:
            return Type.FIRE;
        }
    }

    /// <summary>
    /// Checks if the current floor theme is cave
    /// </summary>
    /// <returns></returns>
    public static bool IsCurrentlyCave() {
        return GetCurrentTheme() == Type.CAVE;
    }

    /// <summary>
    /// Checks if the current floor theme is forest
    /// </summary>
    /// <returns></returns>
    public static bool IsCurrentlyForest() {
        return GetCurrentTheme() == Type.FOREST;
    }

    /// <summary>
    /// Checks if the current floor theme is fire
    /// </summary>
    /// <returns></returns>
    public static bool IsCurrentlyFire() {
        return GetCurrentTheme() == Type.FIRE;
    }

     /// <summary>
    /// Checks if the current floor theme is safe zone
    /// </summary>
    /// <returns></returns>
    public static bool IsCurrentlySafeZone() {
        return GetCurrentTheme() == Type.SAFEZONE;
    }

}