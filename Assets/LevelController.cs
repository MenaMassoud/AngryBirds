using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    private static int _nextLevelIndex = 1;
    //underscore means it is part of my class not passed by a function..
    private Monsters[] _monsters;

    //when ever the gameobject is enabled this function is called
    private void OnEnable()
    {
        //when all the enimies are the monsters are died display go to next level level
        _monsters = FindObjectsOfType<Monsters>();
        
    }
    // Update is called once per frame
    void Update()
    {
        foreach (Monsters monster in _monsters) {
            if (monster != null) {//monster not destroied
                return;
            }
            Debug.Log("You killed all enemies");

            _nextLevelIndex++;
            string nextLevelName = "Level" + _nextLevelIndex;
            SceneManager.LoadScene(nextLevelName);
        }
        
    }
}
