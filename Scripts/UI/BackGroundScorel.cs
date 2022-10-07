using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundScorel : MonoBehaviour
{
    [Range(-100f, 50f)]
    public float ScrollSpeed = 0.5f;
    private float offset;
    private Material Mat;

    // Start is called before the first frame update
    void Start()
    {
        Mat = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        offset += (Time.deltaTime * ScrollSpeed) / 10f;
        Mat.SetTextureOffset("_MainTex", new Vector2(offset, 0));
    }
}
