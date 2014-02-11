using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

public class LoadWorld : MonoBehaviour {
	public static World overworld;
	public static Dictionary<string, GameObject> prefabdict;
	public static GameObject defaulttile;

	public TextAsset overworldXML;

	// Use this for initialization
	void Start () {
		overworldXML = (TextAsset) Resources.Load ("overworld");
		XmlSerializer serializer = new XmlSerializer (typeof(World));
		TextReader reader = new StringReader (overworldXML.text);
		overworld = serializer.Deserialize (reader) as World;
		reader.Close ();
		initializeprefabdict ();
		overworld.loadRoom (7, 7);
	}
	
	public void initializeprefabdict() {
		defaulttile = (GameObject) Resources.Load ("Tile02");
		prefabdict = new Dictionary<string, GameObject> ();
		prefabdict.Add ("00", (GameObject) Resources.Load("Tile00"));
		prefabdict.Add ("01", (GameObject) Resources.Load("Tile01"));
		prefabdict.Add ("02", defaulttile);
		prefabdict.Add ("03", (GameObject) Resources.Load("Tile03"));
		prefabdict.Add ("04", (GameObject) Resources.Load("Tile04"));
		prefabdict.Add ("05", (GameObject) Resources.Load("Tile05"));
		prefabdict.Add ("06", (GameObject) Resources.Load("Tile06"));
		prefabdict.Add ("07", (GameObject) Resources.Load("Tile07"));
		prefabdict.Add ("08", (GameObject) Resources.Load("Tile08"));
		prefabdict.Add ("09", (GameObject) Resources.Load("Tile09"));
		prefabdict.Add ("0a", (GameObject) Resources.Load("Tile0a"));
		prefabdict.Add ("0b", (GameObject) Resources.Load("Tile0b"));
		prefabdict.Add ("0c", (GameObject) Resources.Load("Tile0c"));
		prefabdict.Add ("0d", (GameObject) Resources.Load("Tile0d"));
		prefabdict.Add ("0e", (GameObject) Resources.Load("Tile0e"));
		prefabdict.Add ("0f", (GameObject) Resources.Load("Tile0f"));
		prefabdict.Add ("10", (GameObject) Resources.Load("Tile10"));
		//prefabdict.Add ("11", (GameObject) Resources.Load ("Tile11"));
		//prefabdict.Add ("12", (GameObject) Resources.Load ("Tile12"));
		//prefabdict.Add ("13", (GameObject) Resources.Load ("Tile13"));
		//prefabdict.Add ("14", (GameObject) Resources.Load ("Tile14"));
		prefabdict.Add ("15", (GameObject) Resources.Load ("Tile15"));
		prefabdict.Add ("16", (GameObject) Resources.Load ("Tile16"));
		prefabdict.Add ("17", (GameObject) Resources.Load ("Tile17"));
		prefabdict.Add ("18", (GameObject) Resources.Load ("Tile18"));
		prefabdict.Add ("19", (GameObject) Resources.Load ("Tile19"));
		prefabdict.Add ("1b", (GameObject) Resources.Load ("Tile1b"));
		prefabdict.Add ("1c", (GameObject) Resources.Load ("Tile16"));
		prefabdict.Add ("1d", (GameObject) Resources.Load ("Tile17"));
		prefabdict.Add ("1e", (GameObject) Resources.Load ("Tile18"));
		prefabdict.Add ("1f", (GameObject) Resources.Load ("Tile19"));
		prefabdict.Add ("24", (GameObject) Resources.Load ("Tile18"));
		prefabdict.Add ("2a", (GameObject) Resources.Load("Tile2a"));
		prefabdict.Add ("2e", (GameObject) Resources.Load("Tile2e"));
		prefabdict.Add ("2f", (GameObject) Resources.Load("Tile2f"));
		prefabdict.Add ("30", (GameObject) Resources.Load("Tile30"));
		prefabdict.Add ("3c", (GameObject) Resources.Load("Tile3c"));
		prefabdict.Add ("3d", (GameObject) Resources.Load("Tile3d"));
		prefabdict.Add ("3e", (GameObject) Resources.Load("Tile3e"));
		prefabdict.Add ("42", (GameObject) Resources.Load ("Tile42"));
		prefabdict.Add ("43", (GameObject) Resources.Load ("Tile43"));
		prefabdict.Add ("44", (GameObject) Resources.Load ("Tile44"));
		prefabdict.Add ("50", (GameObject) Resources.Load ("Tile50"));
		prefabdict.Add ("51", (GameObject) Resources.Load ("Tile51"));
		prefabdict.Add ("52", (GameObject) Resources.Load ("Tile52"));
		prefabdict.Add ("56", (GameObject) Resources.Load ("Tile50"));
		prefabdict.Add ("57", (GameObject) Resources.Load ("Tile51"));
		prefabdict.Add ("58", (GameObject) Resources.Load ("Tile52"));
		prefabdict.Add ("64", (GameObject) Resources.Load ("Tile64"));
		prefabdict.Add ("65", (GameObject) Resources.Load ("Tile65"));
		prefabdict.Add ("66", (GameObject) Resources.Load ("Tile66"));
		prefabdict.Add ("6a", (GameObject) Resources.Load ("Tile64"));
		prefabdict.Add ("6b", (GameObject) Resources.Load ("Tile65"));
		prefabdict.Add ("6c", (GameObject) Resources.Load ("Tile66"));
		prefabdict.Add ("78", (GameObject) Resources.Load ("Tile78"));
		prefabdict.Add ("79", (GameObject) Resources.Load ("Tile79"));
		prefabdict.Add ("7a", (GameObject) Resources.Load ("Tile7a"));
		prefabdict.Add ("7e", (GameObject) Resources.Load ("Tile78"));
		prefabdict.Add ("7f", (GameObject) Resources.Load ("Tile79"));
		prefabdict.Add ("80", (GameObject) Resources.Load ("Tile7a"));
		prefabdict.Add ("8c", (GameObject) Resources.Load ("Tile8c"));
		prefabdict.Add ("8d", (GameObject) Resources.Load ("Tile8d"));
		prefabdict.Add ("8e", (GameObject) Resources.Load ("Tile8e"));
		prefabdict.Add ("8f", (GameObject) Resources.Load ("Tile8f"));
		prefabdict.Add ("90", (GameObject) Resources.Load ("Tile90"));
		prefabdict.Add ("91", (GameObject) Resources.Load ("Tile91"));
		prefabdict.Add ("92", (GameObject) Resources.Load ("Tile8c"));
		prefabdict.Add ("93", (GameObject) Resources.Load ("Tile8d"));
		prefabdict.Add ("94", (GameObject) Resources.Load ("Tile8e"));
		prefabdict.Add ("95", (GameObject) Resources.Load ("Tile8f"));
		prefabdict.Add ("96", (GameObject) Resources.Load ("Tile90"));
		prefabdict.Add ("97", (GameObject) Resources.Load ("Tile91"));
	}
}

[XmlRoot("World")]
public class World {
	[XmlArray("Rooms")]
	[XmlArrayItem("Room")]
	public List<Room> Rooms = new List<Room> ();

	public void loadRoom(int x, int y) {
		Rooms[x * 8 + y].Initialize();
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

		//if(block == true){
		tileprefab.AddComponent ("Rigidbody");
		tileprefab.rigidbody.useGravity = false;
		tileprefab.AddComponent ("BoxCollider");
		tileprefab.rigidbody.constraints = RigidbodyConstraints.FreezeAll;
		position.z = -1;
		tileprefab.transform.position = position;
		//}
	}
}