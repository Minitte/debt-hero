using System;
using UnityEngine;

/// <summary>
/// A class holding x and z coordinate data
/// </summary>
[Serializable]
public class XZCoordinate {

    /// <summary>
    /// A XZCoordinate at (0, 0)
    /// </summary>
    /// <value></value>
    public static XZCoordinate zero {
        get {
            return new XZCoordinate(0, 0);
        }
    }

    /// <summary>
    /// X coordinate
    /// </summary>
    public int x;

    /// <summary>
    /// Z coordinate
    /// </summary>
    public int z;

    /// <summary>
    /// Constructor for an XZCoordinate
    /// </summary>
    /// <param name="x"></param>
    /// <param name="z"></param>
    public XZCoordinate(int x, int z) {
        this.x = x;
        this.z = z;
    }

    /// <summary>
    /// Creates a new Vector2 with x = x and y = z
    /// </summary>
    /// <returns></returns>
    public Vector2 ToVector2() {
        return new Vector2(x ,z);
    }

    /// <summary>
    /// Creates a new Vector3 with x = x, y = 0 and z = z
    /// </summary>
    /// <returns></returns>
    public Vector3 ToVector3() {
        return new Vector3(x, 0, z);
    }

    #region operators

    /// <summary>
    /// sums x and z of both left hand side and right hand side XZCoordinates into a new XZCoordinate object.
    /// </summary>
    /// <param name="lhs"></param>
    /// <param name="rhs"></param>
    /// <returns></returns>
    public static XZCoordinate operator+(XZCoordinate lhs, XZCoordinate rhs) {
        return new XZCoordinate(lhs.x + rhs.x, lhs.z + rhs.z);
    }

    /// <summary>
    /// subtracks left hand side xz with right hand side zx XZCoordinates into a new XZCoordinate object.
    /// </summary>
    /// <param name="lhs"></param>
    /// <param name="rhs"></param>
    /// <returns></returns>
    public static XZCoordinate operator-(XZCoordinate lhs, XZCoordinate rhs) {
        return new XZCoordinate(lhs.x - rhs.x, lhs.z - rhs.z);
    }

    /// <summary>
    /// scales x and z of left hand side by right hand side into a new XZCoordinate object
    /// </summary>
    /// <param name="lhs"></param>
    /// <param name="rhs"></param>
    /// <returns></returns>
    public static XZCoordinate operator*(XZCoordinate lhs, int rhs) {
        return new XZCoordinate(lhs.x * rhs, lhs.z * rhs );
    }

    /// <summary>
    /// scales x and z of left hand side by right hand side into a new XZCoordinate object
    /// </summary>
    /// <param name="lhs"></param>
    /// <param name="rhs"></param>
    /// <returns></returns>
    public static XZCoordinate operator*(XZCoordinate lhs, float rhs) {
        return new XZCoordinate((int) (lhs.x * rhs), (int) (lhs.z * rhs));
    }

    #endregion

    
}