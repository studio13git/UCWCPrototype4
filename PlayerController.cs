using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed =0.5f;
    private Rigidbody playerRb;
    private GameObject focalPoint;
    public GameObject powerUpIndicator;
    public bool hasPowerup = false;
    private float powerUpStrength = 10;


    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");

    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * forwardInput * speed);
        powerUpIndicator.transform.position = transform.position + new Vector3(0,-0.5f, 0);
        
    }
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("PowerUp"))
        {
            Destroy(other.gameObject);
            hasPowerup = true;
            powerUpIndicator.gameObject.SetActive(true);
            Debug.Log(hasPowerup);
        }
    }
     private void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.CompareTag("Enemy") && hasPowerup){
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position);
            Debug.Log("You hit him and you have a powerup");
            enemyRigidbody.AddForce(awayFromPlayer * powerUpStrength, ForceMode.Impulse );
            StartCoroutine(PowerupCountdownRoutine());
        }
    
    }
    IEnumerator PowerupCountdownRoutine(){
        yield return new WaitForSeconds(7);
        powerUpIndicator.gameObject.SetActive(true);
        hasPowerup = false;
        Debug.Log(hasPowerup);
    }
    
}
