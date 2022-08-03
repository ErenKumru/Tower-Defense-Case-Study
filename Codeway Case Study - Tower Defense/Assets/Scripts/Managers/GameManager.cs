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

            /*
             * To save the game:
             *      Create GameData class
             *          In GameData class store stage, coins, kill count and list of turrets that are already builded
             *          
             *      Create SaveSystem class
             *          In SaveSystem implement Load and Save methods using BinaryFormatter to the fiel path Application.persistentDataPath + "/*filename*"
             */
    //}
}
