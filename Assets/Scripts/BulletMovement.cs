using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BulletMovement : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private float secondsToDestroy = 5f;
    
    private Rigidbody2D mRb; 
    private float timer = 0f;

    void Start()
    {
        mRb = GetComponent<Rigidbody2D>();

        HeroController hero = GameManager.Instance.hero;
            
        mRb.velocity = new Vector2(
            hero.GetPointDirection() == 1 ? speed : -speed, 
            0f
        );
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > secondsToDestroy)
        {
            // Destruir la bala
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
