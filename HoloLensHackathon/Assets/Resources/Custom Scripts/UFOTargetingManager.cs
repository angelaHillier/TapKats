using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Events;

public class UFOTargetingManager : MonoBehaviour {
    public int score = 0;

    public List<GameObject> kitties = new List<GameObject>();
    public GameObject gameOverText;
    public UnityEvent gameHasEnded;

    


    void Update()
    {
        if (kitties.Count < 1)
        {
            gameHasEnded.Invoke();
        }


        //Make below more optimized if possible
        for(int i = 0; i < kitties.Count; i++)
        {
            if(kitties[i] == null)
            {
                kitties.Remove(kitties[i]);
            }
        }
    }

    public void showGameOverMenu()
    {
        gameOverText.GetComponentInChildren<Score>().score = score;
        gameOverText.SetActive(true);
        gameOverText.transform.parent = null;
    }
}
