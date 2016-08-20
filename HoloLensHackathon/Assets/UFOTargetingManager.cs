using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Events;

public class UFOTargetingManager : MonoBehaviour {
    public List<GameObject> kitties = new List<GameObject>();
    public GameObject gameOverText;
    public UnityEvent gameHasEnded;

    void Update()
    {
        if (kitties.Count < 1)
        {
            gameHasEnded.Invoke();
        }
    }

    public void showGameOverMenu()
    {
        gameOverText.SetActive(true);
        gameOverText.transform.parent = null;
    }
}
