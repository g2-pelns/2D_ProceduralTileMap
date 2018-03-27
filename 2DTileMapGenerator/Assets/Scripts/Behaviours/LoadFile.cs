using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;
using System;

public class LoadFile : MonoBehaviour {

    private float tileVal;
    public Tile curTile;

    protected FileInfo theSourceFile = null;
    protected StreamReader reader = null;
    protected string text = " "; //assigned to allow first line to be read below

    public void LoadLevel()
    {
        theSourceFile = new FileInfo("LatestSave.txt");
        reader = theSourceFile.OpenText();

        //do
        //{
        //    text = reader.ReadLine();
        //    Debug.Log(text);
        //} while (text != null);

        for (int i = 0; i < World.width; i++)
        {
            for (int j = 0; j < World.height; j++)
            {
                text = reader.ReadLine();
                switch (text)
                {
                    case "D":
                        tileVal = World.dirtEndHeight;
                        break;
                    case "G":
                        tileVal = World.grassEndHeight;
                        break;
                    case "S":
                        tileVal = World.beachEndHeight;
                        break;
                    case "W":
                        tileVal = World.seaLevel;
                        break;
                    case "C":
                        tileVal = World.stoneEndHeight;
                        break;
                    case "X":
                        tileVal = 1.5f;
                        break;
                    default:
                        break;
                }


                World.instance.tiles[i, j] = World.instance.MakeTileAtHeight(tileVal);
            }
        }

        World.instance.SubdivideTilesArray();

        text = " ";
        reader.Close();
    }

}
