using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class tester : MonoBehaviour {

    TextMesh testText;

    int debugType = 0;
    bool pressed = false;

	// Use this for initialization
	void Start () {
        testText = GetComponent<TextMesh>();
	}
	
	// Update is called once per frame
	void Update () {

        switch (debugType)
        {
            case 0:

                break;
            case 1:

                break;
            case 2:

                break;
            case 3:

                break;
            case 4:

                break;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) && !pressed)
        {
            pressed = true;
            debugType--;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && !pressed)
        {
            pressed = true;
            debugType++;
        }
        else
        {
            pressed = false;
        }

        if (debugType > 4)
            debugType = 0;
        else if(debugType < 0)
            debugType = 4;
        

        testText.text = ("DebugType : " + debugType);

    }
}
