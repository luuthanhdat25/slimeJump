using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockStatic : MonoBehaviour
{
    bool requestDestroy = false;
    private Animator Ani;
    public bool Visited = false;
    // Start is called before the first frame update
    void Start()
    {
        Ani = GetComponent<Animator>();
        Ani.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (requestDestroy) Destroy(gameObject, 3f);
    }
   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            requestDestroy = true;
            Ani.enabled = true;
        }
    }
}
