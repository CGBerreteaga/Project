using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuOptions : MonoBehaviour
{
    [SerializeField] GameObject clickSoundPrefab;
    // Start is called before the first frame update
    

    public void StartGame() {
        Instantiate(clickSoundPrefab,transform.position,transform.rotation);
        SceneManager.LoadScene("Level1");
    }

    public void Controls() {
        Instantiate(clickSoundPrefab,transform.position,transform.rotation);
    }

    public void QuitGame() {
        Instantiate(clickSoundPrefab,transform.position,transform.rotation);
         #if UNITY_EDITOR
        // If so, stop playing the game in the editor
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        // Otherwise, quit the application
        Application.Quit();
        #endif
    }
}
