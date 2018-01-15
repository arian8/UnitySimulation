using UnityEngine;
/* 
    Players ~dance~ once they escape
*/


public class Escaped : MonoBehaviour {
    
	void OnTriggerEnter(Collider col)
	{
		PlayerClass player = col.gameObject.GetComponent<PlayerClass> ();
		player.TargetsList.Clear();
		player.dress.SetActive(false);
		player.prisoner.SetActive(true);

        player.GetComponent<Animation>().Play("Idle_SexyDance");


	}
}
