using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {

    public AudioSource audioSource;
    public GameObject bigSphere;
    public GameObject miniSphere;
    public Material miniSphereMat;

    public Color colorStart = Color.red;
    public Color colorEnd = Color.green;


    float minD = 0;
    float maxD = 5;
    float avgD;



    float distance;

    // Use this for initialization
    void Start () {

        distance = Vector2.Distance(bigSphere.transform.position, miniSphere.transform.position);
        
        avgD = (maxD - distance) / maxD;
        audioSource.pitch = avgD;


    }
	
	// Update is called once per frame
	void Update () {
        distance = Vector2.Distance(bigSphere.transform.position, miniSphere.transform.position);

        if (distance >= 5)
        {
            distance = distance - 5;
        }
        else
        {
            distance = 5 - distance;
        }



        avgD = (maxD - distance) / maxD;
        audioSource.pitch = avgD;
        miniSphereMat.color = Color.Lerp(colorStart, colorEnd, avgD);


    }
}
