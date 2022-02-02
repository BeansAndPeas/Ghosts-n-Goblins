using System;
using System.Collections.Generic;

[Serializable]
public class Level {
    private string[] placement;
    private Keys keys;

    public string[] GetPlacements() {
        return this.placement;
    }

    public Keys GetKeyValues() {
        return this.keys;
    }

    [Serializable]
    public class Keys {
        public string key, value;
    }
}