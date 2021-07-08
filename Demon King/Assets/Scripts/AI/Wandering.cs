using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Wandering : MonoBehaviour
{
    private NavMeshAgent agent;
    Vector3 location;
    private float distanceToDestination = 0;
    private Vector3 previousDestination;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    void Update()
    {
        distanceToDestination = Vector3.Distance(gameObject.transform.localPosition, location);

        if (location == Vector3.zero || previousDestination == location)
        {
            location = RandomNavSphere(gameObject.transform.localPosition, 10, -1);
            agent.SetDestination(location);
        }

        if (distanceToDestination <= 1)
        {
            location = RandomNavSphere(gameObject.transform.localPosition, 10, -1);
            agent.SetDestination(location);
        }
        if (Physics.Raycast(gameObject.transform.localPosition, gameObject.transform.TransformDirection(Vector3.forward), out RaycastHit hit,10))
        {
            if (hit.collider.tag == "Obstruction")
            {
                location = RandomNavSphere(gameObject.transform.localPosition, 10, -1);
                agent.SetDestination(location);
            }
        }

        if (agent.velocity == Vector3.zero)
        {
            location = RandomNavSphere(gameObject.transform.localPosition, 10, -1);
            agent.SetDestination(location);
        }
        
    }
    
    public static Vector3 RandomNavSphere(Vector3 origin, float distance, int layermask)
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * distance;

        randomDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randomDirection, out navHit, distance, layermask);

        return navHit.position;
    }
}
