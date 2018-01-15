using UnityEngine;

/*
    UI management script. Each player has a canvas above their head, 
    that follows them and always face the current camera. 

 */

public class CanvasController : MonoBehaviour
{
    private Vector3 offset;
    private Canvas canvas;
    private Camera cam_active; 


    void Start()
    {
        canvas = GetComponentInChildren<Canvas>();
        cam_active = GameObject.Find("Main Camera").GetComponent<Camera>();
        offset = canvas.transform.position - transform.position; 
        canvas.transform.LookAt(cam_active.transform.position);
    }

    void Update()
    {
        cam_active = GameObject.Find("Main Camera").GetComponent<GameScript>().active;
        canvas.transform.position = transform.position + offset;
        canvas.transform.LookAt(cam_active.transform.position);
    }
}