using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] float movementSpeed = 4f;
    [SerializeField] float followRadius = 20f;
    [SerializeField] float blastRadius = 1.5f;
    [SerializeField] float blastPower = 10f;

    private void Update()
    {
        Vector2 thisPos = new Vector2(transform.position.x, transform.position.y);
        Vector2 playerPos = new Vector2(player.transform.position.x, player.transform.position.y);
        float distance = (playerPos - thisPos).magnitude;
        if (distance > followRadius) return;
        RaycastHit2D[] hits;
        hits = Physics2D.LinecastAll(thisPos, playerPos);

        if(hits[0].collider != null && hits[0].collider.tag == "Player")
        {
            MoveTowards(playerPos);
        }

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, blastRadius);
        foreach (Collider2D collider in colliders)
        {
            if(collider.tag == "Player")
            {
                Rigidbody2D rb = collider.GetComponent<Rigidbody2D>();
                if (rb == null) continue;
                Vector2 otherPos = new Vector2(rb.gameObject.transform.position.x, rb.gameObject.transform.position.y);
                Vector2 dir = (otherPos - thisPos).normalized;
                rb.AddForce(dir * blastPower, ForceMode2D.Impulse);
            }
        }
    }

    void MoveTowards(Vector2 position)
    {
        Vector2 thisPos = new Vector2(transform.position.x, transform.position.y);
        Vector2 dir = (position - thisPos).normalized;
        transform.Translate(dir * movementSpeed * Time.deltaTime, Space.World);
    }    
}
