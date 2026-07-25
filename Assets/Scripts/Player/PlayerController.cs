using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;

public class PlayerController : MonoBehaviour//,IDamageable
{
    public Animator animator;
    public CharacterController characterController;
    public float speed = 10f;
    public float turnSpeed = 1f;
    public float gravity = 9.8f;
    public float jumpSpeed = 5f;

    private float vSpeed = 0f;


    public KeyCode jumpKey = KeyCode.Space;

    [Header("Run Setup")]
    public KeyCode keyRun = KeyCode.LeftShift;
    public float speedRun = 1.5f;

    [Header("Flash Damage")]
    public List<FlashColor> flashColors;

    public HealthBase healthBase;

    private void OnValidate()
    {
        if (healthBase == null)
        {
           healthBase = GetComponent<HealthBase>();
        }
    }

    private void Awake()
    {
        OnValidate();
        healthBase.OnDamaged += Damage;
    }

    void Update()
    {
        transform.Rotate(0, Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime, 0);

        var inputAxisVertical = Input.GetAxis("Vertical");
        var speedVector = transform.forward * inputAxisVertical * speed;

        if (characterController.isGrounded)
        {
            vSpeed = 0;
            if (Input.GetKeyDown(jumpKey))
            { 
                vSpeed = jumpSpeed;
            }
        }

        vSpeed -= gravity * Time.deltaTime;
        speedVector.y = vSpeed;

        var isWalking = inputAxisVertical != 0;
        if (isWalking)
        {
            if (Input.GetKey(keyRun))
            {
                speedVector *= speedRun;
                animator.speed = speedRun;
            }
            else
            {
                animator.speed = 1;
            }
        }

        characterController.Move(speedVector * Time.deltaTime);

        if (inputAxisVertical != 0)
        {
            animator.SetBool("Run", true);
        }
        else
        {
            animator.SetBool("Run", false);
        }
    }


    #region HEALTH
    public void Damage(HealthBase h)
    {
       flashColors.ForEach(flashColor => flashColor.Flash());
    }

    public void Damage(float damage)
    {
        healthBase.Damage(damage);

    }
    #endregion
}

