using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class enemySpawner : MonoBehaviour {
    public GameObject[] ufoTypes = new GameObject[6];
    public List<GameObject> spawnedUfos = new List<GameObject>();
    public int maxNumberOfShipsAliveAtOnce = 3;
    public float shipSpawnRate = 5.0f;
    public float UFOSpinSpeed = 2.0f;
    public bool isStartLevel;
   
    
    void Start () {
        InvokeRepeating("spawnAShipIfUnderBudget", 0.0f, shipSpawnRate);
	}

    void spawnAShipIfUnderBudget()
    {
        if (spawnedUfos.Count < maxNumberOfShipsAliveAtOnce)
        {
            //Debug.Log("spawned a UFO");
            spawnedUfos.Add(GameObject.Instantiate(ufoTypes[Random.Range(0, 6)], new Vector3(Random.Range(-10f, 10f), -5.5f, Random.Range(-10f, 10f)), new Quaternion(0f, 0f, 0f, 0f)) as GameObject);

            for (int i = 0; i < spawnedUfos.Count; i++)
            {
                spawnedUfos[i].GetComponent<enemyManager>().isStartLevel = isStartLevel;
            }
        }
    }

	void Update () {
        if (spawnedUfos.Count > 0)
        {
            for (int i = 0; i < spawnedUfos.Count; i++)
            {
                //spawnedUfos[i].transform.Rotate(new Vector3(0.0f, UFOSpinSpeed, 0.0f));
            }
        }
    }
}
