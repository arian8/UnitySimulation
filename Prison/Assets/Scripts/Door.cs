using UnityEngine;

/*
    Opening/Closing of simple doors (animations)
*/

public class Door : MonoBehaviour {

	private Animation anim;
	private int nb = 0;
	private bool isclosed = true;
	public AnimationClip open;
	public AnimationClip close;

	void Start ()
    {
		anim = GetComponent<Animation> ();
		anim.AddClip(open, "ouverture");
		anim.AddClip (close, "fermeture");
	}

	void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
		{
			nb++;
			if (nb == 1) {
				anim.Play ("ouverture");
				isclosed = false;
			}
		}
	}

	void OnTriggerExit(Collider col)
	// closes only when all the players are out of the collider 
	{
		if (col.gameObject.tag == "Player")
		{
			nb--;
		}
		if ((nb == 0)&& !isclosed)
		{
			anim.Play ("fermeture");
			isclosed = true;
		}
        
	}
	
}
