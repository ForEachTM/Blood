using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : MonoBehaviour
{

    public GameObject smokePrefab;

    SpriteRenderer spriteRenderer;

    new Rigidbody2D rigidbody2D;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Destroy(gameObject, 2f);
    }

    public void ExplosionKnockback(float knockback, Vector3 position)
    {
        Vector2 moveDirection = transform.position - position;

        rigidbody2D.AddForce(new Vector2(moveDirection.normalized.x * knockback, knockback), ForceMode2D.Impulse);
        rigidbody2D.AddTorque(knockback * 30f);
    }

    public void Flip(float sign)
    {
        Vector3 scale = transform.localScale;
        scale.x = sign;
        transform.localScale = scale;
    }

    public void SetSprite(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }

    public void AddSmoke()
    {
        GameObject smoke = Instantiate(smokePrefab, transform.position, Quaternion.identity);
        smoke.gameObject.transform.SetParent(gameObject.transform);
    }
}
