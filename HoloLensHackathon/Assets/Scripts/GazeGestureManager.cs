using UnityEngine;
using UnityEngine.VR.WSA.Input;

public class GazeGestureManager : MonoBehaviour
{
    public static GazeGestureManager Instance { get; private set; }

    // Represents the hologram that is currently being gazed at.
    public GameObject FocusedObject { get; private set; }
    public GameObject cannonManager;
    public GameObject StartButton;
    public Texture startHoverTexture;
    public Texture startTexture;
    public Texture endTexture;
    public Texture endHoverTexture;
    public enemySpawner EnemySpawner;

    GestureRecognizer recognizer;

    // Use this for initialization
    void Start()
    {
        Instance = this;
        // Set up a GestureRecognizer to detect Select gestures.
        recognizer = new GestureRecognizer();
        recognizer.TappedEvent += (source, tapCount, ray) =>
        {
            cannonManager.SendMessageUpwards("OnSelect");
            // Send an OnSelect message to the focused object and its ancestors.
            if (FocusedObject != null)
            {
                FocusedObject.SendMessageUpwards("OnSelect");
            }

        };
        recognizer.StartCapturingGestures();
    }

    // Update is called once per frame
    void Update()
    {
        // Figure out which hologram is focused this frame.
        GameObject oldFocusObject = FocusedObject;

        // Do a raycast into the world based on the user's
        // head position and orientation.
        var headPosition = Camera.main.transform.position;
        var gazeDirection = Camera.main.transform.forward;

        
        RaycastHit hitInfo;
        if (Physics.Raycast(headPosition, gazeDirection, out hitInfo))
        {
            // If the raycast hit a hologram, use that as the focused object.
            FocusedObject = hitInfo.collider.gameObject;

            if (EnemySpawner.isStartLevel) {
                if (FocusedObject == StartButton)
                {
                    StartButton.GetComponent<Renderer>().material.mainTexture = startHoverTexture;
                } else
                {
                    StartButton.GetComponent<Renderer>().material.mainTexture = startTexture;
                }
            }

            if (FocusedObject.tag == "VillagerCat")
            {
                cannonManager.GetComponent<CannonManager>().DontShoot = true;
            } else if (FocusedObject.tag != "VillagerCat")
            {
                cannonManager.GetComponent<CannonManager>().DontShoot = false;
            }

            if (FocusedObject.tag == "DefendText" && GameObject.Find("EndGameText").activeInHierarchy == true)
            {
                GameObject.Find("DefendAgain").GetComponent<Renderer>().material.mainTexture = endHoverTexture;
            }
            else
            {
                if (GameObject.Find("DefendAgain") != null)
                {
                    GameObject.Find("DefendAgain").GetComponent<Renderer>().material.mainTexture = endTexture;
                }
            }
        }
        else
        {
            // If the raycast did not hit a hologram, clear the focused object.
            FocusedObject = null;
        }

        // If the focused object changed this frame,
        // start detecting fresh gestures again.
        if (FocusedObject != oldFocusObject)
        {
            recognizer.CancelGestures();
            recognizer.StartCapturingGestures();
        }
    }
}