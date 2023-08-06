using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour {
    public void startGame() {
        StartCoroutine(DelayedCoroutine.delayedCoroutine(0.5f, () => SceneManager.LoadScene("PlayScene")));
    }

    public void endGame() {
        Application.Quit();
    }
}
