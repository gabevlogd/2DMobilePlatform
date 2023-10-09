using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Weapon
{
    [SerializeField]
    private Bullet Bullet;
    [SerializeField]
    private Transform rightSpawn;
    [SerializeField]
    private Transform leftSpawn;
    [SerializeField]
    private float bulletSpeed;
    [SerializeField]
    private float weaponCooldown;

    private bool canShoot = true;

    public void Shoot(Vector2 direction)
    {
        if (!canShoot) return;

        if (direction == Vector2.right)
        {
            Bullet bullet = GameManager.Instantiate(Bullet, rightSpawn.position, Quaternion.identity);
            bullet.Rigidbody.velocity = direction * bulletSpeed;
        }
        else
        {
            Bullet bullet = GameManager.Instantiate(Bullet, leftSpawn.position, Quaternion.identity);
            bullet.Rigidbody.velocity = direction * bulletSpeed;
            bullet.SpriteRenderer.flipY = true;
        }

        GameManager.Instance.StartCoroutine(StartCooldown());
    }

    private IEnumerator StartCooldown()
    {
        canShoot = false;
        yield return new WaitForSeconds(weaponCooldown);
        canShoot = true;
    }
}
