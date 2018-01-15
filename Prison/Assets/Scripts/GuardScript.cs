using UnityEngine;
using UnityEngine.AI;

/*

    Guard is at the gate. If a player meets him, few things need to be checked:
    
    Is the player disguised ?
    --> Yes : 
           Lets him pass the gate
           If it's the second time the player comes by -meaning he's going back to the bedrooms- 
           goes to the final door if he knows its position, or keeps walking
           If it's only the first time, goes to the key positions
                    
    --> No :
        If he knows the position of the Box containing disguise, goes there.
        Otherwise, only remmebers position of the gate.

*/

public class GuardScript : MonoBehaviour {

    private bool ismoved;
    private NavMeshAgent agent;

    private Vector3 pos1, pos2; 

    private Animation anim;
    private bool isplaying = false;

    public Texture no_guard;

    void Start()
    {
        ismoved = false;
        pos1 = this.transform.position;
        pos2 = pos1 + Vector3.forward * 3;
        anim = GetComponent<Animation>();
        agent = GetComponent<NavMeshAgent>();
        anim.Play("Idle");
    }

    void FixedUpdate()
    {
        if(this.transform.position.x == pos1.x && this.transform.position.z == pos1.z)
        {
            this.transform.LookAt(GameObject.Find("Target9").transform);
        }

        if (this.transform.position == agent.destination && isplaying)
        {
            anim.Stop("Walking");
            isplaying = false;
            anim.Play("Idle");
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            PlayerClass player = col.gameObject.GetComponent<PlayerClass>();

            if (player.isDressed)  
            {
                if (!ismoved)
                {
                    moveGuard();
                    ismoved = true;
                }

                if (player.passedSecurity)
                {
                    if (player.hasKey && player.Target_FinalDoor)
                    {
                        player.changeTargetPriority(player.Target_FinalDoor);
                        player.changeDest();
                    }
                    else
                    {   player.keepWalking();  }
                }
                else
                { 
                    // adding the two new targets to the list (possible positions of the key)
                    player.TargetsList.Insert(0, GameObject.Find("Target8"));
                    player.TargetsList.Insert(0, GameObject.Find("Target7"));

                    player.passedSecurity = true;
                    player.changeDest();
                }
            }

            if (!player.isDressed)
            {
                player.displayImageAbovePlayer(no_guard);
                player.Target_Gate = player.TargetsList[0];
                
                if (player.Target_Box)
                {
                    player.changeTargetPriority(player.Target_Box);
                    player.changeDest();
                }
                else
                {   player.keepWalking(); }
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            PlayerClass player = col.gameObject.GetComponent<PlayerClass>();
            player.removeImageAbovePlayer();
            if (ismoved)
            {
                agent = GetComponent<NavMeshAgent>();
                agent.destination = pos1;
                ismoved = false;
            }
        }
    }

    public void moveGuard()
    {
        agent.destination = pos2;
        if (!isplaying)
        {
            anim.Stop("Idle");
            anim.Play("Walking");
            isplaying = true; 
        }
    }
    
}
