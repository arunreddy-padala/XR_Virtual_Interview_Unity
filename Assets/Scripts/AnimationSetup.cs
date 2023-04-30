using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSetup : MonoBehaviour
{

    // get the handshake component from the game object

    public GameObject Avatar;

    private StandUpAndWalkToSubject standUpAndWalkToSubject;

    // Start is called before the first frame update
    void Start()
    {
  
      // set the avatar to be sitting idle
      Avatar.GetComponent<Animator>().SetInteger("animeState", 1);
      standUpAndWalkToSubject = Avatar.GetComponent<StandUpAndWalkToSubject>();

      // wait for 2 seconds before calling the standupandwalktosubject function
      Invoke("CallStandUpAndWalkToSubject", 2f);

   }

    void CallStandUpAndWalkToSubject()
    {
        standUpAndWalkToSubject.StandUpAndWalkToSubjectFunction();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
