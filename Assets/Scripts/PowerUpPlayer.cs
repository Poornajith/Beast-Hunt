using System.Collections;
using UnityEngine;

public class PowerUpPlayer : MonoBehaviour
{
    [SerializeField] private float powerUpActiveTime = 5.0f;

    private PlayerController playerController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        StartCoroutine(PowerUpCountdownRoutine());
    }

    IEnumerator PowerUpCountdownRoutine()
    {
        yield return new WaitForSeconds(powerUpActiveTime);
        playerController.hasPowerUp = false;
        this.enabled = false;
    }
}
