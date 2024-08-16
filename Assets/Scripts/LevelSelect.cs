using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelSelect : MonoBehaviour
{

    public void ChangeScene(string Scene)
    {
        Debug.Log("Starting Game");
        SceneManager.LoadScene(Scene);
    }
}
