using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using UnityEngine.UI;

/* 
    PlayerScript linked to each player to : 
    - access their game data (bool and GameObjects)
    - move it 
    - display/remove image on their own canvas

 */

public class PlayerClass : MonoBehaviour
{
    public GameObject body;
    public NavMeshAgent agent;
    public List<GameObject> TargetsList;

    public bool isDressed = false;
    public bool hasKey = false;
    public bool passedSecurity = false;

    // Targets of interest : the ones the player will remember if it explores them
    public GameObject Target_Box;
    public GameObject Target_Gate;
    public GameObject Target_FinalDoor;

    // If the player is dressed as a prisonner or disguised
    public GameObject prisoner;
    public GameObject dress;

    public void GenerateTargetsList()
    // random generation of targets list for the player
    {
        bool[] bool_env = new bool[8]; 
        // at the very beginning, only 8 targets possible : 6 bedroom + 1 gate + 1 final door

        for (int i = 0; i < 8; i++)
        {
            bool_env[i] = true;
        }

        while (TargetsList.Count < 8)
        {
            int rnd = Random.Range(0, 8);

            while (!bool_env[rnd])
            {
                rnd = Random.Range(0, 8);
            }

            if (bool_env[rnd])
            {
                if (rnd == 7)
                {
                    TargetsList.Add(GameObject.Find("Target9"));
                } 
                else
                {
                    TargetsList.Add(GameObject.Find("Target" + rnd));  
                }
                bool_env[rnd] = !bool_env[rnd];
            }
        }
        
    }


    // Navigation functions : 
    #region
    public void keepWalking() 
    {
        GameObject temp = TargetsList[0];
        TargetsList.Remove(temp);
        TargetsList.Add(temp);
        agent.destination = TargetsList[0].transform.position;
    }

    public void keepWalking(GameObject target)
    {
        TargetsList.Remove(target);
        TargetsList.Add(target);
        agent.destination = TargetsList[0].transform.position;
    }

    public void changeTargetPriority(GameObject target)
    {
        TargetsList.Remove(target);
        TargetsList.Insert(0, target);

    }
    public void changeDest()
    {
        agent.destination = TargetsList[0].transform.position;
    }
    #endregion

    //Displaying/Removing images above the players :
    #region
    public void displayImageAbovePlayer(Texture tex)
    {
        Canvas can = GetComponentInChildren<Canvas>();
        RawImage image = can.GetComponentInChildren<RawImage>();
        image.enabled = true;
        image.texture = tex;

    }

    public void removeImageAbovePlayer() 
    {
        Canvas can = GetComponentInChildren<Canvas>();
        if (can.GetComponentInChildren<RawImage>())
        {
            RawImage image = can.GetComponentInChildren<RawImage>();
            image.enabled = false;
        }
    }
    #endregion

}

