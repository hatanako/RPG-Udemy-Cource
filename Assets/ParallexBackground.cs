using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallexBacground : MonoBehaviour
{
    private GameObject cam;

    //background move speed
    [SerializeField] private float parallexEffectX;
    [SerializeField] private float parallexEffectY;

    private float xPosition;
    private float yPosition;

    // Start is called before the first frame update
    void Start()
    {
        cam = GameObject.Find("Main Camera");

        xPosition = transform.position.x;
        yPosition = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        // move diration and speed
        float xDistanceToMove = cam.transform.position.x * parallexEffectX;
        float yDistanceToMove = cam.transform.position.y * parallexEffectY;

        transform.position = new Vector3 (xPosition * xDistanceToMove, yPosition * yDistanceToMove);
    }
}
