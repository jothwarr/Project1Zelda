using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

public class LoadWorld : MonoBehaviour {
	World overworld;
	public static Dictionary<string, GameObject> prefabdict;
	public static GameObject defaulttile;

	// Use this for initialization
	void Start () {
		string path = Application.dataPath + "/overworld.xml";
		XmlSerializer serializer = new XmlSerializer (typeof(World));
		FileStream stream = new FileStream (path, FileMode.Open);
		overworld = serializer.Deserialize (stream) as World;
		stream.Close ();
		initializeprefabdict ();
		overworld.Initialize ();
	}
	
	public void initializeprefabdict() {
		defaulttile = (GameObject) Resources.Load ("BlankTile");
		prefabdict = new Dictionary<string, GameObject> ();
		prefabdict.Add ("02", defaulttile);
		prefabdict.Add ("18", (GameObject) Resources.Load ("CaveTile"));
		prefabdict.Add ("24", (GameObject) Resources.Load ("CaveTile"));
		prefabdict.Add ("43", (GameObject) Resources.Load ("TreeTile"));
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
		foreach (Tile tile in Tiles) {
			tile.Initialize(x, y);
		}
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
		position = new Vector3 (16f * x + this.x, -11f * y - this.y, 0);
		GameObject tile;
		if (!LoadWorld.prefabdict.TryGetValue (value, out tile))
						tile = LoadWorld.defaulttile;
		tileprefab = (GameObject) Object.Instantiate (tile, position, Quaternion.identity);
	}
}