using UnityEngine;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Linq;
using System;

public class LevelReader : MonoBehaviour {
    public const string PATH = @"Assets/Levels/";
    public GameObject tilePrefab;
    private readonly string[] levelSplitter = { "Levels/" };

    void Start() {        
        // Get all files in the Levels directory
        string[] files = Directory.GetFiles(PATH);

        // Loop through each file
        foreach(var file in files) {
            // Check if it has the 'json' extension
            if(file.EndsWith(".json")) {
                string levelName = file.Replace(".json", "").Split(levelSplitter, System.StringSplitOptions.RemoveEmptyEntries)[1];
                string jsonText = File.ReadAllText(file);
                JObject @object = JObject.Parse(jsonText);
                
                IEnumerable<JToken> placementObjs = @object.GetValue("placement").Values();
                Dictionary<string, string> keysObjs = @object.GetValue("keys").ToObject<Dictionary<string, string>>();

                string[] placements = new string[placementObjs.Count()];
                var tileKeys = new Dictionary<string, Tile>();

                var keys = new List<string>();
                int yLayer = 0;
                foreach(JToken elem in placementObjs) {
                    string current = placements[yLayer++] = elem.Value<string>().Trim();
                    char[] currentKeys = current.ToCharArray();
                    foreach(char c in currentKeys) {
                        if(!keys.Contains(c.ToString()) && !Char.IsWhiteSpace(c)) {
                            keys.Add(c.ToString());
                        }
                    }
                }

                keys.Add(" ");
                keysObjs.Add(" ", "air");

                foreach(string key in keys) {
                    if(!keysObjs.Keys.Contains(key))
                        throw new InvalidOperationException("Missing key: '" + key + "' in level: '" + levelName + "'");
                    string tileName;
                    keysObjs.TryGetValue(key, out tileName);
                    var tile = new Tile(tileName);
                    tileKeys.Add(key, tile);
                }

                int xPos = 0;
                int yPos = 0;
                foreach(string y in placements.Reverse()) {
                    foreach(string x in y.ToCharArray().Select(c => c.ToString())) {
                        GameObject tileObj = GameObject.Instantiate(tilePrefab);
                        Tile tile;
                        if(tileKeys.TryGetValue(x, out tile)) {
                            Vector3 position = tileObj.transform.position;
                            tileObj.name = tile.name;
                            position.x = xPos * tileObj.transform.localScale.x;
                            position.y = yPos * tileObj.transform.localScale.y;
                            tileObj.transform.position = position;
                        } else
                            throw new InvalidOperationException("Missing key: '" + x + "' in level: '" + levelName + "'");

                        xPos++;
                    }

                    yPos++;
                    xPos = 0;
                }
            }
        }
    }
}
