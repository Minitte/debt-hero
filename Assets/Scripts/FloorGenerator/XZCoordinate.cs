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
    /// Constructor for an XZCoordinate
    /// </summary>
    /// <param name="x">float value rounded down</param>
    /// <param name="z">float value rounded down</param>
    public XZCoordinate(float x, float z) {
        this.x = (int) x;
        this.z = (int) z;
    }

    /// <summary>
    /// Copy constructor
    /// </summary>
    /// <param name="other"></param>
    public XZCoordinate(XZCoordinate other) {
        x = other.x;
        z = other.z;
    }

    /// <summary>
    /// Calulates the block distance. Sum of differences in x and z
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public int blockDistance(XZCoordinate other) {
        int blockDist = 0;
        
        blockDist += Math.Abs(x - other.x);
        blockDist += Math.Abs(z - other.z);

        return blockDist;
    }

    /// <summary>
    /// create a XZCoordinate with z + 1
    /// </summary>
    /// <returns></returns>
    public XZCoordinate up() {
        return new XZCoordinate(x, z + 1);
    }

    /// <summary>
    /// create a XZCoordinate with z - 1
    /// </summary>
    /// <returns></returns>
    public XZCoordinate down() {
        return new XZCoordinate(x, z - 1);
    }

    /// <summary>
    /// create a XZCoordinate with x - 1
    /// </summary>
    /// <returns></returns>
    public XZCoordinate left() {
        return new XZCoordinate(x - 1, z);
    }

    /// <summary>
    /// create a XZCoordinate with x + 1
    /// </summary>
    /// <returns></returns>
    public XZCoordinate right() {
        return new XZCoordinate(x + 1, z);
    }

    /// <summary>
    /// Creates a new Vector2 with x = x and y = z
    /// </summary>
    /// <returns></returns>
    public Vector2 toVector2() {
        return new Vector2(x ,z);
    }

    /// <summary>
    /// Creates a new Vector3 with x = x, y = 0 and z = z
    /// </summary>
    /// <returns></returns>
    public Vector3 toVector3() {
        return new Vector3(x, 0, z);
    }

    /// <summary>
    /// returns the coordinate as a string
    /// </summary>
    /// <returns></returns>
    public override string ToString() {
        return "x:" + x + " z:" + z;
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

    /// <summary>
    /// checks if the x and z of both lhs and rhs are equal
    /// </summary>
    /// <param name="lhs"></param>
    /// <param name="rhs"></param>
    /// <returns></returns>
    public static bool operator==(XZCoordinate lhs, XZCoordinate rhs) {
        return lhs.x == rhs.x && lhs.z == rhs.z;
    }

    /// <summary>
    /// checks if the x and z of both lhs and rhs are not equal
    /// </summary>
    /// <param name="lhs"></param>
    /// <param name="rhs"></param>
    /// <returns></returns>
    public static bool operator!=(XZCoordinate lhs, XZCoordinate rhs) {
        return lhs.x != rhs.x || lhs.z != rhs.z;
    }

    #endregion

    
}