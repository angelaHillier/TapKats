using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour {

    public Texture startSelectTexture;
    public GameObject Menu;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnSelect()
    {
        GetComponent<Renderer>().material.mainTexture = startSelectTexture;
        Menu.SetActive(false);
        GetComponent<AudioSource>().Play();
        SceneManager.LoadScene("MainLevel");
        //Application.LoadLevel(1);
    }
}
