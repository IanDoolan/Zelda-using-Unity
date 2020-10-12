using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum playerState 
{
    walk,
    attack,
    interact,
    stagger,
    idle
}


public class PlayerMovement : MonoBehaviour
{
    //variables
    public playerState currentState;
    public float speed;
    private Rigidbody2D myRigidbody;
    private Vector3 change;
    private Animator animator;
    public FloatValue currentHealth;
    public Signal playerHealthSignal;


    // Start is called before the first frame update
    void Start()
    {
        currentState = playerState.walk;
        animator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>(); 
        animator.SetFloat("moveX", 0);
        animator.SetFloat("moveY", -1);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");
        if(currentState == playerState.walk || currentState == playerState.idle) 
        {
            UpdateAnimatonAndMove();
        }
    }

    void Update()
    {
        if(Input.GetButtonDown("attack") && currentState != playerState.attack
            && currentState != playerState.stagger)
        {
            StartCoroutine(AttackCo());
        }
    }


    private IEnumerator AttackCo() 
    {
        animator.SetBool("attacking", true);
        currentState = playerState.attack;
        yield return null;
        animator.SetBool("attacking", false);
        yield return new WaitForSeconds(.3f);
        currentState = playerState.walk;
    }


    void UpdateAnimatonAndMove()
    {
       if(change != Vector3.zero)
        {
            MoveCharacter();
            animator.SetFloat("moveX", change.x);
            animator.SetFloat("moveY", change.y);
            animator.SetBool("moving", true);
        }else{
            animator.SetBool("moving", false);
        } 
    }


     // Update is called once per frame
    void MoveCharacter()
    {
        change.Normalize();
        myRigidbody.MovePosition(
            transform.position + change * speed * Time.deltaTime
        );    
    }

    public void Knock(float knockTime, float damage)
    {
        //Debug.Log("Hit");
        //currentHealth.initialValue -= damage;
        currentHealth.RuntimeValue -= damage;
        playerHealthSignal.Raise();
        
        //if(currentHealth.initialValue > 0)
        if(currentHealth.RuntimeValue > 0)
        {
            StartCoroutine(KnockCo(knockTime));
        } else
        {
            this.gameObject.SetActive(false); 
        }
        
    }

    private IEnumerator KnockCo(float knockTime)
    {
        if(myRigidbody != null)
        {
            yield return new WaitForSeconds(knockTime);
            myRigidbody.velocity = Vector2.zero;   
            currentState = playerState.idle;
            myRigidbody.velocity = Vector2.zero;
        }
    }
}
