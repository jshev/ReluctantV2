using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneSwap : MonoBehaviour
{
    public string sceneName;
    LevelChanger lvlC;

    void Start() {
        lvlC = FindObjectOfType<LevelChanger>();
        if (lvlC == null) {
            print("No LevelChanger in Scene");
        }
    }

    public void SceneLoad ()
    {
        if (lvlC != null) {
            lvlC.FadeToLevel(sceneName);
        } else {
            SceneManager.LoadScene(sceneName);
        }
    }
}
