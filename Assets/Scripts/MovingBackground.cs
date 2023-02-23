using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBackground : MonoBehaviour
{
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    
        transform.position += new Vector3(-400 * Time.deltaTime, 0);
        if (transform.position.x < -1350) transform.position = new Vector3(3200, transform.position.y);
    }
}
  