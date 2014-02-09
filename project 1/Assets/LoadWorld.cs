using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

public class LoadWorld : MonoBehaviour {
	World overworld;
	public static Dictionary<string, string> prefabdict;

	// Use this for initialization
	void Start () {
		Debug.LogWarning ("Entered LoadWorld.Start()");
		string path = Application.dataPath + "/smallworld.xml";
		XmlSerializer serializer = new XmlSerializer (typeof(World));
		FileStream stream = new FileStream (path, FileMode.Open);
		overworld = serializer.Deserialize (stream) as World;
		stream.Close ();
		Debug.LogWarning ("Read in overworld.xml");
		initializeprefabdict ();
		Debug.LogWarning ("Initialized tile dictionary");
		overworld.Initialize ();
		Debug.LogWarning ("Initialized overworld");
	}
	
	public void initializeprefabdict() {
		prefabdict = new Dictionary<string, string> ();
		prefabdict.Add ("02", "BlankTile");
		prefabdict.Add ("18", "CaveTile");
		prefabdict.Add ("1b", "TreeTile");
	}
}

[XmlRoot("World")]
public class World {
	[XmlArray("Rooms")]
	[XmlArrayItem("Room")]
	public List<Room> Rooms = new List<Room> ();

	public void Initialize () {
		foreach (Room room in Rooms) {
			room.Initialize();
		}
	}
}

public class Room {
	[XmlAttribute("x")]
	public int x;

	[XmlAttribute("y")]
	public int y;

	[XmlArray("Tiles")]
	[XmlArrayItem("Tile")]
	public List<Tile> Tiles = new List<Tile> ();

	public void Initialize() {
		Debug.LogWarning ("Started Room");
		foreach (Tile tile in Tiles) {
			tile.Initialize(x, y);
		}
		Debug.LogWarning ("Finished Room");
	}
}

public class Tile {
	[XmlAttribute("x")]
	public int x;

	[XmlAttribute("y")]
	public int y;

	[XmlAttribute("value")]
	public string value;

	[XmlAttribute("block")]
	public string block;

	public Vector3 position;

	GameObject tileprefab;

	public void Initialize (int x, int y) {
		Debug.LogWarning ("Started Tile");
		position = new Vector3 (16f * x + this.x, 11f * y + this.y, 0);
		string tiletype;
		if (value != null && LoadWorld.prefabdict.TryGetValue (value, out tiletype)) {
			tiletype = "BlankTile";
		}
		else {
			tiletype = "BlankTile";
		}
		tileprefab = (GameObject) Object.Instantiate (Resources.Load("BlankTile"), position, Quaternion.identity);
		Debug.LogWarning ("Finished Tile");
	}
}