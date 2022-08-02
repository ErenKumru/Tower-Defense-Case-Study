using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<LevelManager>
{
    
    public void StartNewGame()
    {
        SceneManager.LoadScene(1);
    }

    public void EndGame()
    {
        Time.timeScale = 0f;
        //save game
        StartCoroutine(HandleGameEnding());
    }

    private IEnumerator HandleGameEnding()
    {
        yield return new WaitForSecondsRealtime(2f);
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    //private void LoadGame()
    //{
    //    
    //}

    //private void SaveGame()
    //{

    //}
}
