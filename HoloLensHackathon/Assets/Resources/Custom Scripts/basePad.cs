using UnityEngine;
using System.Collections;

public class basePad : MonoBehaviour {

    public GameObject CatPre;
    public float timer;
    public AudioClip[] smashSounds = new AudioClip[4];



    GameObject myCat;
    bool isActive = true;
    int timeStep = 5;

    //Color startCol;
    //public Color delayColor, brokenColor;


    // Use this for initialization
    void Start () {
        myCat = Instantiate(CatPre, transform.position, Quaternion.identity) as GameObject;
        myCat.GetComponent<Cat>().myBase = gameObject;

        

    }
	
	// Update is called once per frame
	void Update () {
	    
	}


    public void catAbducted()
    {
        isActive = false;
        AudioSource.PlayClipAtPoint(smashSounds[Random.Range(0, smashSounds.Length)], transform.position);

        gameObject.GetComponent<MeshExploder>().Explode();
        Destroy(gameObject);
    }

    public void catRescued()
    {
        StartCoroutine("respawnTime");
    }

    IEnumerator respawnTime()
    {
        if (isActive)
        {
            for (int i = 0; i < timeStep; i++)
            {
                //Debug.Log("changing colors" + i);
                yield return new WaitForSeconds(timer / timeStep);




                //Doesn't work
               // GetComponent<MeshRenderer>().material.color = Color.Lerp(delayColor, startCol, i / (timeStep - 1));
            }

            myCat = Instantiate(CatPre, transform.position, Quaternion.identity) as GameObject;
            myCat.GetComponent<Cat>().myBase = gameObject;

        }
    }
}
