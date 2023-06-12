using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlast : MonoBehaviour
{
    [SerializeField] float rechargeTime = 1f;
    [SerializeField] float blastRadius = 6f;
    [SerializeField] float blastPower = 10f;

    bool canBlast = true;
    void Update()
    {
        if(canBlast)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                canBlast = false;
                StartCoroutine(Recharge());
                Blast();
            }
        }
    }

    IEnumerator Recharge()
    {
        yield return new WaitForSeconds(rechargeTime);
        canBlast = true;
    }
    void Blast()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, blastRadius);
        Vector2 thisPos = new Vector2(transform.position.x, transform.position.y);
        foreach (Collider2D collider in colliders)
        {
            Rigidbody2D rb = collider.GetComponent<Rigidbody2D>();
            if (rb == null) continue;
            Vector2 otherPos = new Vector2(rb.gameObject.transform.position.x, rb.gameObject.transform.position.y);
            Vector2 dir = (otherPos - thisPos).normalized;
            rb.AddForce(dir * blastPower, ForceMode2D.Impulse);
        }
    }
}
