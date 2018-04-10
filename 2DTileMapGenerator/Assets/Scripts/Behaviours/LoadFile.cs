using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;
using System;

public class LoadFile : MonoBehaviour {

    private float tileVal;
    public Tile curTile;
    public static LoadFile lf_instance;

    public int width;
    public int height;

    protected FileInfo theSourceFile = null;
    protected StreamReader reader = null;
    string text = ""; //assigned to allow first line to be read below

    public void LoadLevel()
    {
        theSourceFile = new FileInfo("LatestSave.txt");
        reader = theSourceFile.OpenText();

        World.instance.tiles = new Tile[width, height];
        //do
        //{
        //    text = reader.ReadLine();
        //    Debug.Log(text);
        //} while (text != null);

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                text = reader.ReadLine();

                if (text == "D")
                {
                    tileVal = World.dirtEndHeight;
                }
                else if (text == "G")
                {
                    tileVal = World.grassEndHeight;
                }
                else if (text == "S")
                {
                    tileVal = World.beachEndHeight;
                }
                else if (text == "W")
                {
                    tileVal = World.seaLevel;
                }
                else if (text == "C")
                {
                    tileVal = World.stoneEndHeight;
                }
                else
                {
                    tileVal = 1.5f;
                }

                //tiles[i, j] = MakeTileAtHeight(noiseValues[i, j]);
                World.instance.tiles[i, j] = World.instance.MakeTileAtHeight(tileVal);
                World.instance.tiles[i, j].m_x = i;
                World.instance.tiles[i, j].m_y = j;
            }
        }

        World.instance.LoadMap();

        text = " ";
        reader.Close();
    }

}