using System;

/// <summary>
/// An inner class to hold row and col data
/// </summary>
[Serializable]
public class ItemSlot {
    
    /// <summary>
    /// Row number
    /// </summary>
    public int row;

    // column number
    public int col;

    /// <summary>
    /// Constructor for an itemslot object
    /// </summary>
    /// <param name="row"></param>
    /// <param name="col"></param>
    public ItemSlot(int row, int col) {
        this.row = row;
        this.col = col;
    }

    /// <summary>
    /// Copy constructor
    /// </summary>
    /// <param name="other"></param>
    public ItemSlot(ItemSlot other) {
        this.row = other.row;
        this.col = other.col;
    }

    // override object.GetHashCode
    public override int GetHashCode() {
        string s = "r:" + row + "c:" + col;

        return s.GetHashCode();
    }

    // override object.Equals
    public override bool Equals(object obj) {
        //
        // See the full list of guidelines at
        //   http://go.microsoft.com/fwlink/?LinkID=85237
        // and also the guidance for operator== at
        //   http://go.microsoft.com/fwlink/?LinkId=85238
        //
        
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }
        
        ItemSlot other = (ItemSlot) obj;

        return row == other.row && col == other.col;
    }
}