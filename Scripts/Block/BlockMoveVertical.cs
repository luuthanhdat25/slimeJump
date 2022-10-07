using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMoveVertical : MonoBehaviour
{
    public float Max, Min, Speed = 1;
    private float TimeCount = 0, TimeRate = 0.005f;
    private float MinRange, MaxRange;
    private Vector2 DownPos, UpPos, Target;
    bool requestDestroy = false;
    private Slime RequestToSlime;
    private Animator Ani;
    bool NullSetParent = false;
    // Start is called before the first frame update
    void Start()
    {
        Ani = GetComponent<Animator>();
        Ani.enabled = false;
        RequestToSlime = GameObject.Find("Player").GetComponent<Slime>();
        MinRange = Min;
        MaxRange = Max;
        gameObject.name = gameObject.name.Replace("(Clone)", "");

        if (gameObject.name == "RandomVertical")
        {
            Speed = Random.Range(0.8f, 1.3f);
            MinRange = Random.Range(1, 1.5f);
            MaxRange = Random.Range(1, 1.5f);       
        }

       /* if (gameObject.tag == "Level")
        {*/
        
        DownPos = new Vector2(transform.position.x, transform.position.y - MinRange);
        UpPos = new Vector2(transform.position.x, transform.position.y + MaxRange);
        Target = DownPos;
    }

    // Update is called once per frame
    void Update()
    {
        if(requestDestroy)
        {
            if (Mathf.Abs(TimeCount - Time.time) >= (3 - TimeRate))
            {
                NullSetParent = true;
                Destroy(gameObject);
            }
        }
        if (Distance(new Vector2(transform.position.x, transform.position.y), Target) <= 0.1)
            Target = (Target == DownPos) ? UpPos : DownPos;
        else
            transform.position = Vector3.MoveTowards(transform.position, Target, Speed * Time.deltaTime);
    }

    private float Distance(Vector2 value1, Vector2 value2)
    {
        float v1 = value1.x - value2.x, v2 = value1.y - value2.y;
        return (float)Mathf.Sqrt((v1 * v1) + (v2 * v2));
    }

    private void OnCollisionEnter2D( Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            if (NullSetParent)
            {
                collision.gameObject.transform.SetParent(null);
            }
            else
            {
                requestDestroy = true;
                Ani.enabled = true;
                TimeCount = Time.time;
                collision.gameObject.transform.SetParent(transform);
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            collision.gameObject.transform.SetParent(null);
        }
    }
}
