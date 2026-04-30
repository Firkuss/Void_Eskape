using UnityEngine;

public class SpeedManager : MonoBehaviour
{
    public float moveSpeed;
    public float moveSpeedP2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        moveSpeed = 0.03f;
        moveSpeedP2 = 0.02f;
    }

    // Update is called once per frame
    void Update()
    {
      
    }
}
