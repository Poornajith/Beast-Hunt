using UnityEngine;

public class BgRepeat : MonoBehaviour
{
    [SerializeField] private float speed = -40.0f;
    [SerializeField] private float RepeatX = -40.0f;

    private float startX;
    void Start()
    {
        startX = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        // Move the GameObject forward
        transform.position += transform.right * speed * Time.deltaTime;
        if (transform.position.x < RepeatX)
        {
            Vector3 newPos = transform.position;
            newPos.x = startX;
            transform.position = newPos;
        }
    }
}
