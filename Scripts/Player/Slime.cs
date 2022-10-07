using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slime : MonoBehaviour
{
    [SerializeField] private AudioSource jumpSoundEffect;
    [SerializeField] public AudioSource groundedEffect;
    [SerializeField] private AudioSource dieEffect;
    public int score = 0;
    public BlockStatic bs;

    public GameObject PauseGame;
    public GameObject Tutor;
    public Text TextScore;
    public Text scoreFinish;
    public GameObject Cam;
    public bool isGrounded, RequestJump;
    public Transform groundCheck;
    private Rigidbody2D rb;
    public bool canJump, StartTime;

    public CanvaEdit RequestCanva;
    public GameObject RankTable;
    public float groundCheckRadius, TimePlay = 0;
    public LayerMask whatIsGround;

    public Vector2 jumpDirec;
    public float jumpVaule = 0.0f;
    public float xValue = 0.0f;

    private movePoint RequestToPoint;
    private Animator anim;
    private float forceJump = 0;

    public string NameSlime, IDSlime;

    private void Start()
    {
        Time.timeScale = 1;
        RankTable.SetActive(false);
        changeInfo(InputPlayer.stringA, InputPlayer.stringB);
        anim = GetComponent<Animator>();
        RequestToPoint = GameObject.Find("Point").GetComponent<movePoint>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && Time.timeScale != 0)
        {
            Time.timeScale = 0;
            PauseGame.SetActive(true);
            Tutor.SetActive(false);
        }
        if (StartTime == true) TimePlay = TimePlay + Time.deltaTime;
        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("yVelocity", rb.velocity.y);
        CheckIfCanJump();
        Jump();
        jumpDirec = new Vector2(xValue, jumpVaule);
        
    }
    private void FixedUpdate()
    {    
        Vector3 targetPos = new Vector3(gameObject.transform.position.x + 4.5f, Cam.transform.position.y, Cam.transform.position.z);
        Cam.transform.position = Vector3.Lerp(Cam.transform.position, targetPos, 0.5f);
        CheckSurroundings();
    }

    private void CheckIfCanJump()
    {
        if(isGrounded && rb.velocity.y < 0.001f)
        {
            canJump = true;
        }
        else
        {
            canJump = false;
        }
    }

    public void changeInfo(string Name, string ID)
    {
        NameSlime = Name; IDSlime = ID;
    }
    private void Jump()
    {
        //increase Vector when space down
        if (Input.GetKey("space") && canJump)
        {
            RequestJump = true;

            if (jumpVaule >= 0 && jumpVaule <= 8f && xValue >= 0 && xValue <= 6f)
            {
                jumpVaule += 0.15f;
                xValue += 0.072f;
                forceJump += 0.1f;
            }
            anim.SetFloat("isPreJump",forceJump);
        }

        //Jump action
        if (Input.GetKeyUp("space")&& canJump)
        {
            jumpSoundEffect.Play();  
            RequestToPoint.updateTarget();
            rb.velocity = jumpDirec;
            jumpVaule = 0;
            xValue = 0;
            isGrounded = false;
            RequestJump = false;
            forceJump = 0f;
            anim.SetFloat("isPreJump", forceJump);
        }
    }
    private void CheckSurroundings()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius); 
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "BlockStart")
        {
            StartTime = true;
            groundedEffect.Play();
            if (collision.gameObject.GetComponent<Visit>().Visited == true)
                return;
            collision.gameObject.GetComponent<Visit>().Visited = true;
            RequestJump = false;
        }

        if (collision.gameObject.CompareTag("Ground") && rb.velocity.y < 0.001f)
        {
            if (collision.gameObject.GetComponent<Visit>().Visited == true)
                return;
            collision.gameObject.GetComponent<Visit>().Visited = true;
            RequestJump = false;
            groundedEffect.Play();
            score++;
            TextScore.text = score.ToString();
            scoreFinish.text = score.ToString();
        }
        if (collision.gameObject.CompareTag("Die"))
        {
            dieEffect.Play();
            Destroy(gameObject, 2f);
            RankTable.SetActive(true);
            GameObject.Find("ScoreTable").SetActive(false);
            SavingPlayer.RequestAddPalyer(NameSlime, IDSlime, score, TimePlay);
            RequestCanva.UpdateDataBox();
            Time.timeScale = 0;
        }
    }

}