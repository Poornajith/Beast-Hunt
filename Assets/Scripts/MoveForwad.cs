using UnityEngine;

public class MoveForwad : MonoBehaviour
{
    [SerializeField] private float speed = 40.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         // Move the GameObject forward
        transform.position += transform.forward * speed * Time.deltaTime;
    }

}

