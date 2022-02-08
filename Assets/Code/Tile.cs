using System;

public class Tile {
    private readonly string name;
    private readonly bool noCollide;

    public Tile(string name, bool noCollide) {
        this.name = name;
    }
    public Tile(string name) {
        this(name, false);
    }

    public bool IsAir() {
        return this.name.ToLower().Equals("air");
    }

    public bool CanCollide() {
        return !this.noCollide;
    }

    public string GetName(bool lowercase) {
        return lowercase ? this.name.ToLower() : this.name;
    }

    public string GetName() {
        return GetName(false);
    }
}