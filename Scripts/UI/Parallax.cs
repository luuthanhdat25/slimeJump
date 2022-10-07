using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Cam;
    public float parallaxNumber;
    public float Length, Pos;
    void Start()
    {
        Pos = transform.position.x;
        Length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        float Res = Cam.transform.position.x * (1 - parallaxNumber);
        float Dis = Cam.transform.position.x * parallaxNumber;
        transform.position = new Vector3(Pos + Dis, transform.position.y, transform.position.z);
        if (Res > Pos + Length) Pos += Length;
        else if (Res < Pos - Length) Pos -= Length;
    }
}
