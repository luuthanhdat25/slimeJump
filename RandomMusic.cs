using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMusic : MonoBehaviour
{
    public AudioSource[] Musics;
    // Start is called before the first frame update
    void Start()
    {
        int ranMusic = Random.Range(0, Musics.Length);
        Musics[ranMusic].Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
