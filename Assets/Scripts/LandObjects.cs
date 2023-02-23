using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandObjects : MonoBehaviour
{
    void Start()
    {
        FindLand();
    }
  
    public void FindLand()
    {
        Ray ray = new Ray(transform.position, -transform.up);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)) transform.position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
        
        else
        {
            ray = new Ray(transform.position, transform.up);
            if (Physics.Raycast(ray, out hit)) transform.position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
            
        }
    }
}