using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    public float thrust;
    public float knockTime;
    public float damage;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        // if items with tag breakable then use Pot script if by item with tag Player 
        if(other.gameObject.CompareTag("breakable") && this.gameObject.CompareTag("Player"))
        {
            other.GetComponent<Pot>().Smash();
        }
        if(other.gameObject.CompareTag("Enemies") || other.gameObject.CompareTag("Player"))
        {
            Rigidbody2D hit = other.GetComponent<Rigidbody2D>();
            if(hit != null)
            {    
                Vector2 difference = hit.transform.position - transform.position;
                difference = difference.normalized * thrust; 
                hit.AddForce(difference, ForceMode2D.Impulse);

                // && is need as Log contains 2 colliders, should have been set to different sub object
                if(other.gameObject.CompareTag("Enemies") && other.isTrigger)
                {
                    hit.GetComponent<Enemy>().currentState = EnemyState.stagger;                  
                    other.GetComponent<Enemy>().Knock(hit, knockTime, damage);
                } 
                if(other.gameObject.CompareTag("Player"))
                {
                    if(other.GetComponent<PlayerMovement>().currentState != playerState.stagger)
                    {
                        hit.GetComponent<PlayerMovement>().currentState = playerState.stagger;                  
                        other.GetComponent<PlayerMovement>().Knock(knockTime, damage);
                    }
                                    } 
                
            }
        }
    }
}
