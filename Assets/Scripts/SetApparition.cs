using Unity.Multiplayer.Center.Common;
using UnityEngine;
using System.Collections.Generic;

public class SetApparition : MonoBehaviour
{
    public GameObject spawnSet;
    public GameObject groundSet;
    public List<GameObject> presetlist;
    public GameObject SpeedManager;
    public Transform parentPlane;
    
    private SpeedManager speedM;
    private GameObject presetChild;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        speedM = FindFirstObjectByType<SpeedManager>();
        ChildInstantiate();

        presetChild.transform.position = spawnSet.transform.position;
        if (spawnSet.tag == "Plane2")
        {
            presetChild.transform.position = presetChild.transform.position - new Vector3(0, 10, 0);
        }


    }

    public void OnTriggerEnter2D(Collider2D collision)
    {       

        Destroy(collision.gameObject);
        
        ChildInstantiate();

        presetChild.transform.position = spawnSet.transform.position;

        if (parentPlane.tag == "Plane2")
        {
            presetChild.transform.position = presetChild.transform.position - new Vector3(0, 10, 0);
            speedM.moveSpeedP2 += 0.000095f;
        }
        else 
        {
            speedM.moveSpeed += 0.0001f;
        }

    }

    public void ChildInstantiate()
    {
        presetChild = Instantiate(groundSet, parentPlane);
        presetChild.name = "ChildPreset";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
