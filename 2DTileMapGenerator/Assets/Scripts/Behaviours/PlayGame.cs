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
