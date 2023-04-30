using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandUpAndWalkToSubject : MonoBehaviour
{

    public Transform movePositionTransformShakeHandPosition;

    private UnityEngine.AI.NavMeshAgent navMeshAgent;

    private Animator animator;

    public GameObject HandShakeTriggerButton;


    // 0 = stand idle
    // 1 = sitting idle
    // 2 = walking 
    // 3 = shake hands
    // 4 = stand up
    // 5 = turn right
    // 6 = turn 180


    // Start is called before the first frame update
    void Start()
    {

        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        animator = GetComponent<Animator>();

    }


    public void StandUpAndWalkToSubjectFunction()
    {
        StartCoroutine(StandUp());
    }

    IEnumerator StandUp()
    {
        // check if the player is sitting on the chair
        if (animator.GetInteger("animeState") == 1)
        {
            yield return new WaitForSeconds(2f);
            // if sitting in chair then stand up
            animator.SetInteger("animeState", 4);
        }

        // // stand up
        // animator.SetInteger("animeState", 4);


        yield return new WaitForSeconds(3f); // wait 2 seconds and move to position
        StartCoroutine(MoveToShakeHandPosition());
    }


    IEnumerator MoveToShakeHandPosition()
    {
        animator.SetInteger("animeState", 2);
        // move to the position

        navMeshAgent.SetDestination(movePositionTransformShakeHandPosition.position);
        // play the walking animation

        // wait a short time for the destination to be set
        yield return new WaitForSeconds(0.1f);
        while (navMeshAgent.remainingDistance > navMeshAgent.stoppingDistance)
        {
            yield return null; // wait for the agent to reach the destination
        }

        // rotate the avatar to face the hand shake position
        transform.rotation = movePositionTransformShakeHandPosition.rotation;

        // set the animation to stand idle
        animator.SetInteger("animeState", 0);

        // turn on the TriggerHandShake GameObject
        HandShakeTriggerButton.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
