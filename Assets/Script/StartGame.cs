using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.PlayBackgroundSound();
    }

    public void PlayGame()
    {
        AudioManager.instance.StopBackgroundSound();
        SceneManager.LoadScene(0);
    }
}
