using UnityEngine;

public class SetBehaviour : MonoBehaviour
{
    [Header("Movement Settings")]
    [Tooltip("Horizontal movement speed.")]
    public SpeedManager speedM;
    public GameObject spawnSet;
    public float sm; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        speedM = FindFirstObjectByType<SpeedManager>();

        
    }

    // Update is called once per frame
    void Update()
    {
        var addSpeed = new Vector3(speedM.moveSpeed, 0f, 0f);
        var addSpeedP2 = new Vector3(speedM.moveSpeedP2, 0f, 0f);

        if (gameObject.tag == "Plane2")
        {
            transform.position = transform.position - addSpeedP2;
        }
        else {
            transform.position = transform.position - addSpeed;
        }
           
    }
}

