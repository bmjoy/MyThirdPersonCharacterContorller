using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(NavMeshAgent))]

public class CharacterController : MonoBehaviour
{
    private Animator animator;
    private NavMeshAgent navMeshAgent;

    private Joystick joystick;

    [SerializeField]
    private float moveSpeed = 4.0f;

    private float vertical = 0f;
    private float horizontal = 0f;

    private Vector3 forward;
    private Vector3 right;

    // Start is called before the first frame update
    void Start()
    {
        this.animator = this.GetComponent<Animator>();
        this.navMeshAgent = this.GetComponent<NavMeshAgent>();

        this.joystick = GameObject.FindWithTag("Joystick").GetComponent<Joystick>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateInput();
        UpdateMove();
    }

    void UpdateInput()
    {
        vertical = joystick.Vertical;
        horizontal = joystick.Horizontal;
        if(0 == vertical && 0 == horizontal)
        {
            vertical = Input.GetAxis("Vertical");
            horizontal = Input.GetAxis("Horizontal");
        }
    }

    void UpdateMove()
    {
        forward = this.transform.forward;
        forward.y = 0;
        forward = forward.normalized;

        right = this.transform.right;
        right.y = 0;
        right = right.normalized;

        Vector3 direction = forward * vertical + right* horizontal;

        navMeshAgent.velocity = direction.normalized * moveSpeed;
        Debug.Log("Speed = " + navMeshAgent.velocity.magnitude);
        animator.SetFloat("MoveSpeed", navMeshAgent.velocity.magnitude);
    }
}
