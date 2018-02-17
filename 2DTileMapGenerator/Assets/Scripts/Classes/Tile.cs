using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile {

    public enum Type { Dirt, Grass, Sand, Water, Stone, Void}
    public Type type;

	public float m_x;
	public float m_y;

	public float M_x {
		get {
			return m_x;
		}
		set {
			m_x = value;
		}
	}

	public float M_y {
		get {
			return m_y;
		}
		set {
			m_y = value;
		}
	}

    public Tile (Type type)
    {
        this.type = type;
    }
}
