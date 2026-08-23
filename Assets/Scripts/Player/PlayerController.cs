using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using EDGEE.Core.Singleton;

public class PlayerController : Singleton<PlayerController>//,IDamageable
{
    public List<Collider> colliders;

    public Animator animator;
    public CharacterController characterController;
    public float speed = 10f;
    public float turnSpeed = 1f;
    public float gravity = 9.8f;
    public float jumpSpeed = 5f;

    private float vSpeed = 0f;
    
    [SerializeField] private bool shakeCameraOnShoot = false;

    public KeyCode jumpKey = KeyCode.Space;

    [Header("Run Setup")]
    public KeyCode keyRun = KeyCode.LeftShift;
    public float speedRun = 1.5f;

    [Header("Flash Damage")]
    public List<FlashColor> flashColors;

    [Header("Life")]
    public HealthBase healthBase;

    private bool _alive = true;

    private void OnValidate()
    {
        if (healthBase == null)
        {
            healthBase = GetComponent<HealthBase>();
        }
    }

    protected override void Awake()
    {
        base.Awake();
        // deixe Awake apenas para inicializações que precisam rodar antes do Start
        OnValidate();
    }

    private void Start()
    {
        // registra eventos em Start para garantir que outros Awake() (ex: EffectsManager) já rodaram
        if (healthBase == null)
        { 
            return;
        }

        healthBase.OnDamaged += Damage;
        healthBase.OnKilled += OnKill;
    }

    private void OnDestroy()
    {
        if (healthBase != null)
        {
            healthBase.OnDamaged -= Damage;
            healthBase.OnKilled -= OnKill;
        }
    }


    private void OnKill(HealthBase h)
    {
        if (_alive)
        {
            _alive = false;
            animator.SetTrigger("Death");
            colliders.ForEach(i => i.enabled = false);
            Invoke(nameof(Revive), 3f);
        }
    }

    private void Revive()
    {
       _alive = true;
       healthBase.ResetLife();
       animator.SetTrigger("Revive");
       colliders.ForEach(i => i.enabled = true);
       Respawn();    
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

    public void Respawn()
    {
        if (CheckPointManager.Instance.HasCheckPoint())
        {
            Vector3 checkpointPos = CheckPointManager.Instance.GetPositionFromLastCheckPoint();

            characterController.enabled = false;
            transform.position = checkpointPos;
            characterController.enabled = true;
        }
    }

    #region HEALTH
    public void Damage(HealthBase h)
    {
        // segurança adicional: flashColors pode ser nulo ou conter nulos
        if (flashColors != null)
        {
            for (int i = 0; i < flashColors.Count; i++)
            {
                var fc = flashColors[i];
                if (fc != null) fc.Flash();
                if (shakeCameraOnShoot)
                {
                    CameraShaker.Instance.Shake();
                }
            }
        }

        if (EffectsManager.Instance != null)
            EffectsManager.Instance.ChangeVignette();
        else
            Debug.LogWarning("EffectsManager não inicializado ao chamar ChangeVignette()", this);
    }


    public void Damage(float damage)
    {
        healthBase?.Damage(damage);

    }
    #endregion
}

