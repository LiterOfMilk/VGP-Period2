using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody targetRb;
    private GameManager gameManager;
    private float minSpeed = 13;
    private float maxSpeed = 16;
    private float maxTorque = 10.0f;
    private float xRange = 4;
    private float ySpawnPos = -2;

    public ParticleSystem explostionParticle;
    public int pointValue;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        targetRb = GetComponent<Rigidbody>();
        targetRb.AddForce(RandomForce(), ForceMode.Impulse);
        targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);
        transform.position = RandomSpawnPos();
    }
    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < -10)
        {
            Destroy(gameObject);
            if(!gameObject.CompareTag("Bad") && gameManager.isGameActive)
            {
                gameManager.UpdateLives(-1);
            }
        }
    }

    public void DestroyTarget()
    {
        if (gameManager.isGameActive)
        {
            Destroy(gameObject);
            Instantiate(explostionParticle, transform.position, explostionParticle.transform.rotation);
            gameManager.UpdateScore(pointValue);
        }
    }
    

//    private void OnMouseDown()
//    {
//        if(gameManager.isGameActive && !gameManager.paused)
//        {
//            Destroy(gameObject);
//            Instantiate(explostionParticle, transform.position, explostionParticle.transform.rotation);
//            gameManager.UpdateScore(pointValue);
//        }
//    }

    Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }
    float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }
    Vector3 RandomSpawnPos()
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos);
    }

}
