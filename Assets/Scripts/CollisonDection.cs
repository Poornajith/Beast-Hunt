using UnityEngine;

public class CollisonDection : MonoBehaviour
{
    //[SerializeField] private LayerMask playerLayerMask;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) 
        {
            //Destroy(other.gameObject);
            Debug.Log("Player hit");
        }
        //Destroy(gameObject);
    }
}
