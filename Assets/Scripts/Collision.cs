 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collision : MonoBehaviour
{
    public static bool IsInputEnabled = true;
    
    public Material nextSkyBox;
    public GameObject passSound;
    public GameObject deathSound;
    public GameObject theModel;
    public Text scoreText;
    public Text bestText;
    public Text highscoreText;
    public Text best;
    public static int score = 0;
    public int highscore = 0;
    void Start() 
    {
        IsInputEnabled = true;
        score = 0;
        highscore = PlayerPrefs.GetInt("highscore",0);
        highscoreText.text = "HIGHSCORE: " + highscore.ToString();
        best.text = "BEST: " + highscore.ToString();
    }
    void Update()
    {
        scoreText.text = score.ToString(); 
        bestText.text = score.ToString();
        if (highscore < score) 
            {
                PlayerPrefs.SetInt("highscore",score);
                highscore = PlayerPrefs.GetInt("highscore",0);
                highscoreText.text = "HIGHSCORE: " + highscore.ToString();
                best.text = "NICE! NEW BEST: " + highscore.ToString();
            }
         if (score == 10)  RenderSettings.skybox = nextSkyBox;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Checkpoint" && Collision.IsInputEnabled)
        {
            score+=1;
            GameObject GO = Instantiate(passSound);

            other.GetComponent<Collider>().enabled = false;
            Destroy(other.gameObject,10f);
            Destroy(GO,3f);
        }
        

    }

    void OnCollisionEnter(UnityEngine.Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle" && Collision.IsInputEnabled)
        {   
            Instantiate(deathSound);
            IsInputEnabled = false;
        }
         if (collision.gameObject.tag == "Roof" && Collision.IsInputEnabled)
        {   
            Instantiate(deathSound);
            best.text = "CANT GO THERE!! BEST: " + highscore.ToString();
            IsInputEnabled = false;
        }
    }

    

}
