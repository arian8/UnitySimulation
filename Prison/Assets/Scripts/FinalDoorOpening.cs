using UnityEngine;

/*
   Final door script. 
   If already opened, players target list is emptied (he stops moving)
   If not, we check if player has key (then opens). If not, he remembers position of the final door and keep walking.

*/

public class FinalDoorOpening : MonoBehaviour {

	private Animation anim;
	private bool trigger;
    public Texture no_key;

    void Start () 
	{
        trigger = false;
		anim = GetComponent<Animation> ();	
	}
	void OnTriggerEnter(Collider col) 
	{
        PlayerClass player = col.gameObject.GetComponent<PlayerClass>();
       
        if (col.gameObject.tag == "Player") 
			{
            if (!trigger)
            {
                if (player.hasKey)
                {
                    anim.Play("FinalDoorOpening");
                    trigger = true;
                    player.changeTargetPriority(GameObject.Find("Target10"));
                    player.changeDest();
                }
                else
                {
                    // if has no key : picture appears and players remember position of final door
                    player.displayImageAbovePlayer(no_key);
                    player.Target_FinalDoor = GameObject.Find("Target9"); 

                    player.keepWalking();
                }
			}
            else
            {
                // player escapes
				player.changeTargetPriority(GameObject.Find("Target10"));
                player.changeDest();
            }
        }
    }
    void OnTriggerExit(Collider col)
    {
        PlayerClass player = col.gameObject.GetComponent<PlayerClass>();

        if (!trigger)
        {
            player.removeImageAbovePlayer();
        }
    }
}
