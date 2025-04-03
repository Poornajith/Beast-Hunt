using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private int projectileCount = 30;
    [SerializeField] private int superProjectileCount = 5;

    [SerializeField] private float speed = 20.0f;
    [SerializeField] private float jumpForce = 700.0f;
    [SerializeField] private float attackCooldown = 0.5f;
    [SerializeField] private float groundDistance = 0.2f;

    [SerializeField] private Transform projectileSpawnPoint;
    [SerializeField] private Transform groundCheck;

    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private GameObject superProjectilePrefab;

    [SerializeField] private LayerMask groundMask;

    private bool isGrounded;
    private bool isPlayerFacingForward;

    private float horizontalInput;
    private float timeElapsedSinceLastAttack;

    private PowerUpPlayer powerUpPlayer;
    private Animator playerAnimator;

    private List<GameObject> projectiles;
    private List<GameObject> superProjectiles;

    public bool hasPowerUp;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isPlayerFacingForward = true;
        timeElapsedSinceLastAttack = Mathf.Infinity;

        playerAnimator = GetComponent<Animator>();
        powerUpPlayer = GetComponent<PowerUpPlayer>();

        projectiles = new List<GameObject>();
        for (int i = 0; i < projectileCount; i++)
        {
            GameObject projectile = Instantiate(projectilePrefab);
            projectile.SetActive(false);
            projectiles.Add(projectile);
        }
        
        superProjectiles = new List<GameObject>();
        for (int i = 0; i < superProjectileCount; i++)
        {
            GameObject projectile = Instantiate(superProjectilePrefab);
            projectile.SetActive(false);
            superProjectiles.Add(projectile);
        }
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        playerAnimator.SetBool("IsGround", isGrounded);

        horizontalInput = Input.GetAxis("Horizontal");
        timeElapsedSinceLastAttack += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
            playerAnimator.SetTrigger("Jump");
        }

        //play animations
        PlayAnimations();

        // Rotate the GameObject left and right
        if (horizontalInput < 0 && isPlayerFacingForward)
        {
            RotatePlayer();

        }
        else if (horizontalInput > 0 && !isPlayerFacingForward)
        {
            RotatePlayer();

        }

        if (!isPlayerFacingForward)
        {
            MovePlayerLeft();
        }
        else
        {
            MovePlayerRight();
        }

        // spawn projectile
        if (Input.GetKeyDown(KeyCode.Mouse0) && (timeElapsedSinceLastAttack >= attackCooldown))
        {
            Attack();
        }

    }

    private void Attack()
    {
        timeElapsedSinceLastAttack = 0f;
        playerAnimator.SetTrigger("Attack");

        if (hasPowerUp)
        {
            foreach (GameObject projectile in superProjectiles)
            {
                if (!projectile.activeInHierarchy)
                {
                    projectile.gameObject.SetActive(true);
                    projectile.transform.position = projectileSpawnPoint.position;
                    projectile.transform.rotation = projectileSpawnPoint.rotation;
                    break;
                }
            }
        }
        else
        {
            foreach (GameObject projectile in projectiles)
            {
                if (!projectile.activeInHierarchy)
                {
                    projectile.gameObject.SetActive(true);
                    projectile.transform.position = projectileSpawnPoint.position;
                    projectile.transform.rotation = transform.rotation;
                    break;
                }
            } 
        }
        
    }
    void RotatePlayer()
    {
        isPlayerFacingForward = !isPlayerFacingForward;
        transform.Rotate(Vector3.up, 180f);
    }

    void MovePlayerRight()
    {
        // Move the GameObject right (forward)
        transform.position += transform.forward * (horizontalInput * speed * Time.deltaTime);
        
    }
    void MovePlayerLeft()
    {
        // Move the GameObject left (backward)
        transform.position += transform.forward * (horizontalInput * speed * Time.deltaTime * -1);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
        {
            hasPowerUp = true;
            powerUpPlayer.enabled = true;
            other.gameObject.SetActive(false);
        }
    }

    void PlayAnimations()
    {
        if (horizontalInput != 0 && isGrounded)
        {
            playerAnimator.SetBool("IsRuning", true);
        }
        else
        {
            playerAnimator.SetBool("IsRuning", false);
        }

    }
}
