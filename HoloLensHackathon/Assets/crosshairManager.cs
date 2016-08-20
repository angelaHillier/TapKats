using UnityEngine;
using System.Collections;

public class crosshairManager : MonoBehaviour {

    private Quaternion lookAtRotation;
	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {

        lookAtRotation = Quaternion.LookRotation(Camera.main.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookAtRotation, Time.deltaTime);

    }
}
