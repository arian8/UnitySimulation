using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*
    Game Management Script :
        - camera switch
        - random players spawn in the 6 bedrooms
        - pause menu
        - players navigation
*/

public class GameScript : MonoBehaviour
{ 
    // Cameras
    public Camera main ;
    public Camera p1;
    public Camera p2;
    public Camera p3;
	public Camera p4;
    public Camera active;

    // Players
    public GameObject player1;
    public GameObject player2;
    public GameObject player3;
    public PlayerClass[] players = new PlayerClass[3];

    // Game pause
    public GameObject canvas; 
    public Text text;

    // Pour la navigation
    private Vector3 targetposition;

    void Start()
    {
        // Camera initialisation
        #region
        active = main;
        main.enabled = true;
        p1.enabled = false;
        p2.enabled = false;
        p3.enabled = false;
		p4.enabled = false;
        #endregion

        // Players array initialisation 
        players[0] = player1.GetComponent<PlayerClass>();
        players[1] = player2.GetComponent<PlayerClass>();
        players[2] = player3.GetComponent<PlayerClass>();

        agentsRandomSpawn(players);

    }

    void Update()
    {
        //Pause menu
        #region
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(canvas.gameObject.activeInHierarchy==false)
            {
                Time.timeScale = 0;
                text.text = "PRESS ECHAP TO RESUME";
                canvas.gameObject.SetActive(true);

            }
            else
            {
                text.text = "PRESS ECHAP TO PAUSE";
                canvas.gameObject.SetActive(false);
                Time.timeScale = 1;
            }
            
        }
        #endregion

        //Game resume
        #region
        if (Input.GetKeyDown (KeyCode.P)) 
		{
			SceneManager.LoadScene("simulation");
		}
        #endregion

        //Camera Switch 
        #region
        if (Input.GetKeyDown(KeyCode.A)) //Main
        {
            active = main;
            main.enabled = true;
            p1.enabled = false;
            p2.enabled = false;
            p3.enabled = false;
			p4.enabled = false;
        }

        if (Input.GetKeyDown(KeyCode.Z)) //1 Orouge
        {
            active = p1;
            main.enabled = false;
            p1.enabled = true;
            p2.enabled = false;
            p3.enabled = false;
			p4.enabled = false;
        }
        if (Input.GetKeyDown(KeyCode.E)) //2 Vert
        {
            active = p2;
            main.enabled = false;
            p1.enabled = false;
            p2.enabled = true;
            p3.enabled = false;
			p4.enabled = false;
        }

        if (Input.GetKeyDown(KeyCode.R)) //3 Jaune
        {
            active = p3;
            main.enabled = false;
            p1.enabled = false;
            p2.enabled = false;
            p3.enabled = true;
			p4.enabled = false;
        }

		if (Input.GetKeyDown(KeyCode.T)) //3 Jaune
		{
            active = p4;
			main.enabled = false;
			p1.enabled = false;
			p2.enabled = false;
			p3.enabled = false;
			p4.enabled = true;
		}
        #endregion

        // Basic Navigation (players goes through its TargetsList - see PlayerClass)
        #region
        foreach (PlayerClass pl in players)
        {
       
            /* 
             * Affichage de la liste des targets pour chaque joueur, si besoin
               string n = "";
               for (int i = 0; i < pl.TargetsList.Count; i++)
               {n += pl.TargetsList[i].name; }
               Debug.Log(pl.name +" : "+n); 
            */

            if (pl.TargetsList.Count != 0)
            // cas où la liste est vide : le joueur est à la porte finale
            {
                if (Vector3.Distance(pl.TargetsList[0].transform.position, pl.body.transform.position) < 1.5)
                // si joueur est proche de sa target, on la remet en fin de liste
                {
                    pl.keepWalking();
                }
            }
        }
        #endregion
    }

    public void agentsRandomSpawn(PlayerClass[] players) 
    // Players random spawn (+ first targets assigned)
    {
        bool[] bool_players = new bool[6];

        // each room can only contain one player

        for (int i = 0; i < 6; i++)
        {
            bool_players[i] = true;
        }

        foreach (PlayerClass pl in players)
        {
            pl.GenerateTargetsList();  // setting the player's target list 
            pl.removeImageAbovePlayer();

            int rnd = Random.Range(0, 6);

            while (!bool_players[rnd])
            {
                rnd = Random.Range(0, 6);
            }

            if (bool_players[rnd])
            {
                GameObject spawning_position = GameObject.Find("Target" + rnd);
                pl.body.transform.position = spawning_position.transform.position;
                bool_players[rnd] = !bool_players[rnd];
            }

            if (pl.TargetsList[0]) // setting the player's first destination
            {
                pl.agent.destination = pl.TargetsList[0].transform.position;
            }
        }
    }
}