using UnityEngine;

/*
    Opening/Closing of double doors 
*/

public class DoubleDoor : MonoBehaviour
{
    public Animator anim;
    private int nb = 0;
    private bool isclosed = true;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            anim.Play("DoorAnimation");
            nb++;
            isclosed = false;
        }
    }

    void OnTriggerExit(Collider col)
    // closes only when all of the players are out of the trigger zone
    {
        if (col.gameObject.tag == "Player")
        {
            nb--;
        }
        if ((nb == 0) && !isclosed)
        {
            anim.Play("DoorAnimationClosed");
            isclosed = true;
        }
    }

}