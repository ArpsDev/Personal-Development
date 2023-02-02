using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [Header("Character Controller")]
    public CharacterController controller;

    [Header("Movement")]
    public float WalkSpeed = 2f;
    public float RunSpeed = 4f;
    public float TurnSmoothTime = 0.1f;
    private float TurnSmoothVelocity;

    [Header("Animator")]
    public Animator ani;

    [Header("Camera")]
    public Transform cam;

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            ani.SetFloat("IsWalking", 2);

            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref TurnSmoothVelocity, TurnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * WalkSpeed * Time.deltaTime);
        }
        else
        {
            ani.SetFloat("IsWalking", 0);
        }
        
        if (direction.magnitude >= 0.1f & Input.GetKey(KeyCode.LeftShift)) 
        {
            ani.SetFloat("IsRunning", 3);

            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref TurnSmoothVelocity, TurnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * RunSpeed * Time.deltaTime);
        }
        else
        {
            ani.SetFloat("IsRunning", 0);
        }
    }
}
