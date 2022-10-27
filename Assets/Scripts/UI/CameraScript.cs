using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*SUMMARY (Updated sept 23 2022)
 * 
 * this script is for moving the camera using arrow keys at a speed set in the inspector
 */
public class CameraScript : MonoBehaviour
{
    public float cameraSpeed = 5f;

    private void Start()
    {
        transform.position =new Vector3(0, 1, 0);
    }
    // Update is called once per frame
    void Update()
    {
        //moving camera using arrow keys
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(new Vector3(cameraSpeed * Time.deltaTime, 0, 0));
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(new Vector3(-cameraSpeed * Time.deltaTime, 0, 0));
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(new Vector3(0,-cameraSpeed * Time.deltaTime,0));
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(new Vector3(0,cameraSpeed * Time.deltaTime, 0));
        }
    }
}
