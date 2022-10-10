using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class ThingController : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float maxHealth;
    [SerializeField]
    private float lookDistance = 10;

    private Rigidbody2D mRb;
    private Animator mAnimator;
    private Slider mSlider;
    private Transform mCanvas;
    private Transform mRaycastPoint;
    private float mHealth;

    private bool isAttacking = false;

    private void Start()
    {
        mRb = GetComponent<Rigidbody2D>();
        mAnimator = GetComponent<Animator>();
        mSlider = transform.Find(
            "Canvas"
        ).Find(
            "HealthBar"
        ).Find(
            "Border"
        ).GetComponent<Slider>();

        mCanvas = transform.Find("Canvas");
        mRaycastPoint = transform.Find("RaycastPoint");

        mHealth = maxHealth;
        mSlider.maxValue = maxHealth;
        
    }

    private void Update()
    {
        if (isAttacking)
        {
            mRb.velocity = new Vector2(-speed, mRb.velocity.y);
        }else
        {
            mRb.velocity = new Vector2(0f, mRb.velocity.y);
        }
        
        Look();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Hurt();
        }
        
    }

    private void Look()
    {
        RaycastHit2D hit =  Physics2D.Raycast(
            mRaycastPoint.position,
            Vector3.left,
            lookDistance);

        if (hit)
        {
            isAttacking = true;
        }else
        {
            isAttacking = false;
        }
    }

    private void Hurt()
    {
        mHealth -= 25f;
        // Disminuir la vida en el slider
        mSlider.value = mHealth;

        if (mHealth <= 0f)
        {
            // Morir
            mCanvas.gameObject.SetActive(false);
            mRb.velocity = Vector2.zero;
            mAnimator.SetTrigger("Die");
        }
    }
}
