using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    //configs params
    [Header("Player")]
    [SerializeField] float moveSpeed = 10;
    [SerializeField] float padding = 0f;
    [SerializeField] int PlayerHealth = 200;

    [Header("Projectile")]
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileFiringPeriod = 0.1f;

    Coroutine firingCoroutine;

    float xMin;
    float xMax;
    float yMin;
    float yMax;

    // Start is called before the first frame update
    void Start()
    {
        setUpMoveBoundries();
    }

    

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
           firingCoroutine = StartCoroutine(FireContinuously());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }
    }

    

    IEnumerator FireContinuously()
    {
        while (true)
        {
            GameObject bullet =
                Instantiate(bulletPrefab, transform.position, Quaternion.identity)
                as GameObject;
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            yield return new WaitForSeconds(projectileFiringPeriod);

        }

    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        var newXPos = Mathf.Clamp(transform.position.x + deltaX,xMin,xMax);
        var newYPos = Mathf.Clamp(transform.position.y + deltaY,yMin, yMax) ;
        transform.position = new Vector2(newXPos, newYPos);
        
    }

    private void setUpMoveBoundries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damage = other.GetComponent<DamageDealer>();
        PlayerHealth -= damage.GetDamage();
        if(PlayerHealth <= 0)
        {
            Destroy(gameObject);
        }
        Destroy(other.gameObject);
    }
}
