using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class EnterCave : MonoBehaviour {

    public string LevelToEnter;
    public string LevelFrom;


    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                SceneManager.LoadScene(LevelToEnter);
                SceneManager.UnloadSceneAsync(LevelFrom);
            }
        }
    }
}
