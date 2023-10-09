using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public SpriteRenderer SpriteRenderer;
    public Rigidbody2D Rigidbody;

    private void Awake() => Rigidbody = GetComponent<Rigidbody2D>();
}
