using UnityEngine;
using System.Collections;
using UnityEngine.VR.WSA.Input;

public class CursorSelect : MonoBehaviour {

    public GameObject cursorReference;
    private bool HandDetected;
	// Use this for initialization
	void Start () {

        InteractionManager.SourceDetected += InteractionManager_SourceDetected;
        InteractionManager.SourceLost += InteractionManager_SourceLost;

    }
	
	// Update is called once per frame
	void Update () {

        if (HandDetected)
        {
            cursorReference.GetComponent<Transform>().localScale = new Vector3(1.25f, 1.25f, 1.25f);
        } else
        {
            cursorReference.GetComponent<Transform>().localScale = new Vector3(1f, 1f, 1f);
        }

    }

    private void InteractionManager_SourceDetected(InteractionSourceState hand)
    {
        HandDetected = true;
    }

    private void InteractionManager_SourceLost(InteractionSourceState hand)
    {
        HandDetected = false;
    }

    public void SelectFade()
    {

        StartCoroutine(FadeTo(1f, 2.5f));

    }

    IEnumerator FadeTo(float targetValue, float duration)
    {
        float alpha = 0f;
        for (float t = 0f; t < 1f; t += Time.deltaTime / duration)
        {
            Color newColor = new Color(Mathf.Lerp(alpha, targetValue, t), Mathf.Lerp(alpha, targetValue, t), 1, 1);
            cursorReference.GetComponent<Renderer>().material.color = newColor;

            yield return null;

        }
    }
}
