using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

public class LoadWorld : MonoBehaviour {
	World overworld;

	// Use this for initialization
	void Start () {
		Debug.LogWarning ("Entered LoadWorld.Start()");
		string path = Application.dataPath + "/overworld.xml";
		XmlSerializer serializer = new XmlSerializer (typeof(World));
		FileStream stream = new FileStream (path, FileMode.Open);
		overworld = serializer.Deserialize (stream) as World;
		stream.Close ();
		Debug.LogWarning ("Read in overworld.xml");
		Debug.LogWarning ("Number of Rooms: " + overworld.Rooms.Count);
	}
}

[XmlRoot("World")]
public class World {
	[XmlArray("Rooms")]
	[XmlArrayItem("Room")]
	public List<Room> Rooms = new List<Room> ();
}

public class Room {
	[XmlAttribute("x")]
	public int x;

	[XmlAttribute("y")]
	public int y;

	[XmlArray("Tiles")]
	[XmlArrayItem("Tile")]
	public List<Tile> Tiles = new List<Tile> ();
}

public class Tile {
	[XmlAttribute("x")]
	public int x;

	[XmlAttribute("y")]
	public int y;

	[XmlAttribute("val")]
	public string value;
}