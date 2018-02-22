using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Tile {

    public enum S_Type { Dirt, Grass, Sand, Water, Stone, Void }
    public S_Type type;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public S_Tile(S_Type type)
    {
        this.type = type;
    }
}
