using UnityEngine;
using System.Collections;

public class CannonManager : MonoBehaviour {

    public GameObject cameraRef;
    public GameObject cannonParent;
    public GameObject cannonRef;
    public GameObject spawnSocket;
    public GameObject projectileCat;
    public GameObject cylinderRef;
    public GameObject HelpText;

    private GameObject projectileClone;
    private float headRotation;
    private Quaternion targetRotation;
    private float yVelocity = 0.0F;
    private bool HasFiredOnce = false;

    public AudioClip[] meowSounds;
    public bool DontShoot { get; set; }
    // Use this for initialization
    void Start () {

        DontShoot = false;
    }
	
	// Update is called once per frame
	void Update () {

        headRotation = Mathf.SmoothDampAngle(headRotation, cameraRef.transform.rotation.eulerAngles.y, ref yVelocity, .24f);
        targetRotation.eulerAngles = new Vector3(0f, headRotation, 0f);
        cannonParent.transform.rotation = targetRotation;


        Debug.Log("dont shoot = " + DontShoot);

 //       if (Input.GetKeyDown("w"))
 //       {
 //           if (!HasFiredOnce)
 //           {
 //               HelpText.SetActive(false);
 //               HasFiredOnce = true;
 //           }

 //           if (GetComponent<AmmoManager>().shotsRemaining > 0 && !DontShoot)
 //           {
 //               GetComponent<AudioSource>().pitch = Random.Range(0.98f, 1.02f);
 //               GetComponent<AudioSource>().Play();

 //               projectileClone = (GameObject)Instantiate(projectileCat, spawnSocket.transform.position, Camera.main.transform.rotation);
 //               projectileClone.AddComponent<AudioSource>();
 //               projectileClone.GetComponent<AudioSource>().volume = Random.Range(1.0f, 1.5f);
 //               projectileClone.GetComponent<AudioSource>().pitch = Random.Range(0.95f, 1.05f);
 //               projectileClone.GetComponent<AudioSource>().maxDistance = 10.0f;
 //               projectileClone.GetComponent<AudioSource>().dopplerLevel = 0.5f;
 //               projectileClone.GetComponent<AudioSource>().spatialBlend = 1.0f;
 //               projectileClone.GetComponent<AudioSource>().spread = 360.0f;
 //               projectileClone.GetComponent<AudioSource>().PlayOneShot(meowSounds[Random.Range(0, meowSounds.Length)]);

 //               cannonRef.GetComponent<Animation>().Rewind();
 //               projectileClone.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * 1000f);
 //               cannonRef.GetComponent<Animation>().Play();

 //               GetComponent<AmmoManager>().shotsRemaining--;
 //               GetComponent<AmmoManager>().updateShotsRemaining();
 //           }

 //           if (GetComponent<AmmoManager>().shotsRemaining == 0)
 //           {
 //               GetComponent<AmmoManager>().ToggleEmptyAlert(true);
 //               cameraRef.gameObject.GetComponent<UFOTargetingManager>().showGameOverMenu();
 //               StartCoroutine(FadeTo(0f, 2f));
 //           }
 //           //if (projectileClone != null) Destroy(projectileClone, 5f);
 //       }
	}

    void OnSelect()
    {
       // Debug.Log("FIRE!");
        if (!HasFiredOnce)
        {
            HelpText.SetActive(false);
            HasFiredOnce = true;
        }

        if (GetComponent<AmmoManager>().shotsRemaining > 0 && !DontShoot)
        {
            GetComponent<AudioSource>().pitch = Random.Range(0.98f, 1.02f);
            GetComponent<AudioSource>().Play();

            projectileClone = (GameObject)Instantiate(projectileCat, spawnSocket.transform.position, Camera.main.transform.rotation);
            projectileClone.AddComponent<AudioSource>();
            projectileClone.GetComponent<AudioSource>().volume = Random.Range(1.0f, 1.5f);
            projectileClone.GetComponent<AudioSource>().pitch = Random.Range(0.95f, 1.05f);
            projectileClone.GetComponent<AudioSource>().maxDistance = 10.0f;
            projectileClone.GetComponent<AudioSource>().dopplerLevel = 0.5f;
            projectileClone.GetComponent<AudioSource>().spatialBlend = 1.0f;
            projectileClone.GetComponent<AudioSource>().spread = 360.0f;
            projectileClone.GetComponent<AudioSource>().PlayOneShot(meowSounds[Random.Range(0, meowSounds.Length)]);

            cannonRef.GetComponent<Animation>().Rewind();
            projectileClone.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * 1000f);
            cannonRef.GetComponent<Animation>().Play();

            GetComponent<AmmoManager>().shotsRemaining--;
            GetComponent<AmmoManager>().updateShotsRemaining();
        }

        if (GetComponent<AmmoManager>().shotsRemaining == 0)
        {
            GetComponent<AmmoManager>().ToggleEmptyAlert(true);
            StartCoroutine(FadeTo(0f, 2f));
        }
        //if (projectileClone != null) Destroy(projectileClone, 5f);
    }

    IEnumerator FadeTo(float targetValue, float duration)
    {
        float alpha = 1f;
        for (float t = 0f; t < 1f; t += Time.deltaTime / duration)
        {
            Color newColor = new Color(255, 114, 114, Mathf.Lerp(alpha, targetValue, t));
            GetComponent<AmmoManager>().EmptyText.GetComponent<Renderer>().material.color = newColor;

            yield return null;

        }
        GetComponent<AmmoManager>().ToggleEmptyAlert(false);

    }
}
