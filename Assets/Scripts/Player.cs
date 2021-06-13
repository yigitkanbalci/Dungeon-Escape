using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //initiate Rigidbody
    private Rigidbody2D myRigidbody;

    //initialize animator
    private Animator myAnimator;

    //score keeper
    private int score; //score value
    public Text totalScore; //score text

    //Audio effects
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private AudioClip coinSound;
    [SerializeField]
    private AudioClip keySound;
    [SerializeField]
    private GameObject keyExists; //flag for key in inventory
    [SerializeField]
    private float speed = 10; //horizontal movement speed
    private bool lookRight; //direction of sprite image (default is right)

    // Start is called before the first frame update
    void Start()
    {
        lookRight = true; //facing right
        score = 0; //initialize score to 0

        myRigidbody = GetComponent<Rigidbody2D>(); //get rigidbody
        myAnimator = GetComponent<Animator>(); //get animator
    }

    // Update is called once per frame
    void Update()
    {
        float hz = Input.GetAxis("Horizontal"); //get horizontal movement with arrow keys

        basicHorizontal(hz);
        switchDirection(hz);
        
    }

    private void basicHorizontal(float hz)
    {   
        myRigidbody.velocity = new Vector2(hz * speed, myRigidbody.velocity.y); //move rigidbody object
        myAnimator.SetFloat("heroSpeed", Mathf.Abs(hz));
    }

    private void switchDirection(float hz)
    {
        if((hz > 0 && !lookRight) || (hz < 0 && lookRight))
        {
            lookRight = !lookRight;
            Vector3 direction = transform.localScale;
            direction.x *= -1;
            transform.localScale = direction;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Coin")
        {
            other.gameObject.SetActive(false);
            score += 100;
            scoreUpdate(score);
            audioSource.PlayOneShot(coinSound, 1f);
        }

        if(other.gameObject.tag == "Key")
        {
            other.gameObject.SetActive(false);
            keyExists.SetActive(true);
            audioSource.PlayOneShot(keySound, 1f);
        }
    }

    void scoreUpdate(int count)
    {
        string scoreDisplay = "Score: " + count.ToString();
        totalScore.text = scoreDisplay;
    }
}
