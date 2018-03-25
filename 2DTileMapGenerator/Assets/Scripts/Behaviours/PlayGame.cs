using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.IO;
using System;
public class PlayGame : MonoBehaviour
{
    int values = 10;
    string valueFile = "";
    public const string FILE_NAME = "LatestSave.txt";

    public void CreateTextFile()
    {
        for (int j = 0; j < 5; j++)
        {
            valueFile = valueFile + values.ToString() + " ";
        }

        //if (File.Exists(FILE_NAME))
        //{
        //    Console.WriteLine("{0} already exists.", FILE_NAME);
        //    return;
        //}

        StreamWriter sr = File.CreateText(FILE_NAME);
        sr.WriteLine("This is my file.");
        sr.WriteLine(valueFile);
        //sr.WriteLine("I can write ints {0} or floats {1}, and so on.", 1, 4.2);
        sr.Close();
    }

    public void changeScene(string s_Name)
    {
        SceneManager.LoadScene(s_Name);
    }

}
