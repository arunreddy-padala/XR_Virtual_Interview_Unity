using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShakeHandAndSitInChair : MonoBehaviour
{
    private Animator animator;

    [SerializeField] private Transform movePositionTransformChair;

    private NavMeshAgent navMeshAgent;

    // Start is called before the first frame update
    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        // start standing idle
        animator.SetInteger("animeState", 0);

    }


    // call shakehand animation
    public void CallShakeHand()
    {

        GetComponent<StandUpAndWalkToSubject>().enabled = false;
        StartCoroutine(ShakeHands());
    }

    // shake hands then trigger movetochairsequence
    IEnumerator ShakeHands()
    {

        animator.SetInteger("animeState", 3);

        yield return new WaitForSeconds(3f); // wait 2 seconds and shake hands
        StartCoroutine(MovetoChairSequence());
    }

    IEnumerator MovetoChairSequence()
    {


        Debug.Log("move to chair sequence" + movePositionTransformChair.position);

        // Debug.Log("move to chair sequence position",movePositionTransformChair.position);
        navMeshAgent.SetDestination(movePositionTransformChair.position); // use SetDestination instead of directly setting the destination
        Debug.Log("Current destination: " + navMeshAgent.destination);

        // check the navMeshAgent setDestination position x value, y value, z value

        animator.SetInteger("animeState", 2);
        yield return new WaitForSeconds(0.1f); // wait a short time for the destination to be set

        while (navMeshAgent.remainingDistance > navMeshAgent.stoppingDistance)
        {
            yield return null; // wait for the agent to reach the destination
        }

        navMeshAgent.isStopped = true; // stop the player
        animator.SetInteger("animeState", 0);

        // rotate the player to match the chair
        transform.rotation = movePositionTransformChair.rotation;
        animator.SetInteger("animeState", 1); // sit on the chair
    }

}
