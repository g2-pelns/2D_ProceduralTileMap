using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayGame : MonoBehaviour
{
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