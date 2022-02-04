using System;

public class Tile {
    public readonly string name;
    public Tile(string name) {
        this.name = name;
    }

    public bool IsAir() {
        return this.name.ToLower().Equals("air");
    }
}