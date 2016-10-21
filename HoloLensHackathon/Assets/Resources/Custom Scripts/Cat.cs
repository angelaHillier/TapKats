using UnityEngine;
using System.Collections;

// "Cat" is the class for a Villager-type Cat. "BulletCat" is separate.
public class Cat : MonoBehaviour {
    public bool ufoHasTargetedMe = false;
    public AudioClip[] meows = new AudioClip[4];
    public MeowTextManager meowText;

    [HideInInspector]
    public GameObject myBase;

    

    private Vector3 initialPosition;
    private Quaternion initialRotation;

    void Awake()
    {
        Camera.main.GetComponent<UFOTargetingManager>().kitties.Add(this.gameObject);
    }

    void Start() {
        
        GetComponent<AudioSource>().clip = meows[Random.Range(0, meows.Length)];
        InvokeRepeating("playMeow", 0.0f, Random.Range(2.0f, 3.0f));
        GetComponent<AudioSource>().volume = 0.25f;

        initialPosition = transform.position;
        initialRotation = transform.rotation;

    }

    void playMeow()
    {
        GetComponent<AudioSource>().pitch = Random.Range(0.98f, 1.02f);
        GetComponent<AudioSource>().Play();
        meowText.MeowTrigger();

    }

    void OnSelect()
    {
        GameObject.Find("CannonParent").GetComponent<AmmoManager>().shotsRemaining++;
        GameObject.Find("CannonParent").GetComponent<AmmoManager>().updateShotsRemaining();
        myBase.GetComponent<basePad>().catRescued();
        Camera.main.GetComponent<UFOTargetingManager>().kitties.Remove(Camera.main.GetComponent<UFOTargetingManager>().kitties.Find(kat => kat.gameObject == gameObject));
        Destroy(this.gameObject);
    }

    public void playCoroutine()
    {

        StartCoroutine(ReturnToOrigin());

    }

    IEnumerator ReturnToOrigin()
    {
        for (float t = 0f; t < 2f; t += Time.deltaTime / 2f)
        {
            Vector3 lerpVector = Vector3.Lerp(transform.position, initialPosition, t);
            transform.position = lerpVector;

            yield return null;

        }

    }

    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "shredder")
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            transform.rotation = initialRotation;
            transform.position = initialPosition;
        }

    }
}
