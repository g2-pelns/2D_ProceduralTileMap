using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuControl : MonoBehaviour {

    public Text m_Seed;
    public Text m_Width, m_Height;
    public Text m_Freq, m_Amp, m_Lac, m_Per;
    public Text m_Sea, m_Beach, m_Grass, m_Dirt, m_Stone;

    // Use this for initialization
    void Start () {
        m_Seed.text = "0";

        m_Width.text = "0";
        m_Height.text = "0";

        m_Freq.text = "0";
        m_Amp.text = "0";
        m_Lac.text = "0";
        m_Per.text = "0";

        m_Sea.text = "0";
        m_Beach.text = "0";
        m_Grass.text = "0";
        m_Dirt.text = "0";
        m_Stone.text = "0";
    }
	
	// Update is called once per frame
	void Update () {
        m_Seed.text = World.seed;

        m_Width.text = World.width.ToString();
        m_Height.text = World.height.ToString();

        m_Freq.text = World.frequency.ToString();
        m_Amp.text = World.amplitude.ToString();
        m_Lac.text = World.lacunarity.ToString();
        m_Per.text = World.persistance.ToString();

        m_Sea.text = World.seaLevel.ToString();
        m_Beach.text = World.beachEndHeight.ToString();
        m_Grass.text = World.grassEndHeight.ToString();
        m_Dirt.text = World.dirtEndHeight.ToString();
        m_Stone.text = World.stoneEndHeight.ToString();
    }

	public static void AddValue_Width()
	{
		World.width += 10;
	}

	public static void AddValue_Height()
	{
		World.height += 10;
	}

	public static void AddValue_Freq()
	{
		World.frequency += 0.1f;
	}

	public static void AddValue_Amp()
	{
		World.amplitude += 0.01f;
	}

	public static void AddValue_Lac()
	{
		World.lacunarity += 0.01f;
	}

	public static void AddValue_Per()
	{
		World.persistance += 0.01f;
	}

	public static void AddValue_Sea()
	{
		World.seaLevel += 0.05f;
	}

	public static void AddValue_Beach()
	{
		World.beachEndHeight += 0.05f;
	}

	public static void AddValue_Grass()
	{
		World.grassEndHeight += 0.05f;
	}

	public static void AddValue_Dirt()
	{
		World.dirtEndHeight += 0.05f;
	}

	public static void AddValue_Stone()
	{
		World.stoneEndHeight += 0.05f;
	}

	public static void MinusValue_Width()
	{
		World.width -= 10;
	}

	public static void MinusValue_Height()
	{
		World.height -= 10;
	}

	public static void MinusValue_Freq()
	{
		World.frequency -= 0.1f;
	}

	public static void MinusValue_Amp()
	{
		World.amplitude -= 0.01f;
	}

	public static void MinusValue_Lac()
	{
		World.lacunarity -= 0.01f;
	}

	public static void MinusValue_Per()
	{
		World.persistance -= 0.01f;
	}

	public static void MinusValue_Sea()
	{
		World.seaLevel -= 0.05f;
	}

	public static void MinusValue_Beach()
	{
		World.beachEndHeight -= 0.05f;
	}

	public static void MinusValue_Grass()
	{
		World.grassEndHeight -= 0.05f;
	}

	public static void MinusValue_Dirt()
	{
		World.dirtEndHeight -= 0.05f;
	}

	public static void MinusValue_Stone()
	{
		World.stoneEndHeight -= 0.05f;
	}
}
