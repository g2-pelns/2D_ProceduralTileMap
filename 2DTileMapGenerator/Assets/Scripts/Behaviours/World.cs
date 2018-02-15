using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class World : MonoBehaviour {

    public static World instance;

    public Material material;
    public Button rndButton;
    public Button genButton;
    public List<Button> addVal;
    public List<Button> minVal;

    public static int width;
    public static int height;

    public static string seed;
    public bool randomSeed;

    public static float frequency;
    public static float amplitude;

    public static float lacunarity;
    public static float persistance;

    public int octaves;

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

        width = 50;
        height = 50;

        frequency = 0f;
        amplitude = 0f;
        lacunarity = 0f;
        persistance = 0f;

        seaLevel = 0.2f;
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

        for (int i = 0; i < addVal.Count; i++)
        {
            switch (i)
            {
                case 0:
                    addVal[i].onClick.AddListener(AddValue_Width);
                    break;
                case 1:
                    addVal[i].onClick.AddListener(AddValue_Height);
                    break;
                case 2:
                    addVal[i].onClick.AddListener(AddValue_Freq);
                    break;
                case 3:
                    addVal[i].onClick.AddListener(AddValue_Amp);
                    break;
                case 4:
                    addVal[i].onClick.AddListener(AddValue_Lac);
                    break;
                case 5:
                    addVal[i].onClick.AddListener(AddValue_Per);
                    break;
                case 6:
                    addVal[i].onClick.AddListener(AddValue_Sea);
                    break;
                case 7:
                    addVal[i].onClick.AddListener(AddValue_Beach);
                    break;
                case 8:
                    addVal[i].onClick.AddListener(AddValue_Grass);
                    break;
                case 9:
                    addVal[i].onClick.AddListener(AddValue_Dirt);
                    break;
                case 10:
                    addVal[i].onClick.AddListener(AddValue_Stone);
                    break;
            }

        }
        for (int i = 0; i < minVal.Count; i++)
        {
            switch (i)
            {
                case 0:
                    minVal[i].onClick.AddListener(MinusValue_Width);
                    break;
                case 1:
                    minVal[i].onClick.AddListener(MinusValue_Height);
                    break;
                case 2:
                    minVal[i].onClick.AddListener(MinusValue_Freq);
                    break;
                case 3:
                    minVal[i].onClick.AddListener(MinusValue_Amp);
                    break;
                case 4:
                    minVal[i].onClick.AddListener(MinusValue_Lac);
                    break;
                case 5:
                    minVal[i].onClick.AddListener(MinusValue_Per);
                    break;
                case 6:
                    minVal[i].onClick.AddListener(MinusValue_Sea);
                    break;
                case 7:
                    minVal[i].onClick.AddListener(MinusValue_Beach);
                    break;
                case 8:
                    minVal[i].onClick.AddListener(MinusValue_Grass);
                    break;
                case 9:
                    minVal[i].onClick.AddListener(MinusValue_Dirt);
                    break;
                case 10:
                    minVal[i].onClick.AddListener(MinusValue_Stone);
                    break;
            }

        }

        CreateTiles();  
        SubdivideTilesArray();
	}
	
	// Update is called once per frame
	public void Update ()
    {
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

    void AddValue_Width()
    {
        width += 1;
    }

    void AddValue_Height()
    {
        height += 1;
    }

    void AddValue_Freq()
    {
        frequency += 0.1f;
    }

    void AddValue_Amp()
    {
        amplitude += 0.01f;
    }

    void AddValue_Lac()
    {
        lacunarity += 0.01f;
    }

    void AddValue_Per()
    {
        persistance += 0.01f;
    }

    void AddValue_Sea()
    {
        seaLevel += 0.05f;
    }

    void AddValue_Beach()
    {
        beachEndHeight += 0.05f;
    }

    void AddValue_Grass()
    {
        grassEndHeight += 0.05f;
    }

    void AddValue_Dirt()
    {
        dirtEndHeight += 0.05f;
    }

    void AddValue_Stone()
    {
        stoneEndHeight += 0.05f;
    }

    void MinusValue_Width()
    {
        width -= 1;
    }

    void MinusValue_Height()
    {
        height -= 1;
    }

    void MinusValue_Freq()
    {
        frequency -= 0.1f;
    }

    void MinusValue_Amp()
    {
        amplitude -= 0.01f;
    }

    void MinusValue_Lac()
    {
        lacunarity -= 0.01f;
    }

    void MinusValue_Per()
    {
        persistance -= 0.01f;
    }

    void MinusValue_Sea()
    {
        seaLevel -= 0.05f;
    }

    void MinusValue_Beach()
    {
        beachEndHeight -= 0.05f;
    }

    void MinusValue_Grass()
    {
        grassEndHeight -= 0.05f;
    }

    void MinusValue_Dirt()
    {
        dirtEndHeight -= 0.05f;
    }

    void MinusValue_Stone()
    {
        stoneEndHeight -= 0.05f;
    }
}
