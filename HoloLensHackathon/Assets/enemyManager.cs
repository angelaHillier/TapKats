using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public enum ufoAction {InPursuit, Abducting, Idle};

public class enemyManager : MonoBehaviour {
    public AudioClip[] smashSounds = new AudioClip[4];
    public ufoAction ufoStatus = ufoAction.Idle;

    public Transform transformOfCatToAbduct;
    public float shipSpeed = 0.3f;
    public float abductionTime = 3.0f;
    public AudioClip pursuitSound;
    private Vector3 velocity = Vector3.zero;
    private Vector3 initialScale;
    private Vector3 initialPosition;

    public bool isStartLevel;

    void Start() {

    }

    void findAnUntargetedCat()
    {
        List<GameObject> untargetedCats = new List<GameObject>(Camera.main.GetComponent<UFOTargetingManager>().kitties);
        if (untargetedCats.Count > 0)                                                   // If there are any untargeted Cats,
        {
            untargetedCats = untargetedCats.OrderBy(feline => Vector3.Distance(transform.position, feline.transform.position)).ToList();            // Sort targets List by distance to this UFO.

            for (int i = 0; i < untargetedCats.Count; i++)
            {
                if (!untargetedCats[i].GetComponent<Cat>().ufoHasTargetedMe)
                {
                    transformOfCatToAbduct = untargetedCats[i].transform;                                                                                    // Designate the first (closest) entry in the target List as the one I'm going to travel towards in order to abduct.
                    //Debug.Log("Ship chose " + transformOfCatToAbduct.gameObject.name + " to kidnap.");
                    untargetedCats[i].GetComponent<Cat>().ufoHasTargetedMe = true;                                                                           // Mark that target's "targeted" bool as true.
                    ufoStatus = ufoAction.InPursuit;
                    initialScale = transformOfCatToAbduct.localScale;
                    initialPosition = transformOfCatToAbduct.position;
                    // Mark my "in pursuit" member as true.
                    break;
                }
            }
        }
    }

    IEnumerator stateTimer()
    {
        yield return new WaitForSeconds(20.0f);
        if (transformOfCatToAbduct.gameObject != null)
        {
            Destroy(transformOfCatToAbduct.gameObject);
            Destroy(this.gameObject);
        }
    }

    void Update() {
        transform.Rotate(new Vector3(0.0f, 1.0f, 0.0f));

        if (!isStartLevel)
        {
            switch (ufoStatus)
            {
                case ufoAction.Idle:
                    StopCoroutine(stateTimer());
                    StartCoroutine(stateTimer());
                    findAnUntargetedCat();
                    break;
                case ufoAction.InPursuit:
                    StopCoroutine(stateTimer());
                    StartCoroutine(stateTimer());
                    if (transformOfCatToAbduct != null)
                    {
                        if (!GetComponent<AudioSource>().isPlaying) GetComponent<AudioSource>().PlayOneShot(pursuitSound, 1f);
                        Vector3 targetPosition = transformOfCatToAbduct.position + new Vector3(0.0f, 2.0f, 0.0f);
                        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, shipSpeed);
                        if (Vector3.Distance(this.transform.position, targetPosition) < 0.1f)
                        {
                            ufoStatus = ufoAction.Abducting;
                            //Debug.Log(this.gameObject.name + " is abducting " + transformOfCatToAbduct.gameObject.name);
                        }
                    }
                    else if (transformOfCatToAbduct == null)
                    {
                        ufoStatus = ufoAction.Idle;
                    }
                    break;
                case ufoAction.Abducting:
                    StopCoroutine(stateTimer());
                    StartCoroutine(stateTimer());
                    if (!GetComponent<AudioSource>().isPlaying) GetComponent<AudioSource>().Play();
                    transformOfCatToAbduct.gameObject.GetComponent<SphereCollider>().enabled = false;
                    transformOfCatToAbduct.gameObject.GetComponent<Rigidbody>().useGravity = false;
                    transformOfCatToAbduct.transform.position = Vector3.SmoothDamp(transformOfCatToAbduct.transform.position, transform.position, ref velocity, abductionTime);
                    transformOfCatToAbduct.localScale -= new Vector3(.01f, .01f, .01f);

                    if (Vector3.Distance(transform.position, transformOfCatToAbduct.transform.position) < 0.1f)
                    {
                        Camera.main.GetComponent<UFOTargetingManager>().kitties.Remove(Camera.main.GetComponent<UFOTargetingManager>().kitties.Find(kat => kat.gameObject == transformOfCatToAbduct.gameObject));
                        Destroy(transformOfCatToAbduct.gameObject);
                        ufoStatus = ufoAction.Idle;
                    }
                    if (transformOfCatToAbduct == null)
                    {
                        ufoStatus = ufoAction.Idle;
                    }
                    break;
                default:
                    break;
            }
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Cat")
        {
            Destroy(GameObject.FindGameObjectWithTag("Cat"));
            GameObject.Find("CannonParent").GetComponent<AmmoManager>().shotsRemaining++;
            GameObject.Find("CannonParent").GetComponent<AmmoManager>().updateShotsRemaining();
            AudioSource.PlayClipAtPoint(smashSounds[Random.Range(0, smashSounds.Length)], transform.position);
            GameObject.Find("EnemySpawner").GetComponent<enemySpawner>().spawnedUfos.Remove(gameObject);
            if (transformOfCatToAbduct.gameObject != null)
            {
                transformOfCatToAbduct.gameObject.GetComponent<SphereCollider>().enabled = true;
                transformOfCatToAbduct.gameObject.GetComponent<Rigidbody>().useGravity = true;
                transformOfCatToAbduct.gameObject.GetComponent<Cat>().ufoHasTargetedMe = false;
                transformOfCatToAbduct.localScale = initialScale;
                transformOfCatToAbduct.gameObject.GetComponent<Cat>().playCoroutine();
            }

            this.gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
            this.gameObject.transform.GetChild(1).GetComponent<MeshRenderer>().enabled = false;
            this.gameObject.transform.GetChild(0).GetComponent<MeshExploder>().Explode();
            this.gameObject.transform.GetChild(1).GetComponent<MeshExploder>().Explode();
            Destroy(this.gameObject);
        }
    }
}
