using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour

{
    private Rigidbody targetRb;
    private GameManager gameManager;
    private float minSpeed = 12;
    private float maxSpeed = 14;
    private float maxTorque = 10;
    private float xRange = 4;
    private float ySpawnPos = 0;
    public int pointValue;
    public ParticleSystem explosionParticle;

    // Start is called before the first frame update
    void Start()
    {
        targetRb = GetComponent<Rigidbody>();
        targetRb.AddForce(RandomForce(), ForceMode.Impulse);
        targetRb.AddTorque(RandomTorque (), RandomTorque(), RandomTorque(), ForceMode.Impulse);

        transform.position = RandomSpawnPos();

        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
//  Targets move up with random speed
    Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }
//  Targets rotate with random direction
    float RandomTorque()
     {
       return Random.Range(-maxTorque, maxTorque);
     }
//  Targets spawn in random positions
     Vector3 RandomSpawnPos ()
     {
       return new Vector3(Random.Range(-xRange, xRange), ySpawnPos);
     }
//  Destroy targets by click mouse on them
    private void OnMouseDown()
    {
        if (gameManager.isGameActive)
        {
        Destroy(gameObject);
        gameManager.UpdateScore(pointValue);
        Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation); 
        }
       
    }
//  Destroy targets out of the scene 
    private void OnTriggerEnter(Collider other) 
    {
        Destroy(gameObject);
//  Game over if dont click good target       
        if (!gameObject.CompareTag("Bad"))
        {
            gameManager.GameOver();
        }
    }
    

}
