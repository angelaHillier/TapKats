using UnityEngine;
using System.Collections;

public class rotator : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown("a"))
        {
            GetComponent<Transform>().Rotate(0f, 90f, 0f);
        }
        if (Input.GetKeyDown("s"))
        {
            GetComponent<Transform>().Rotate(90f, 0f, 0f);
        }
        if (Input.GetKeyDown("d"))
        {
            GetComponent<Transform>().Rotate(0f, 0f, 90f);
        }
    }
}
