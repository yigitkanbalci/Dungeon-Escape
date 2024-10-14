using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D _rigid;
    private float _jumpForce = 6.0f;
    [SerializeField]
    public float speed = 5f; // Increase speed for better movement
    [SerializeField]
    private LayerMask _groundLayer;
    private bool _resetJump = false;
    [SerializeField]
    private bool _grounded = false;
    private PlayerAnimation _anim;

    private SpriteRenderer _spriteRenderer;
    private SpriteRenderer _swordArcRenderer;

    // Start is called before the first frame update
    void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _anim = GetComponent<PlayerAnimation>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _swordArcRenderer = transform.GetChild(1).GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        MoveCharacter();

        if (Input.GetMouseButtonDown(0) && IsGrounded() == true)
        {
            _anim.Attack();
        }
    }

    void MoveCharacter()
    {
        float walk = Input.GetAxisRaw("Horizontal") * speed;

        _grounded = IsGrounded();

        if (walk > 0)
        {
            Flip(false);
        } else if (walk < 0)
        {
            Flip(true);
        }
       
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            _rigid.velocity = new Vector2(_rigid.velocity.x, _jumpForce); // Apply jump force
            _anim.Jump(true);
            StartCoroutine(ResetJumpRoutine()); // Prevent immediate multiple jumps
        }

        _rigid.velocity = new Vector2(walk, _rigid.velocity.y);

        _anim.Move(walk);
    }

    bool IsGrounded()
    {
        // Cast a ray slightly below the player's feet to detect the ground
        Vector2 position = transform.position;
        RaycastHit2D hitInfo = Physics2D.Raycast(position, Vector2.down, 1.0f, _groundLayer);
        Debug.DrawRay(position, Vector2.down, Color.green); // Visualize in Scene view

        if (hitInfo.collider != null)
        {
            if (!_resetJump)
            {
                _anim.Jump(false);
                return true;
            }
        }

        return false;
    }

    private void Flip(bool flip)
    {
        if (flip)
        {
            _spriteRenderer.flipX = true;
            _swordArcRenderer.flipX = true;
            _swordArcRenderer.flipY = true;

            Vector3 newPos = _swordArcRenderer.transform.localPosition;
            newPos.x = -1.01f;
            _swordArcRenderer.transform.localPosition = newPos;
        }
        else
        {
            _spriteRenderer.flipX = false;
            _swordArcRenderer.flipX = false;
            _swordArcRenderer.flipY = false;

            Vector3 newPos = _swordArcRenderer.transform.localPosition;
            newPos.x = 1.01f;
            _swordArcRenderer.transform.localPosition = newPos;

        }
    }

    IEnumerator ResetJumpRoutine()
    {
        _resetJump = true; // Prevent multiple jumps
        yield return new WaitForSeconds(0.1f); // Small delay before allowing another jump
        _resetJump = false;
    }
}
