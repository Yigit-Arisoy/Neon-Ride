using System.Collections;
using UnityEngine;

public class Collisions : MonoBehaviour
{
    
    [SerializeField]
    bool canSlow = false;
    public bool gameOver;

    public gameSettings gameSettings;

    [SerializeField]
    PlayerStats playerStats;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "OBSTACLE")
        {
            if (playerStats.isInvulnerable)
            {
                StartCoroutine(destroyObstacle(other));
            }
            else
            {
                other.isTrigger = false;
                gameOver = true;
                canSlow = true;
                gameObject.GetComponent<Animator>().SetTrigger("gameOver");
                playerStats.gameObject.GetComponent<CarRotation>().enabled = false;
            }
        }

        if (other.tag == "INVULNERABILITY")
        {
            playerStats.Invulnerability += 1;
            Destroy(other.gameObject);
        }

        if (other.tag == "SPEED")
        {
            playerStats.speedMultiplier += 1;
            Destroy(other.gameObject);
        }
    }

    

    IEnumerator destroyObstacle(Collider theCollider)
    {
        theCollider.GetComponent<Animator>().SetTrigger("canDestroy");
        yield return new WaitForSeconds(1f);
    }
}
