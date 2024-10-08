using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D _rigid;
    private float _jumpForce = 5.0f;
    [SerializeField]
    public float speed = 5f; // Increase speed for better movement
    [SerializeField]
    private LayerMask _groundLayer;
    private bool _resetJump = false;

    // Start is called before the first frame update
    void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveCharacter();
    }

    void MoveCharacter()
    {
        float walk = Input.GetAxisRaw("Horizontal") * speed;

        // Apply horizontal movement with speed
        _rigid.velocity = new Vector2(walk, _rigid.velocity.y);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            print("spacebar");
        }

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            Debug.Log("Jump"); // To verify jump is being triggered
            _rigid.velocity = new Vector2(_rigid.velocity.x, _jumpForce); // Apply jump force
            StartCoroutine(ResetJumpRoutine()); // Prevent immediate multiple jumps
        }
    }

    bool IsGrounded()
    {
        // Cast a ray slightly below the player's feet to detect the ground
        Vector2 position = transform.position;
        RaycastHit2D hitInfo = Physics2D.Raycast(position, Vector2.down, 1.0f, _groundLayer);
        Debug.DrawRay(position, Vector2.down * 1.0f, Color.red); // Visualize in Scene view


        if (hitInfo.collider != null)
        {
            Debug.Log("Ground hit: " + hitInfo.collider.name); // Log the name of the object hit
            if (!_resetJump)
            {
                return true;
            }
        }
        else
        {
            Debug.Log("Raycast did not hit any ground");
        }

        return false;
    }

    IEnumerator ResetJumpRoutine()
    {
        _resetJump = true; // Prevent multiple jumps
        yield return new WaitForSeconds(0.1f); // Small delay before allowing another jump
        _resetJump = false;
    }
}
