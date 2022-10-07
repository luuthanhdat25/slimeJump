using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPoint : MonoBehaviour
{
    Vector2 direction;
    Vector2 playerPos;

    private Slime pc;

    public GameObject pointPrefab;
    public GameObject[] points;
    public int numberOfPoints;
    private void Start()
    {
        pc = GetComponent<Slime>();
        points = new GameObject[numberOfPoints];
        playerPos = transform.position;

        for (int i = 0; i < numberOfPoints; i++)
        {
            points[i] = Instantiate(pointPrefab, transform.position, Quaternion.identity);
        }
    }
    private void Update()
    {
        direction = pc.jumpDirec;
        for (int i = 0; i < points.Length; i++)
        {
            points[i].transform.position = transform.position;
        }

        if (Input.GetKey("space")  && pc.canJump )
        {
            for (int i = 0; i < points.Length; i++)
            {
                points[i].transform.position = PointPosition(i * 0.05f);
            }
        }

 

    }

    Vector2 PointPosition(float t)
    {
        Vector2 currentPointPos = (Vector2)transform.position + (direction * t) + 0.56f * Physics2D.gravity * (t * t);
        return currentPointPos;
    }



}
