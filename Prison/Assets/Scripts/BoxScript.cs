using UnityEngine;

/*
    Box contains the disguise. Appears in one of the bedrooms.

    If the player saw the guard (at the gate) already, he knows he needs to dress up. 
    Once he's hidden, he goes directly to the gate. 
    
    Otherwise, he just remembers this position as a "target of interest" (Target_Box : 
    so he can go back there directly when he meets the guard) and keeps walking. 

*/

public class BoxScript : MonoBehaviour
{
	private int posCoffre;
    private Animation anim;
    public Texture guard_appears; 
    
    void Start()
    // Box randomly spawns in one of the 6 bedrooms
    {
        int rnd = Random.Range(0, 6);
		 posCoffre = rnd;
         anim = GetComponent<Animation>();
         anim.Play("Coffre" + posCoffre);
     }

    void OnTriggerEnter(Collider col) 
    {
        if (col.gameObject.tag == "Player")
        {
            PlayerClass player = col.gameObject.GetComponent<PlayerClass>();

			player.Target_Box = GameObject.Find("Target"+posCoffre); // The player keeps the position as Target_Box

            if (player.Target_Gate != null)   
                // he knows he has to dress up: been to the gate already
            {
                player.displayImageAbovePlayer(guard_appears);
                player.isDressed = true;
                player.prisoner.SetActive(false);
                player.dress.SetActive(true);

                 player.changeTargetPriority(player.Target_Gate);  
                //Goes directly to the gate, to pass the security guard
                 player.keepWalking(player.Target_Box);
            }
            
            else 
            {
                player.keepWalking();
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            PlayerClass player = col.gameObject.GetComponent<PlayerClass>();
            player.removeImageAbovePlayer();
        }
    }
}
