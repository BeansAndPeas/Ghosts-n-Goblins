using System;

public class Tile {
    private readonly string name;
    private readonly bool noCollide;

    private Tile(Builder builder) {
        this.name = builder.name;
        this.noCollide = !builder.canCollide;
    }

    public bool IsAir() {
        return this.name.ToLower().Equals("air");
    }

    public bool CanCollide() {
        return !this.noCollide;
    }

    public string GetName(bool lowercase = false) {
        return lowercase ? this.name.ToLower() : this.name;
    }

    public class Builder {
        internal readonly string name;
        internal bool canCollide = true;

        public Builder(string name) {
            this.name = name;
        }

        public Builder DisableCollision() {
            this.canCollide = false;
            return this;
        }

        public Tile Build() {
            return new Tile(this);
        }
    }
}