namespace Weav_App.DTOs
{
    public enum UnitsEnums
    {
        Unknown = 0,
        
        // Weight
        Kg = 1,
        Gr = 2,
        Mg = 3,
        Lb = 4,       // Pound
        Oz = 5,       // Ounce

        // Volume
        L = 10,
        Ml = 11,
        Cl = 12,
        Gal = 13,     // Gallon
        Qt = 14,      // Quart
        Pt = 15,      // Pint
        FlOz = 16,    // Fluid Ounce

        // Quantity-based
        Piece = 20,
        Pcs = 21,
        Pack = 22,
        Box = 23,
        Bag = 24,
        Bottle = 25,
        Jar = 26,
        Can = 27,
        Tray = 28,
        Roll = 29,

        // Others
        Dozen = 30,
        Bunch = 31,
        Pair = 32,
        Set = 33,
        Sheet = 34
    }
}