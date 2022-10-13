using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroController : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float jumpSpeed;
    [SerializeField]
    private float raycastDistance;
    [SerializeField]
    private GameObject prefabBullet;

    private float mMovement = 0f;    
    private bool mIsJumpPressed = false;
    private bool mIsJumping = false;
    private Rigidbody2D mRb;
    private Transform mRaycastPoint;
    private CapsuleCollider2D mCollider;
    private Vector3 mRaycastPointCalculated;
    private Animator mAnimator;
    private Transform mBulletSpawnPoint;
    public int maxJumpCount = 2;
    public int jumpsRemaining = 1;
    private Slider mSlider;
    private float mPower;
    private bool Teleport = false;

    void Start()
    {
        mRb = GetComponent<Rigidbody2D>();
        mRaycastPoint = transform.Find("RaycastPoint");
        mCollider = GetComponent<CapsuleCollider2D>();
        mAnimator = GetComponent<Animator>();
        mBulletSpawnPoint = transform.Find("BulletSpawnPoint");
        mSlider = transform.Find(
            "Canvas"
        ).Find(
            "PowerBar"
        ).Find(
            "Border"
        ).GetComponent<Slider>();

        mPower = minPower;
        mSlider.minValue = minPower;
    }

    void FixedUpdate()
    {
        
        //transform.position += mMovement * speed * Time.fixedDeltaTime * Vector3.right;
        mRb.velocity = new Vector2(
            mMovement * speed,
            mRb.velocity.y
        );

        if (mRb.velocity.x != 0f)
        {
            transform.localScale = new Vector3(
                mRb.velocity.x < 0f ? -1f : 1f,
                transform.localScale.y,
                transform.localScale.z
            );
        }

        IsJumping();

        if (mIsJumpPressed)
        {
            // Comenzar salto
            Jump();
        }

        // Informativo
        Debug.DrawRay(
            mRaycastPointCalculated,
            Vector2.down * raycastDistance,
            mIsJumping == true ? Color.green : Color.white
        );
    }
    public int playerJumps;
    private int tempPlayerJumps;
    void Update()
    {
        mMovement = Input.GetAxis("Horizontal");

        if (mMovement > 0f || mMovement < 0f )
        {
            mAnimator.SetBool("isMoving", true);
        }else
        {
            mAnimator.SetBool("isMoving", false);
        }

        //mIsJumpPressed = Input.GetKeyDown(KeyCode.Space);
        if (!mIsJumping && Input.GetKeyDown(KeyCode.Space) && (jumpsRemaining > 0))
        {
            mIsJumpPressed = true;
            jumpsRemaining -= 1;
        }

        if (Input.GetMouseButtonDown(0))
        {
            // Animacion de disparo
            mAnimator.SetTrigger("shoot");
            Fire();
        }

        mAnimator.SetBool("isJumping", mIsJumping);
        mAnimator.SetBool("isFalling", mRb.velocity.y < 0f);
    }

    public int extrajumps = 2;

    private void Jump()
    {
        mRb.AddForce(Vector3.up * jumpHeight, ForceMode2D.Impulse);
        mIsJumping = true;
        mIsJumpPressed = false;
        extrajumps = +1;
    }
    private void Doublejump ()
    {
        mRb.AddForce(Vector3.up * jumpHeight, ForceMode2D.Impulse);
        mIsJumping = true;
        mIsJumpPressed = false;
    }
    

    private void IsJumping()
    {
        mRaycastPointCalculated = new Vector3(
            mCollider.bounds.center.x,
            mCollider.bounds.center.y - mCollider.bounds.extents.y,
            transform.position.z
        );

        RaycastHit2D hit = Physics2D.Raycast(
            mRaycastPointCalculated,// Posicion origen
            Vector2.down,// Direccion
            raycastDistance// Distancia
        );
        if (hit)
        {
            // Hay una colision, esta en el suelo
            mIsJumping = false;
            jumpsRemaining = maxJumpCount;
        }
    }

    private void Fire()
    {
        Instantiate(
            prefabBullet, 
            mBulletSpawnPoint.position, 
            Quaternion.identity
        );
        Power();
    }

    public int GetPointDirection()
    {
        return (int)transform.localScale.x;
    }

    private void Power()
    {
        mPower += 25f;
        mPower.value = mPower;

        if (mPower = 100f)
        {
            Teleport = true;
        }
    }
    
    public float jumpHeight = 10;
    
}
