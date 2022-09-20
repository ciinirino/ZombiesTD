using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{

    //speed is currently adjusted in inspector
    public float speed = 1f;

    //target is gotten from parent tower object
    public Transform target;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
    }

    public void OnTriggerEnter(Collider other)
    {
        //if the hit object is a wall destroy the bullet
        if (other.CompareTag("Obstacle"))
        {
            Destroy(this.gameObject);
        }
        //if the object hit a zombie destroy both the bullet and the zombie
        else if (other.CompareTag("Zombie"))
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }


}
