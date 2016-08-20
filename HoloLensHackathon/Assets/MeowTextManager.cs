using UnityEngine;
using System.Collections;

public class MeowTextManager : MonoBehaviour {

    Vector3 initialPosition;
	// Use this for initialization
	void Start () {

        initialPosition = transform.position;
	}

	// Update is called once per frame
	void Update () {
        transform.LookAt(Camera.main.transform);
        transform.Rotate(0f, 180f, 0f);

        //transform.position += new Vector3(0f, .001f, 0f);
    }

    public void MeowTrigger()
    {
        StartCoroutine(WaitFor(1f));
        StartCoroutine(AnimatedText(initialPosition.y + .4f));
    }

    IEnumerator AnimatedText(float targetValue)
    {

        float startValue = initialPosition.y;
        for (float t = 0f; t < 1f; t += Time.deltaTime / 1f)
        {
            float height = Mathf.Lerp(startValue, targetValue, t);
            transform.position = new Vector3(initialPosition.x, height, initialPosition.z);

            Color newColor = new Color(255, 255, 255, Mathf.Lerp(1f, 0f, t));
            GetComponent<Renderer>().material.color = newColor;

            yield return null;

        }
    }

    IEnumerator WaitFor(float duration)
    {
        yield return new WaitForSeconds(duration);
        transform.position = initialPosition;
    }
}
