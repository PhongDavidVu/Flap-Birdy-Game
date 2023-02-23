using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FPSControl : MonoBehaviour
{
    public static bool gunOn = false;
    public static bool desObstacle = false;
    private float duration = 3;
    private bool increase = true;
    private float jump = 3.5f;
    private float acc = 1f;
   
    public Camera fpsCam;
    public Camera tpsCam;

    private float hor = 0.0f;
    private float ver = 0.0f;
    public GameObject jumpSound;
    public GameObject gun;
    public GameObject rainPrefab;
    private GameObject rain;
    public GameObject StartUI;
    private Vector3 rotationBy;
    public Text cooldown;
    
    private Rigidbody rb;
    private bool start = false;
    private bool fpsGunTemp;

    void Start()
    {
        gunOn = false;
        fpsGunTemp = false;
        desObstacle = false;
        fpsCam.enabled = true;
        tpsCam.enabled = false;
        increase = true;
        rain = Instantiate(rainPrefab);
        rain.SetActive(false);
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    void Update()
    {
        if (Input.GetKeyDown("c")) 
        {
            fpsCam.enabled = !fpsCam.enabled;
            tpsCam.enabled = !tpsCam.enabled;
            if (tpsCam.enabled && start) {
                if (gunOn){
                gun.SetActive(true); 
                gunOn = false;
                } else {
                    gun.SetActive(false); 
                    gunOn = false;
                }
            }
            if (fpsCam.enabled && start) {
                if (fpsGunTemp){
                gun.SetActive(true); 
                gunOn = true;
                } else {
                    gun.SetActive(false); 
                    gunOn = false;
                }
            }
        }
        if (!start && Input.GetKeyDown("escape"))
        {
            
            StartUI.SetActive(false);
        }
        if (!start && Input.GetKeyDown("space")){
             start = true;
             StartUI.SetActive(false);
        }
        if (start && Time.timeScale != 0 && Collision.IsInputEnabled)
        {
            if (Collision.score % 5 == 0 && increase){ 
                acc +=0.25f;
                increase = false;
            }
            else if (Collision.score % 5 != 0) increase = true;

            rb.useGravity = true;
            if (Input.GetKeyDown("space") && Collision.IsInputEnabled)
            {
                rb.velocity = Vector3.up * jump;
                
                GameObject GO = Instantiate(jumpSound);
                Destroy(GO,2f);
            }
           
            gameObject.transform.Translate(Vector3.right * Time.deltaTime*acc,Space.World);
            tpsCam.transform.Translate(Vector3.right * Time.deltaTime*acc);
            if(Collision.score >= 10) rain.SetActive(true);
            rain.transform.Translate(Vector3.right * Time.deltaTime*acc);
           
            if (fpsCam.enabled && Input.GetKeyDown("a"))
            {   
                if (!gunOn) gun.SetActive(true);
                else gun.SetActive(false);
                gunOn = !gunOn;
                fpsGunTemp = gunOn;
            }
            // camera control
        
            if (fpsCam.enabled)
            {
                hor = Input.GetAxis("Mouse X");
                ver = Input.GetAxis("Mouse Y");    
                rotationBy = new Vector3(0, hor * -1, ver * -1);
                transform.eulerAngles -= rotationBy;
            }
            
            if (desObstacle) 
            {
                cooldown.text = "TIME LEFT: 0:0" + ((int)duration+1).ToString();
                cooldown.gameObject.SetActive(true);
                if (duration > 0) duration -= Time.deltaTime;
                
                else 
                {
                    duration = 3;
                    desObstacle = false;
                    cooldown.gameObject.SetActive(false);
                }
            }

        }
    }
}