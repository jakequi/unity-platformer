using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        StartCoroutine(NextLevel());
    }

    IEnumerator NextLevel(){
        yield return new WaitForSecondsRealtime(0.5f);
        if(SceneManager.GetActiveScene().buildIndex+1 == SceneManager.sceneCountInBuildSettings){
            SceneManager.LoadScene(0);
        }
        else{
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        }
    }
}
