using System.Collections.Generic;
using System;

public class TileRegistry {
    public static readonly Dictionary<string, RegistryObject> Tiles = new Dictionary<string, RegistryObject>();

    public static RegistryObject Register(string name, Func<Tile> tile) {
        var obj = new RegistryObject(name, tile);
        Tiles.Add(name, obj);
        return obj;
    }

    public class RegistryObject : Supplier<Tile> {
        public readonly string name;

        public RegistryObject(string name, Func<Tile> tile) {
            base(tile);
            this.name = name;
        }
    }
}