using UnityEngine;

public class SetApparition : MonoBehaviour
{
    public GameObject spawnSet;
    public GameObject groundSet;
    public GameObject SpeedManager;
    public SpeedManager speedM;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        speedM = FindFirstObjectByType<SpeedManager>();
        Instantiate(groundSet);
        groundSet.transform.position = spawnSet.transform.position;
        if (spawnSet.tag == "Plane2")
        {
            groundSet.transform.position = groundSet.transform.position - new Vector3(0, 10, 0);
        }


    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        print ("I'm in");

        Destroy(collision.gameObject);
        Instantiate(groundSet);
        groundSet.transform.position = spawnSet.transform.position;

        if (spawnSet.tag == "Plane2")
        {
            groundSet.transform.position = groundSet.transform.position - new Vector3(0, 10, 0);
            speedM.moveSpeedP2 += 0.000095f;
        }
        else 
        {
            speedM.moveSpeed += 0.0001f;
        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
