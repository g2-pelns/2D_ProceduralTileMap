using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;
using System;

public class LoadFile : MonoBehaviour {

    protected FileInfo theSourceFile = null;
    protected StreamReader reader = null;
    protected string text = " "; //assigned to allow first line to be read below

    public void LoadLevel()
    {
        theSourceFile = new FileInfo("LatestSave.txt");
        reader = theSourceFile.OpenText();

        do
        {
            text = reader.ReadLine();
            Debug.Log(text);
        } while (text != null);

        text = " ";
        reader.Close();
    }

}
