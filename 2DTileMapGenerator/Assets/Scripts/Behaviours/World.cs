using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class World : MonoBehaviour {

    public static World instance;
	public GameObject cursor;

    public Material material;
    public Button rndButton;
    public Button genButton;

    public List<Button> addVal;
    public List<Button> minVal;
    public List<Button> tileSelects;

    public static int width;
    public static int height;

    public static string seed;
    public bool randomSeed;

    public static float frequency;
    public static float amplitude;

    public static float lacunarity;
    public static float persistance;

    public int octaves;
    public static float selectedTile;

    public static float seaLevel;

    private float beachStartHeight; // SEA
    public static float beachEndHeight;

    private float grassStartHeight; // BEACH END
    public static float grassEndHeight;

    private float dirtStartHeight; // GRASS END
    public static float dirtEndHeight;

    private float stoneStartHeight; //DIRT END
    public static float stoneEndHeight;

    MeshData data;
    Noise noise;
    Mesh mesh;
    Button btn;

    private int time = 0;
    bool randomize;
    bool regen;
    //bool random2 = true;

    public Tile[,] tiles;
	// Use this for initialization

    void Awake()
    {
        randomize = false;
        regen = false;
        instance = this;

        width = 10;
        height = 10;

        frequency = 0f;
        amplitude = 0f;
        lacunarity = 0f;
        persistance = 0f;

        seaLevel = 0.2f;
        selectedTile = 0.25f;

        beachEndHeight = 0.4f;
        grassEndHeight = 0.6f;
        dirtEndHeight = 0.8f;
        stoneEndHeight = 1.0f;

        beachStartHeight = seaLevel;
		grassStartHeight = beachEndHeight;
		dirtStartHeight = grassEndHeight;
		stoneStartHeight = dirtEndHeight;

        if (randomSeed == true)
        {
            int value = Random.Range(-1000, 1000);
            seed = value.ToString();
        }

        noise = new Noise(seed.GetHashCode(), frequency, amplitude, lacunarity, persistance, octaves);
    }

	void Start () {
        btn = rndButton.GetComponent<Button>();
        btn.onClick.AddListener(RandomizeTiles);

        genButton.onClick.AddListener(ReGenerateTiles);

		for (int i = 0; i < addVal.Count; i++){
        
            switch (i)
            {
                case 0:
                addVal[i].onClick.AddListener(MenuControl.AddValue_Width);
                    break;
                case 1:
				addVal[i].onClick.AddListener(MenuControl.AddValue_Height);
                    break;
                case 2:
				addVal[i].onClick.AddListener(MenuControl.AddValue_Freq);
                    break;
                case 3:
				addVal[i].onClick.AddListener(MenuControl.AddValue_Amp);
                    break;
                case 4:
				addVal[i].onClick.AddListener(MenuControl.AddValue_Lac);
                    break;
                case 5:
				addVal[i].onClick.AddListener(MenuControl.AddValue_Per);
                    break;
                case 6:
				addVal[i].onClick.AddListener(MenuControl.AddValue_Sea);
                    break;
                case 7:
				addVal[i].onClick.AddListener(MenuControl.AddValue_Beach);
                    break;
                case 8:
				addVal[i].onClick.AddListener(MenuControl.AddValue_Grass);
                    break;
                case 9:
				addVal[i].onClick.AddListener(MenuControl.AddValue_Dirt);
                    break;
                case 10:
				addVal[i].onClick.AddListener(MenuControl.AddValue_Stone);
                    break;
            }

        }

        for (int i = 0; i < minVal.Count; i++)
        {
            switch (i)
            {
                case 0:
				minVal[i].onClick.AddListener(MenuControl.MinusValue_Width);
                    break;
                case 1:
				minVal[i].onClick.AddListener(MenuControl.MinusValue_Height);
                    break;
                case 2:
				minVal[i].onClick.AddListener(MenuControl.MinusValue_Freq);
                    break;
                case 3:
				minVal[i].onClick.AddListener(MenuControl.MinusValue_Amp);
                    break;
                case 4:
				minVal[i].onClick.AddListener(MenuControl.MinusValue_Lac);
                    break;
                case 5:
				minVal[i].onClick.AddListener(MenuControl.MinusValue_Per);
                    break;
                case 6:
				minVal[i].onClick.AddListener(MenuControl.MinusValue_Sea);
                    break;
                case 7:
				minVal[i].onClick.AddListener(MenuControl.MinusValue_Beach);
                    break;
                case 8:
				minVal[i].onClick.AddListener(MenuControl.MinusValue_Grass);
                    break;
                case 9:
				minVal[i].onClick.AddListener(MenuControl.MinusValue_Dirt);
                    break;
                case 10:
				minVal[i].onClick.AddListener(MenuControl.MinusValue_Stone);
                    break;
            }

        }

        for (int i = 0; i < tileSelects.Count; i++)
        {
            switch (i)
            {
                case 0:
                    tileSelects[i].onClick.AddListener(MenuControl.DirtSelect);
                    break;
                case 1:
                    tileSelects[i].onClick.AddListener(MenuControl.GrassSelect);
                    break;
                case 2:
                    tileSelects[i].onClick.AddListener(MenuControl.SandSelect);
                    break;
                case 3:
                    tileSelects[i].onClick.AddListener(MenuControl.BrickSelect);
                    break;
                case 4:
                    tileSelects[i].onClick.AddListener(MenuControl.StoneSelect);
                    break;
                case 5:
                    tileSelects[i].onClick.AddListener(MenuControl.WaterSelect);
                    break;
                case 6:
                    tileSelects[i].onClick.AddListener(MenuControl.NULLSelect);
                    break;
            }

        }

        CreateTiles();  
        SubdivideTilesArray();
	}
	
	// Update is called once per frame
	public void Update ()
    {
		Vector3 pos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		Tile tileUnderMouse = GetTileAtCoord (pos);

		if (tileUnderMouse != null) {
			cursor.SetActive (true);
			Vector3 cursorPos = new Vector3 (tileUnderMouse.m_x, tileUnderMouse.m_y, 0);
			cursor.transform.position = cursorPos;
		
			if (Input.GetMouseButtonDown (0))
			{
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        if (tiles[i, j].m_x == tileUnderMouse.m_x && tiles[i, j].m_y == tileUnderMouse.m_y)
                        {
                            //regen = true;
                            tiles[i, j] = MakeTileAtHeight(selectedTile);
                            tiles[i, j].m_x = tileUnderMouse.m_x;
                            tiles[i, j].m_y = tileUnderMouse.m_y;

                            GameObject[] oldTiles = GameObject.FindGameObjectsWithTag("CHUNK");
                            foreach (GameObject tiles in oldTiles)
                            {
                                Destroy(tiles);
                            }

                            SubdivideTilesArray();
                        }
                    }
                }
			}

		} else {
			cursor.SetActive (false);
		}

        if (randomize == true)
        {
            CreateRandom();
            randomize = false;
        }
        else if (regen == true)
        {
            ReGenerate();
            regen = false;
        }
    }

	Tile GetTileAtCoord(Vector3 coord)
	{
		int x = Mathf.FloorToInt (coord.x);
		int y = Mathf.FloorToInt (coord.y);

		return GetTileAt(x, y);
	}

    void ReGenerate()
    {
        GameObject[] oldTiles = GameObject.FindGameObjectsWithTag("CHUNK");
        foreach (GameObject tiles in oldTiles)
        {
            Destroy(tiles);
        }

        noise = new Noise(seed.GetHashCode(), frequency, amplitude, lacunarity, persistance, octaves);

        CreateTiles();
        SubdivideTilesArray();
    }

    void CreateRandom()
    {
        int value = Random.Range(-1000, 1000);
        seed = value.ToString();
    }

    void SubdivideTilesArray (int i1 = 0, int i2 = 0)
    {
        //Get size of segment
        int sizeX;
        int sizeY;

        if(tiles.GetLength(0) - i1 > 100){
            sizeX = 100;
        } else
            sizeX = tiles.GetLength(0) - i1;

        if (tiles.GetLength(1) - i2 > 100)
        {
            sizeY = 100;
        } else
            sizeY = tiles.GetLength(1) - i2;

        GenerateMesh(i1, i2, sizeX, sizeY);

        if (tiles.GetLength(0) >= i1 + 100){
            SubdivideTilesArray(i1 + 100, i2);
            return;
        }

        if (tiles.GetLength(1) >= i2 + 100){
            SubdivideTilesArray(0, i2 + 100);
            return;
        }
    }

    void GenerateMesh (int x, int y, int width, int height) {
        data = new MeshData(x, y, width, height);

        GameObject meshGO = new GameObject("CHUNK");
        meshGO.transform.SetParent(this.transform);
        meshGO.tag = "CHUNK";

        MeshFilter filter = meshGO.AddComponent<MeshFilter>();
        MeshRenderer render = meshGO.AddComponent<MeshRenderer>();
        render.material = material;

        mesh = filter.mesh;

        mesh.vertices = data.vertices.ToArray();
        mesh.triangles = data.triangles.ToArray();
        mesh.uv = data.UVs.ToArray();
    }

    void CreateTiles ()
    {
        tiles = new Tile[width, height];

        float[,] noiseValues = noise.GetNoiseValues(width, height);

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                tiles[i, j] = MakeTileAtHeight(noiseValues[i, j]);
				tiles [i, j].m_x = i;
				tiles [i, j].m_y = j;
            }
        }
    }

	Tile MakeTileAtHeight(float currentHeight)
    {
        if (currentHeight <= seaLevel)
            return new Tile(Tile.Type.Water);

        if (currentHeight >= beachStartHeight && currentHeight <= beachEndHeight)
            return new Tile(Tile.Type.Sand);

        if (currentHeight >= grassStartHeight && currentHeight <= grassEndHeight)
            return new Tile(Tile.Type.Grass);

        if (currentHeight >= dirtStartHeight && currentHeight <= dirtEndHeight)
            return new Tile(Tile.Type.Dirt);

        if (currentHeight >= stoneStartHeight && currentHeight <= stoneEndHeight)
            return new Tile(Tile.Type.Stone);

        return new Tile(Tile.Type.Void);
    }

    public Tile GetTileAt (int x, int y)
    {
        if (x< 0 || x >= width || y < 0 || y >= height)
        {
            return null;
        }
        return tiles[x, y];
    }

    void RandomizeTiles()
    {
        randomize = true;
    }

    void ReGenerateTiles()
    {
        regen = true;
    }
}
