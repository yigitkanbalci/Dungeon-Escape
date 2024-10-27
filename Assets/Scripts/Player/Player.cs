using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour, IDamagable
{
    public int Health { get; set; }

    [SerializeField]
    private int health;
    private Rigidbody2D _rigid;
    private float _jumpForce = 6.5f;
    [SerializeField]
    public float speed = 5f; // Increase speed for better movement
    [SerializeField]
    private LayerMask _groundLayer;
    private bool _resetJump = false;
    [SerializeField]
    private bool _grounded = false;
    private PlayerAnimation _anim;

    [SerializeField]
    private int gems;
    private bool isDead = false;

    private SpriteRenderer _spriteRenderer;
    private SpriteRenderer _swordArcRenderer;

    // Start is called before the first frame update
    void Start()
    {
        _rigid = GetComponent<Rigidbody2D>();
        _anim = GetComponent<PlayerAnimation>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _swordArcRenderer = transform.GetChild(1).GetComponent<SpriteRenderer>();
        _rigid.velocity = Vector2.zero;
        Health = 4;

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

    public void Damage()
    {
        print("Health: " + Health);
        Health--;
        new WaitForSeconds(0.1f); // Small delay before allowing another jump
        UIManager.Instance.UpdateLives(Health);
        //Play hit animation
        //check health, kill player if less than 0,
        if (Health < 1)
        {
            if (!isDead)
            {
                _anim.Death();
            }

            isDead = true;
            Destroy(this.gameObject, 1.1f);
        }
        //gameover display
    }


    public void CollectGems(int val)
    {
        gems += val;
        UIManager.Instance.UpdateGemCount(gems);
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
        Debug.DrawRay(position, Vector2.down, Color.green); 

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

            _swordArcRenderer.transform.localRotation = Quaternion.Euler(106f, 0f, 0f);
        }
        else
        {
            _spriteRenderer.flipX = false;
            _swordArcRenderer.flipX = false;
            _swordArcRenderer.flipY = false;

            Vector3 newPos = _swordArcRenderer.transform.localPosition;
            newPos.x = 1.01f;
            _swordArcRenderer.transform.localPosition = newPos;

            _swordArcRenderer.transform.localRotation = Quaternion.Euler(66f, 0f, 0f);

        }
    }

    IEnumerator ResetJumpRoutine()
    {
        _resetJump = true; // Prevent multiple jumps
        yield return new WaitForSeconds(0.1f); // Small delay before allowing another jump
        _resetJump = false;
    }

    public int GetGems()
    {
        return this.gems;
    }

    public void SetGems(int val)
    {
        this.gems = val;
    }
}
