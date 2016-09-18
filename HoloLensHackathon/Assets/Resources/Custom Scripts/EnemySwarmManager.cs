using UnityEngine;
using System.Collections;

public class EnemySwarmManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        transform.Rotate(Vector3.up * Random.Range(0f, 200f));
	}
	
	// Update is called once per frame
	void Update () {

        transform.Rotate(Vector3.up * (Random.Range(.2f,.4f)));

    }
}
