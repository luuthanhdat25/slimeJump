using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movePoint : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector2 PointNext;
    public GameObject[] Blocks;
    public float RateDistance;
    public float ConstDistance;
    public float Speed, MinHeight, MaxHeight;
    public Vector2 PosChoosenNext;
    private GameObject BlockChoosenNext, BlockNow;
    private bool Created = false;
    private float RandomX, RandomY;
    void Start()
    {
        ChoosenPos();
        PointNext = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
        BlockNow = Blocks[0];
    }

    public void updateTarget()
    {
        PointNext = PosChoosenNext;
        BlockNow = BlockChoosenNext;
        Created = false;
    }

    public void ChoosenPos()
    {

        float LimitDistance = 1;
        int Key = Random.Range(0, Blocks.Length);
        if (Blocks[Key].name == "RandomVertical")
        {
            LimitDistance = 2;
            MinHeight = -1; MaxHeight = 1;
        }
        if (Blocks[Key].name == "RandomHorizontal")
        {
            LimitDistance = 2;
            MaxHeight = -1.5f; MaxHeight = 1.5f;
        }
        if (Blocks[Key].name == "Block") LimitDistance = 3;

        RandomX = gameObject.transform.position.x + ConstDistance + Random.Range(0, LimitDistance);

        float KeyRandom = Random.Range(MinHeight, MaxHeight);
        while (Mathf.Abs(KeyRandom - RandomY) < 0.4f)
            KeyRandom = Random.Range(MinHeight, MaxHeight);

        Debug.Log("tes " + KeyRandom);
        RandomY = KeyRandom;

        PosChoosenNext = new Vector2(RandomX, RandomY);
        BlockChoosenNext = Blocks[Key];
    }

    // Update is called once per frame
    void Update()
    {
        if (Created == true) return;
        if (Distance(PointNext, new Vector2(gameObject.transform.position.x, gameObject.transform.position.y)) == 0)
        {
            Instantiate(BlockNow, transform.position, Quaternion.identity);
            Created = true;
            ChoosenPos();
        }
        else
            transform.position = Vector3.MoveTowards(gameObject.transform.position, PointNext, Time.deltaTime * Speed);
    }

    private float Distance(Vector2 value1, Vector2 value2)
    {
        float v1 = value1.x - value2.x, v2 = value1.y - value2.y;
        return (float)Mathf.Sqrt((v1 * v1) + (v2 * v2));
    }
}
