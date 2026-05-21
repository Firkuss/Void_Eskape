using Unity.Multiplayer.Center.Common;
using UnityEngine;
using System.Collections.Generic;

public class SetApparition : MonoBehaviour
{
    public GameObject spawnSet;
    public List<GameObject> presetlvl1;
    public List<GameObject> presetlvl2;
    public List<GameObject> presetlvl3;
    public GameObject SpeedManager;
    public Transform parentPlane;
    
    private SpeedManager speedM;
    private GameObject presetChild;
    private int Lvl1Proba = 50;
    private int Lvl2Proba = 35;
    private int LvlNextProba = 70;
    private bool SpawnPhase;
    private bool canSpawn;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        SpawnPhase = false;
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
            speedM.moveSpeedP2 += 0.00000090f;
        }
        else 
        {
            speedM.moveSpeed += 0.000001f;
        }

    }

    public void ChildInstantiate()
    {
        canSpawn = true;
        int randomindexLvl1 = Random.Range(0, presetlvl1.Count);
        int randomindexLvl2 = Random.Range(0, presetlvl2.Count);
        int randomindexLvl3 = Random.Range(0, presetlvl3.Count);

        int randomValue = Random.Range(0, 100);

        if (randomValue >= Lvl1Proba && SpawnPhase == false && canSpawn == true)
        {
            presetChild = Instantiate(presetlvl1[randomindexLvl1], parentPlane);
            presetChild.name = "ChildPresetLvl1";
            SpawnPhase = false;
            canSpawn = false;
        }

        if (randomValue < Lvl2Proba && SpawnPhase == false && canSpawn == true)
        {
            presetChild = Instantiate(presetlvl2[randomindexLvl2], parentPlane);
            presetChild.name = "ChildPresetLvl2";
            SpawnPhase = true;
            canSpawn = false;
        }

        if (randomValue >= Lvl2Proba && randomValue < Lvl1Proba && SpawnPhase == false && canSpawn == true)
        {
            presetChild = Instantiate(presetlvl3[randomindexLvl3], parentPlane);
            presetChild.name = "ChildPresetLvl3";
            SpawnPhase = true;
            canSpawn = false;
        }

        if (randomValue < LvlNextProba && SpawnPhase == true && canSpawn == true)
        {
            presetChild = Instantiate(presetlvl1[randomindexLvl1], parentPlane);
            presetChild.name = "ChildPresetLvl1";
            SpawnPhase = false;
            canSpawn = false;
        }

        if (randomValue >= LvlNextProba && SpawnPhase == true && canSpawn == true)
        {
            presetChild = Instantiate(presetlvl2[randomindexLvl2], parentPlane);
            presetChild.name = "ChildPresetLvl2";
            SpawnPhase = true;
            canSpawn = false;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
