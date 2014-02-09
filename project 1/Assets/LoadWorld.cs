using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

public class LoadWorld : MonoBehaviour {
	World overworld;
	static Dictionary<string, string> prefabdict;

	// Use this for initialization
	void Start () {
		//Debug.LogWarning ("Entered LoadWorld.Start()");
		string path = Application.dataPath + "/overworld.xml";
		XmlSerializer serializer = new XmlSerializer (typeof(World));
		FileStream stream = new FileStream (path, FileMode.Open);
		overworld = serializer.Deserialize (stream) as World;
		stream.Close ();
		//Debug.LogWarning ("Read in overworld.xml");
		//Debug.LogWarning ("Number of Rooms: " + overworld.Rooms.Count);
		initializeprefabdict ();
		overworld.Initialize ();
	}
	
	public void initializeprefabdict() {
		prefabdict = new Dictionary<string, string> ();
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

	[XmlAttribute("val")]
	public string value;

	[XmlAttribute("block")]
	public string block;

	public Vector3 position;

	GameObject tileprefab;

	public void Initialize (int x, int y) {
		position = new Vector3 (16f * x + this.x, 11f * y + this.y, 0);
		tileprefab = (GameObject) Object.Instantiate (Resources.Load("BlankTile"), position, Quaternion.identity);
	}
}