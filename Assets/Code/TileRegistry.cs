using System.Collections.Generic;
using System;

public static class TileRegistry {
    public static readonly Dictionary<string, RegistryObject<Tile>> Tiles = new Dictionary<string, RegistryObject<Tile>>();

    public static readonly RegistryObject<Tile> Air = Register("air", builder => builder.DisableCollision().Build());
    public static readonly RegistryObject<Tile> Grass = Register("grass", builder => builder.Build());
    public static readonly RegistryObject<Tile> Dirt = Register("dirt", builder => builder.Build());
    public static readonly RegistryObject<Tile> DirtGrass = Register("dirt_grass", builder => builder.Build());
    public static readonly RegistryObject<Tile> Rock0 = Register("rock_0", builder => builder.DisableCollision().Build());
    public static readonly RegistryObject<Tile> Rock1 = Register("rock_1", builder => builder.DisableCollision().Build());
    public static readonly RegistryObject<Tile> Rock2 = Register("rock_2", builder => builder.DisableCollision().Build());
    public static readonly RegistryObject<Tile> Rock3 = Register("rock_3", builder => builder.DisableCollision().Build());
    public static readonly RegistryObject<Tile> Rock4 = Register("rock_4", builder => builder.DisableCollision().Build());
    public static readonly RegistryObject<Tile> Rock5 = Register("rock_5", builder => builder.DisableCollision().Build());
    public static readonly RegistryObject<Tile> Rock6 = Register("rock_6", builder => builder.DisableCollision().Build());
    public static readonly RegistryObject<Tile> Rock7 = Register("rock_7", builder => builder.DisableCollision().Build());
    public static readonly RegistryObject<Tile> Rock8 = Register("rock_8", builder => builder.DisableCollision().Build());
    public static readonly RegistryObject<Tile> Rock9 = Register("rock_9", builder => builder.DisableCollision().Build());
    public static readonly RegistryObject<Tile> CliffEdge0 = Register("cliff_edge", builder => builder.DisableCollision().Build());
    public static readonly RegistryObject<Tile> CliffEdge1 = Register("cliff_edge_1", builder => builder.DisableCollision().Build());
    public static readonly RegistryObject<Tile> TopGrass = Register("top_grass", builder => builder.DisableCollision().Build());
    public static readonly RegistryObject<Tile> IronFence = Register("iron_fence", builder => builder.DisableCollision().Build());
    public static readonly RegistryObject<Tile> Gavestone0 = Register("gravestone_0", builder => builder.DisableCollision().Build());
    public static readonly RegistryObject<Tile> Gravestone1Bottom = Register("gravestone_1_bottom", builder => builder.DisableCollision().Build());
    public static readonly RegistryObject<Tile> Gravestone1Top = Register("gravestone_1_top", builder => builder.DisableCollision().Build());
    public static readonly RegistryObject<Tile> Gravestone2 = Register("gravestone_2", builder => builder.DisableCollision().Build());
    public static readonly RegistryObject<Tile> Ladder = Register("ladder", builder => builder.DisableCollision().Build());
    public static readonly RegistryObject<Tile> LadderGrass = Register("ladder_grass", builder => builder.DisableCollision().Build());

    private static RegistryObject<Tile> Register(string name, Func<Tile.Builder, Tile> tile) {
        var obj = new RegistryObject<Tile>(name, () => tile.Invoke(new Tile.Builder(name)));
        Tiles.Add(name, obj);
        return obj;
    }
}