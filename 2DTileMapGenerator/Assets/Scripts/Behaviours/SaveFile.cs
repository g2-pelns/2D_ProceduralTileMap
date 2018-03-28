using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;
using System;

public class SaveFile : MonoBehaviour {

    public Tile curTile;

    string values = "";
    string valueFile = "";
    public const string FILE_NAME = "LatestSave.txt";

    public void SaveLevel()
    {

        StreamWriter sr = File.CreateText(FILE_NAME);

        for (int i = 0; i < World.width; i++)
        {

            for (int j = 0; j < World.height; j++)
            {

                curTile = new Tile(World.instance.GetTileAt(i, j).type);

                switch (curTile.type)
                {
                    case Tile.Type.Dirt:
                        values = "D";
                        break;
                    case Tile.Type.Grass:
                        values = "G";
                        break;
                    case Tile.Type.Sand:
                        values = "S";
                        break;
                    case Tile.Type.Water:
                        values = "W";
                        break;
                    case Tile.Type.Stone:
                        values = "C";
                        break;
                    case Tile.Type.Void:
                        values = "X";
                        break;
                    default:
                        break;
                }

                //valueFile = valueFile + values + " ";
                valueFile = values;
                sr.WriteLine(valueFile);
            }
            //sr.WriteLine(valueFile);
            valueFile = "";
        }

        //sr.WriteLine("I can write ints {0} or floats {1}, and so on.", 1, 4.2);
        sr.Close();
    }
}
