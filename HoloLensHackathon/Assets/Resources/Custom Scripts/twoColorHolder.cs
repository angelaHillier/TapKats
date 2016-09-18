using UnityEngine;
using System.Collections;

public class twoColorHolder : MonoBehaviour {
    public Material[] twoColors;

    void Start()
    {
        twoColors = new Material[2];
    }
}
