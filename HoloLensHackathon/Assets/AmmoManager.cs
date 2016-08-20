using UnityEngine;
using System.Collections;

public class AmmoManager : MonoBehaviour {

    public int shotsRemaining = 3;
    public GameObject EmptyText;
    public GameObject ShotsRemainingText;
	// Use this for initialization
	void Start () {

        updateShotsRemaining();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void updateShotsRemaining()
    {
        string ShotsRemainingString = shotsRemaining + " shots remaining!";
        ShotsRemainingText.GetComponent<TextMesh>().text = ShotsRemainingString;

    }

    public void ToggleEmptyAlert(bool toggle)
    {

        if (toggle)
        {
            EmptyText.SetActive(true);
        }
        else
        {
            EmptyText.SetActive(false);
        }

    }
}
