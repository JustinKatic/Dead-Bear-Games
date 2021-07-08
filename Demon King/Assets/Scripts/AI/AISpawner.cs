using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISpawner : MonoBehaviour
{
    [SerializeField] private GameObject agent;
    [SerializeField] private int spawnLimit = 5;
    [SerializeField] private List<Transform> spawnPositions = new List<Transform>();
    
    private List<Vector3> spawnLocations;
    private int numberOfLocations;

    
    
    // Start is called before the first frame update
    void Start()
    {
        spawnLocations = new List<Vector3>();
        foreach (var position in spawnPositions)
        {
            spawnLocations.Add(position.position);
        }

        numberOfLocations = spawnLocations.Count;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private Vector3 RandomSpawnLocation()
    {
        Vector3 randomLocation = Vector3.zero;
        int position = 0;

        position = Random.Range(0, numberOfLocations - 1);

        randomLocation = spawnLocations[position];
    
        return randomLocation;

    }

    private void SpawnNewAI()
    {
        Instantiate(agent, RandomSpawnLocation(), Quaternion.identity);
        
    }

    private int AgentCount()
    {
        int count = 0;
        
        

        return count;
    }
}
