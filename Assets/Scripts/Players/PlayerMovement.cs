using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum PlayerState
{
    walk,
    run,
    attack,
    interact,
    stagger,
    idle
}

public class PlayerMovement : MonoBehaviour
{
    public PlayerState currentState;
    public float walkSpeed;
    public float runSpeed;
    private Rigidbody2D myRigidBody;
    private Vector3 change;
    private Animator animator;
    public FloatValue currentHealth;
    public ObjectSignal playerHealthSignal;
    public GameObject projectile;
    public InventoryItem arrow;
    public PlayerInventory playerInventory;
    public TextMeshProUGUI arrowAmount;

    void Start()
    {
        arrowAmount.text = playerInventory.myInventory.Find(x => x.name == "Arrow").numberHeld + "";
        animator = GetComponent<Animator>();
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        arrowAmount.text = playerInventory.myInventory.Find(x => x.name == "Arrow").numberHeld + "";
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");
        if (Input.GetButtonDown("attack") && currentState != PlayerState.attack && currentState != PlayerState.stagger)
        {
            SoundManager.PlaySound("Sword");
            StartCoroutine(AttackCo());
        }
        else if (Input.GetButtonDown("Second Weapon") && currentState != PlayerState.attack && currentState != PlayerState.stagger)
        {
            StartCoroutine(SecondAttackCo());
        }
        else if ((currentState == PlayerState.walk || currentState == PlayerState.run) && Input.GetButton("run"))
        {
            MoveCharacter(runSpeed);
            currentState = PlayerState.run;
            animator.SetBool("running", true);
            animator.SetFloat("moveX", change.x);
            animator.SetFloat("moveY", change.y);
        }

        else if (currentState == PlayerState.walk || currentState == PlayerState.run || currentState == PlayerState.idle)
        {
            animator.SetBool("running", false);
            UpdateAnimationAndMove();
        }
    }

    private IEnumerator AttackCo()
    {
        animator.SetBool("attacking", true);
        currentState = PlayerState.attack;
        yield return null;
        animator.SetBool("attacking", false);
        yield return new WaitForSeconds(.3f);
        currentState = PlayerState.idle;
    }
    
    private IEnumerator SecondAttackCo()
    {
        if (arrow.numberHeld > 0)
        {
            currentState = PlayerState.attack;
            yield return null;
            MakeArrow();
            arrow.DecreaseAmount(1);
            yield return new WaitForSeconds(.3f);
            currentState = PlayerState.idle;
        }
    }

    private void MakeArrow()
    {
        Vector2 temp = new Vector2(animator.GetFloat("moveX"), animator.GetFloat("moveY"));
        SoundManager.PlaySound("Bow");
        Arrow arrow = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Arrow>();
        arrow.Setup(temp, ChooseArrowDirection());
    }

    Vector3 ChooseArrowDirection()
    {
        float temp = Mathf.Atan2(animator.GetFloat("moveY"), animator.GetFloat("moveX")) * Mathf.Rad2Deg;
        return new Vector3(0, 0, temp);
    }

    void UpdateAnimationAndMove()
    {
        if (change != Vector3.zero)
        {
            MoveCharacter(walkSpeed);
            animator.SetBool("moving", true);
            currentState = PlayerState.walk;
            animator.SetFloat("moveX", change.x);
            animator.SetFloat("moveY", change.y);
        }
        else
        {
            animator.SetBool("moving", false);
            currentState = PlayerState.idle;
        }
    }

    void MoveCharacter(float speed)
    {
        change.Normalize();
        myRigidBody.MovePosition(transform.position + Time.deltaTime * speed * change);
    }

    public void Knock(float knockTime, float damage)
    {
        SoundManager.PlaySound("Hurt");
        currentHealth.RunTimeValue -= damage;
        playerHealthSignal.Raise();
        if (currentHealth.RunTimeValue > 0)
        {
            
            StartCoroutine(KnockCo(knockTime)); 
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }
    
    private IEnumerator KnockCo(float knockTime)
    {
        if (myRigidBody != null)
        {
            yield return new WaitForSeconds(knockTime);
            myRigidBody.velocity = Vector2.zero;
            currentState = PlayerState.idle;
            myRigidBody.velocity = Vector2.zero;
        }
    }
}
