using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour {

    public static World instance;

    public Material material;
    public int width;
    public int height;

    public string seed;
    public bool randomSeed;

    public float frequency;
    public float amplitude;

    public float lacunarity;
    public float persistance;

    public int octaves;

    public float seaLevel;

    private float beachStartHeight; // SEA
    public float beachEndHeight;

    private float grassStartHeight; // BEACH END
    public float grassEndHeight;

    private float dirtStartHeight; // GRASS END
    public float dirtEndHeight;

    private float stoneStartHeight; //DIRT END
    public float stoneEndHeight;

    MeshData data;
    Noise noise;
    Mesh mesh;

    private int time = 0;

    public Tile[,] tiles;
	// Use this for initialization

    void Awake()
    {
        instance = this;
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
        CreateTiles();
        SubdivideTilesArray();
	}
	
	// Update is called once per frame
	void Update () {

        if (randomSeed == true)
        {
            if (time == 100)
            {
                GameObject[] oldTiles = GameObject.FindGameObjectsWithTag("CHUNK");
                foreach (GameObject tiles in oldTiles)
                {
                    Destroy(tiles);
                }

                int value = Random.Range(-1000, 1000);
                seed = value.ToString();

                noise = new Noise(seed.GetHashCode(), frequency, amplitude, lacunarity, persistance, octaves);

                CreateTiles();
                SubdivideTilesArray();

                time = 0;
            }
            time += 1;
        }
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
}
