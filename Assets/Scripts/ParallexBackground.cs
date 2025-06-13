using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallexBacground : MonoBehaviour
{
    private GameObject cam;

    //background move speed
    [SerializeField] private float parallexEffect;

    private float xPosition;
    private float length;

    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("Main Camera");

        length = GetComponent<SpriteRenderer>().bounds.size.x;
        xPosition = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        // move diration and speed
        float distanceMoved = cam.transform.position.x * (1 - parallexEffect);
        float xDistanceToMove = cam.transform.position.x * parallexEffect;

        transform.position = new Vector3 (xPosition * xDistanceToMove, transform.position.y);

        if(distanceMoved > xPosition + length)
        {
            xPosition += length;
        }
        else if (distanceMoved < xPosition - length)
        {
            xPosition -= length;
        }
    }
}
