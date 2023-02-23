using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BulletCollision : MonoBehaviour
{

    private Vector3 norm = new Vector3 (0f,0f,0f);

    public ParticleSystem particleEffect;
    public ParticleSystem deathParticles;
    
    public GameObject powerUpSound;
    public GameObject treeTremble;
    public GameObject eggSound;
    private bool hit = false;

    void  OnCollisionEnter(UnityEngine.Collision collision)
    {
        
        if (collision.gameObject.tag != "FPS") 
        {
            var particles = Instantiate(this.particleEffect);
            particles.transform.position = transform.position;
            particles.transform.rotation = Quaternion.LookRotation(FiringGunBullet.dir);
            GameObject GO = Instantiate(eggSound);
            Destroy(GO,3f);
        }

       
        if (FPSControl.desObstacle && collision.gameObject.tag == "Obstacle" && !hit)
        {

            var particles = Instantiate(deathParticles);
            particles.transform.position = transform.position;
            
            Destroy(collision.transform.parent.gameObject);

            GameObject GOO = Instantiate(treeTremble);
            Destroy(GOO,3f);
            hit = true;
            Collision.score+=1;
         
        }
        
        if (collision.gameObject.tag == "Reward" && !FPSControl.desObstacle && !hit) 
        {
           
            if (collision.gameObject.transform.rotation.eulerAngles != norm ) 
            {
                Destroy(collision.gameObject);
                GameObject GOOO  = Instantiate(powerUpSound);
                Destroy(GOOO,3f);
                FPSControl.desObstacle = true;
                
            }
            collision.gameObject.transform.rotation = Quaternion.Euler(-Vector3.Reflect(this.transform.position, collision.contacts[0].normal) *1/3);
           
          
        }
        if (collision.gameObject.tag != "FPS") Destroy(gameObject);
       

    }

    
}
