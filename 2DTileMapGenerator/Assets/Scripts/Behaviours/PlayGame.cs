using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

using System.IO;
using System;

public class PlayGame : MonoBehaviour
{
    public Tile curTile;

    char values;
    string valueFile = "";
    public const string FILE_NAME = "LatestSave.txt";

    public void CreateTextFile()
    {

        StreamWriter sr = File.CreateText(FILE_NAME);
        sr.WriteLine("This is my file. Its flipped!");

        for (int i = 0; i < World.width; i++)
        {
            for (int j = 0; j < World.height; j++)
            {

                curTile = new Tile(World.instance.GetTileAt(j, i).type);

                switch (curTile.type)
                {
                    case Tile.Type.Dirt:
                        values = 'D';
                        break;
                    case Tile.Type.Grass:
                        values = 'G';
                        break;
                    case Tile.Type.Sand:
                        values = 'S';
                        break;
                    case Tile.Type.Water:
                        values = 'W';
                        break;
                    case Tile.Type.Stone:
                        values = 'C';
                        break;
                    case Tile.Type.Void:
                        values = 'X';
                        break;
                    default:
                        break;
                }

                valueFile = valueFile + values + " ";
            }
            sr.WriteLine(valueFile);
            valueFile = "";
        }

        //sr.WriteLine("I can write ints {0} or floats {1}, and so on.", 1, 4.2);
        sr.Close();
    }

    public void changeScene(string s_Name)
    {
        SceneManager.LoadScene(s_Name);
    }

}


//for (int j = 0; j < 5; j++)
//{
//    
//}

//To check if a file already exists..
//if (File.Exists(FILE_NAME))
//{
//    Console.WriteLine("{0} already exists.", FILE_NAME);
//    return;
//}