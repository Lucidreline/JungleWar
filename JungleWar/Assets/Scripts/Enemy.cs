using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] float health = 100f;
    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBeforeShots;
    [SerializeField] float MaxTimeBeforeShots;
    [SerializeField] float EnemyProjectileVelocity = 10f;
    [SerializeField] GameObject EnemyBulletPrefab;

    void Start()
    {
        shotCounter = UnityEngine.Random.Range(minTimeBeforeShots, MaxTimeBeforeShots);
    }

    void Update()
    {
        CountDownAndShoot();
    }

    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0f)
        {
            Fire();
            shotCounter = UnityEngine.Random.Range(minTimeBeforeShots, MaxTimeBeforeShots);
        }
    }

    private void Fire()
    {
        GameObject enemyProjectile = 
            Instantiate(EnemyBulletPrefab,
                        transform.position,
                        Quaternion.identity);

        enemyProjectile.GetComponent<Rigidbody2D>().velocity = 
            new Vector2(0, -EnemyProjectileVelocity);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        health -= damageDealer.GetDamage();
        ProcessHit(damageDealer);
        Destroy(other.gameObject);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
