using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //speed variable
    [SerializeField]
    private float _speed = 4.0f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Enemy move down
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y <= -4.0f)         //if the enemy goes off screen it respawns at a random location
        {
            transform.position = new Vector3(Random.Range(-9.0f, 9.0f), 7, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //if other is the player it destroys the enemy and damages the player
        if (other.tag == "Player")
            {
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                player.Damage();
            }

            Destroy(this.gameObject);
            }

        //if other is the laser destroy the enemy and destroy the laser
        if (other.tag == "Laser") 
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
    }
}
//-4 y. -9 to 9 x. 7.5!!