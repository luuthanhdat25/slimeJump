using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCount : MonoBehaviour
{
    public Slime slime;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log(slime.score);
            slime.groundedEffect.Play();
            slime.score++;
            Destroy(gameObject);    
        }
    }
}
