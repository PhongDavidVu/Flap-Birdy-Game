using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthQuake : MonoBehaviour
{
    public float dur = 3f;
    public AnimationCurve smooth;
   
    void Update()
    {
       if (Random.Range(1,700) ==1 && Collision.IsInputEnabled && Collision.score >= 10) StartCoroutine(Shake());

    }

    IEnumerator Shake() {
        Vector3 start = this.gameObject.transform.position;
        float elapsed = 0f;

        while(elapsed < dur) 
        {
            elapsed += Time.deltaTime;
            float amplify = smooth.Evaluate(elapsed/ dur);
            gameObject.transform.position = start + Random.insideUnitSphere * amplify;
            yield return null;
        }
        this.gameObject.transform.position = start;
    }
}
