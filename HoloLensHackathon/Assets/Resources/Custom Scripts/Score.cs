using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    //[HideInInspector]
    public int score = 0;

    TextMesh scoreText;

	// Use this for initialization
	void Start () {
        scoreText = GetComponent<TextMesh>();
	}
	
	// Update is called once per frame
	void Update () {
        scoreText.text = "UFO'S DESTROYED : " + score;

    }
}
