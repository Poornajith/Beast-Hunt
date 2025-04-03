using UnityEngine;

public class DestroySelf : MonoBehaviour
{
    [SerializeField] private float destroyLocationX;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x < destroyLocationX)
        {
            gameObject.SetActive(false);
        }
    }
}
