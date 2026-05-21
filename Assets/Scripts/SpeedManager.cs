using UnityEngine;

public class SpeedManager : MonoBehaviour
{
    public float moveSpeed;
    public float moveSpeedP2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        moveSpeed = 0.06f;
        moveSpeedP2 = 0.04f;
    }

    // Update is called once per frame
    void Update()
    {
      
    }
}
