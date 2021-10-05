using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    // Start is called before the first frame update

    public int Invulnerability, pointMultiplier, speedMultiplier;
    public bool isInvulnerable, isMulPoints, isMulSpeed;
    public float invulnerabilityTime, pointTime, speedTime;
    public float invulnerabilityBaseTime, pointBaseTime, speedBaseTime;
    public float invulnerabilityTimeMultiplier, pointTimeMultiplier, speedTimeMultiplier;
    public float speedMultiplierAmount = 1;
    public float pointMultiplierAmount = 1;
    public float turningSpeed = 1;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
