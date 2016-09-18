using UnityEngine;
using System.Collections;

public class KingKatkaManager : MonoBehaviour {

    public GameObject arrowPointer;
    public enemyManager exampleEnemy;
    public GameObject MenuItem1;
    public GameObject MenuItem2;
    // Use this for initialization
    void Start () {

        StartCoroutine(WaitFor(1f));
        MenuItem1.SetActive(false);
        MenuItem2.SetActive(false);
        exampleEnemy.gameObject.SetActive(false);

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator WaitFor(float waitMultiplier)
    {
        yield return new WaitForSeconds(5f * waitMultiplier);
        GetComponent<TextMesh>().text = "Long have we waited, attempting to communicate with you \n across time and space from our home dimension.";
        yield return new WaitForSeconds(10f * waitMultiplier);
        GetComponent<TextMesh>().text = "Only now with your new human technology have we finally \n been able to make contact.";
        yield return new WaitForSeconds(9f * waitMultiplier);
        GetComponent<TextMesh>().text = "Human, You are our last hope.";
        yield return new WaitForSeconds(5f * waitMultiplier);
        GetComponent<TextMesh>().text = "Since the beginning of time, glass has long been the mortal \n enemy of cats, so delicate, so breakable.";
        yield return new WaitForSeconds(10f * waitMultiplier);
        exampleEnemy.gameObject.SetActive(true);
        GetComponent<TextMesh>().text = "We are now under attack by the overlords of Glassuvias, \n evil invaders determined to abduct my people.";
        yield return new WaitForSeconds(10f * waitMultiplier);
        GetComponent<TextMesh>().text = "You must defend us.";
        //yield return new WaitForSeconds(5f * waitMultiplier);
        GetComponent<TextMesh>().text = "Find and tap to equip my loyal kitty subjects for battle. \n With them loaded in our hyper-cat cannon, tap \n again to launch them at enemy invaders! \n Good Luck and Godspeed...";
        arrowPointer.SetActive(true);
        yield return new WaitForSeconds(5f * waitMultiplier);
        MenuItem1.SetActive(true);
        MenuItem2.SetActive(true);
    }
}
