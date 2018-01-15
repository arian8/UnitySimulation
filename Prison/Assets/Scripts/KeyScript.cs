using System.Collections.Generic;
using UnityEngine;

/*
    The key : opens the final door. 
    Can appear in the guard office, or on the dining table
    If a player takes it, the key disappears. Then, player goes directly back to the gate. 

*/

public class KeyScript : MonoBehaviour
{
    public Texture key_appears;
    private Canvas canvas;

    void Start()
    { 
        canvas = GetComponentInChildren<Canvas>();
        randomSpawnKey();
    }
    void Update()
    {
        canvas.transform.Rotate(new Vector3(0,60, 0) * Time.deltaTime, Space.World); 
        // arrow rotating above key to make it visible
    }


    public void randomSpawnKey()
    {
        List<GameObject> KeyList = new List<GameObject>();
        KeyList.Add(GameObject.Find("TargetKey1"));
        KeyList.Add(GameObject.Find("TargetKey2"));
        int rnd = Random.Range(0, 2);
        transform.position = KeyList[rnd].transform.position;
    }

    void OnTriggerEnter(Collider col) 
    {
        if (col.gameObject.tag == "Player")
        {
            PlayerClass player = col.gameObject.GetComponent<PlayerClass>();
 
            player.displayImageAbovePlayer(key_appears);

            player.TargetsList.Remove(GameObject.Find("Target7"));
            player.TargetsList.Remove(GameObject.Find("Target8"));

            player.changeTargetPriority(player.Target_Gate);
            player.changeDest();
        }
        
    }

    void OnTriggerExit(Collider col) 
    {
        if (col.gameObject.tag == "Player")
        {
            PlayerClass player = col.gameObject.GetComponent<PlayerClass>();
            player.removeImageAbovePlayer();
            keyIsTaken(player);
        }
    }
   
    public void keyIsTaken(PlayerClass player)
    {
        player.hasKey = true;
        transform.position = new Vector3(1000, 1000, 1000); // key disappears
    }

    }
