using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_controller : MonoBehaviour
{
    private Rigidbody rigitbody;
    public float speed = 10;
    private Animator animator;
    public float rotationSpeed = 10f;

    private  static float Attack_timer_Time = 1.5f; // удобная выноска для изменений
    private float Attack_timer = Attack_timer_Time; // независимое объявление не в цикле
    
    // Start is called before the first frame update
    void Start()
    {
        rigitbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");


        Vector3 directionVector = new Vector3(h, 0, v);
        if (directionVector != Vector3.zero)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(directionVector), Time.deltaTime * rotationSpeed);
        }

        animator.SetFloat("Speed", Vector3.ClampMagnitude(directionVector, 1).magnitude);

        if (animator.GetFloat("Speed") > 0f)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
        
        rigitbody.velocity = Vector3.ClampMagnitude(directionVector, 1) * speed; // Vector3.ClampMagnitude(vector, value) - позволяет задать максимальную длину вектора


        if (Input.GetKey(KeyCode.H) && animator.GetBool("isRunning") == false)
        {
            animator.SetBool("isAttacking", true);
        }

        if (animator.GetBool("isAttacking"))
        {
            Debug.Log(Attack_timer);
            Attack_timer -= Time.deltaTime;
            if (Attack_timer < 0f)
            {
                animator.SetBool("isAttacking", false);
                Attack_timer = Attack_timer_Time;
            }
        }

    }
}
