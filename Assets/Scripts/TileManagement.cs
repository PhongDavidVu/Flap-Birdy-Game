using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManagement : MonoBehaviour
{
  
    public GameObject prefab;
    private float spawn = 100;
    private float length = 100;
   
    public Transform bird;
    private List<GameObject> active = new List<GameObject>();
    void Start()
    {
       
      
        
    }
    void Update()
    {

        
        if (bird.position.x > spawn-1.4*length)
        {
            Spawning();
            if (active.Count > 3) deleteActive();
        
            
        }
        
    }
    public void Spawning()
    {
        GameObject nObject = Instantiate(prefab,transform.right*spawn,transform.rotation);
        spawn+=length;
        Environment.spawned++;
        active.Add(nObject);

    }
    private void deleteActive()
    {
        Destroy(active[0]);
        active.RemoveAt(0);
    }
}
