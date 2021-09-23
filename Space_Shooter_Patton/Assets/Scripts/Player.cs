using System.Collections; //namespace Unity default
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{ 
//varible requirements * public or private refrence, data type (int, float, bool, string), a name. Optional value assigned.
    [SerializeField] //allows for changing in the inspecter
    private float _speed = 2.5f; //float is a number a decimal and we're creating the _speed varible with a 2.5 vaule
    //create prefab variable for the laser
    [SerializeField]
    private GameObject _laserPrefab;

    [SerializeField]
    private int _lives = 3; //setting our lives to a value of 3 to start

    private SpawnManager _spawnManager;

    private UIManager _uiManager;

    void Start()     // Start is called before the first frame update
    {
        //player start position = (0, 0, 0)
        transform.position = new Vector3(0, 0, 0); //accessing transform component to set new position to 0, 0, 0
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
    }


    void Update()    // Update is called once per frame
    {
        PlayerMovements();
        //if the space button is pushed the then the laser gets instantiated (created)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);
        }
    }
    void PlayerMovements()
    {
        float horizontalInput = Input.GetAxis("Horizontal"); //defining the horizontal input variable and using Unity default name
        float verticalInput = Input.GetAxis("Vertical"); //define vertical input variable using unity default name

        transform.Translate(Vector3.right * horizontalInput * _speed * Time.deltaTime); //new Vector 3 (1, 0, 0) * player horizontal input * speed vaariable * real time
        transform.Translate(Vector3.up * verticalInput * _speed * Time.deltaTime); //create transform.Translate for vertical input 

        //if the player's y vaule is > 5.9 or < -3.9 the player will stop at those locations
        if (transform.position.y >= 5.9f)
        {
            transform.position = new Vector3(transform.position.x, 5.9f, 0);
        }
        else if (transform.position.y <= -3.9f)
        {
            transform.position = new Vector3(transform.position.x, -3.9f, 0);
        }
        if (transform.position.x >= 9.1f)
        {
            transform.position = new Vector3(9.1f, transform.position.y, 0);
        }
        else if (transform.position.x <= -9.1f)
        {
            transform.position = new Vector3(-9.1f, transform.position.y, 0);
        }
    }
        public void Damage()
        {
             _lives -= 1;
        _uiManager.UpdateLives(_lives); //links to UI Manager to update the current lives

         if (_lives < 1)
            {
            _spawnManager.OnPlayerDeath(); 
            Destroy(this.gameObject);
            }
        }
 }


// .8 is where the laser should be. 7.5 should be where it is destroyed
// SpaceShooter

/*
 Optimizatized Options
combine varibles: transform.Translate(new Vector3(horizontalInput, verticalInput, 0) *_speed *TimedeltaTime);

or combine variables and create a new direction variable:
Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
transform.Translate((direction) * _speed *Time.deltaTime);    */