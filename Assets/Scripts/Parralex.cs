using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parralex : MonoBehaviour
{
    float length, StartPos;
    public GameObject cam;
    public float parrallaxEffect;
    
    void Start()
    {
        StartPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        float temp = cam.transform.position.x * (1 - parrallaxEffect);
        float dist = cam.transform.position.x * parrallaxEffect;
        transform.position = new Vector3(StartPos + dist, transform.position.y, transform.position.z);

        if (temp > StartPos + length) StartPos += length;
        else if(temp < StartPos - length) StartPos -= length;
    
    
    
    
    }
}
