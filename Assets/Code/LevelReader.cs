using UnityEngine;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Linq;
using System;

public class LevelReader : MonoBehaviour {
    private const string PATH = @"Assets/Levels/";
    public GameObject tilePrefab;
    private readonly string[] levelSplitter = { "Levels/" };
    public float levelWidth;

    private void Start() {
        // Get all files in the Levels directory
        string[] files = Directory.GetFiles(PATH);

        // Loop through each file
        foreach (var file in files) {
            // Check if it has the 'json' extension
            if (!file.EndsWith(".json")) return;
            
            string levelName = file.Replace(".json", "").Split(levelSplitter, System.StringSplitOptions.RemoveEmptyEntries)[1];
            string jsonText = File.ReadAllText(file);
            JObject @object = JObject.Parse(jsonText);

            IEnumerable<JToken> placementObjs = @object.GetValue("placement").Values();
            Dictionary<string, string> keysObjs = @object.GetValue("keys").ToObject<Dictionary<string, string>>();

            string[] placements = new string[placementObjs.Count()];
            var tileKeys = new Dictionary<string, Tile>();

            var keys = new List<string>();
            int yLayer = 0;
            foreach (JToken elem in placementObjs) {
                string current = placements[yLayer++] = elem.Value<string>().Trim();
                char[] currentKeys = current.ToCharArray();
                foreach (char c in currentKeys) {
                    if (!keys.Contains(c.ToString()) && !char.IsWhiteSpace(c)) {
                        keys.Add(c.ToString());
                    }
                }
            }

            keys.Add(" ");
            keysObjs.Add(" ", "air");

            int longest = GetLongestString(placements);
            for (var y = 0; y < placements.Length; y++) {
                placements[y] = PlaceAirInString(placements[y], longest);
            }

            foreach (string key in keys) {
                string realKey = key;
                if (char.IsWhiteSpace(key.ToCharArray()[0])) realKey = " ";
                if (!keysObjs.Keys.Contains(realKey))
                    throw new InvalidOperationException("Missing key: '" + realKey + "' in level: '" + levelName + "'");
                if (!keysObjs.TryGetValue(realKey, out string tileName)) 
                    throw new InvalidOperationException("Missing key: '" + realKey + "' in level: '" + levelName + "'");
                if(!TileRegistry.Tiles.ContainsKey(tileName)) 
                    throw new NotSupportedException("There is no tile registered by the name: '" + tileName + "'.");
                if (!TileRegistry.Tiles.TryGetValue(tileName, out RegistryObject<Tile> tile)) continue;
                tileKeys.Add(realKey, tile.Get());
            }

            var xPos = 0;
            var yPos = 0;
            foreach (string y in placements.Reverse()) {
                foreach (string x in y.ToCharArray().Select(c => c.ToString())) {
                    if (tileKeys.TryGetValue(x, out Tile tile)) {
                        if (!tile.IsAir()) {
                            var tileObj = GameObject.Instantiate(tilePrefab);
                            Vector3 position = tileObj.transform.position;
                            tileObj.name = tile.GetName();
                            Vector2 scale = tileObj.transform.localScale;
                            position.x = xPos * scale.x * .16f;
                            position.y = yPos * scale.y * .16f;
                            tileObj.transform.position = position;
                            Sprite sprite = Resources.Load<Sprite>("Textures/" + tile.GetName());
                            tileObj.GetComponent<SpriteRenderer>().sprite = sprite;

                            if (!tile.CanCollide())
                                Destroy(tileObj.GetComponent<BoxCollider2D>());
                        }
                    } else
                        throw new InvalidOperationException("Missing key: '" + x + "' in level: '" + levelName + "'");

                    xPos++;
                }
                yPos++;
                
                if (levelWidth != 0) {
                    levelWidth = xPos * 16;
                }
                
                xPos = 0;
            }
        }
    }

    private static int GetLongestString(params string[] strs) => strs.OrderByDescending(str => str.Length).First().Length;
    private static string PlaceAirInString(string layer, int longestLayer) {
        if (layer.Length >= longestLayer) return layer;
        int spaces = longestLayer - layer.Length;
        int padLeftAmount = spaces / 2 + layer.Length;
        return layer.PadLeft(padLeftAmount, ' ').PadRight(longestLayer, ' ');
    }
}
