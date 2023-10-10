using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponBase
{
    [SerializeField]
    protected BulletBase Bullet;
    [SerializeField]
    protected Transform rightSpawn;
    [SerializeField]
    protected Transform leftSpawn;
    [SerializeField]
    protected float bulletSpeed;
    [SerializeField]
    protected float weaponCooldown;

    protected bool canShoot = true;

    public virtual void Shoot(Vector2 direction)
    {
        if (!canShoot) return;

        if (direction == Vector2.right)
        {
            BulletBase bullet = GameManager.Instantiate(Bullet, rightSpawn.position, Quaternion.identity);
            bullet.Rigidbody.velocity = direction * bulletSpeed;
        }
        else
        {
            BulletBase bullet = GameManager.Instantiate(Bullet, leftSpawn.position, Quaternion.identity);
            bullet.Rigidbody.velocity = direction * bulletSpeed;
            bullet.SpriteRenderer.flipY = true;
        }

        GameManager.Instance.StartCoroutine(StartCooldown());
    }

    protected IEnumerator StartCooldown()
    {
        canShoot = false;
        yield return new WaitForSeconds(weaponCooldown);
        canShoot = true;
    }
}
