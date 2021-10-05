using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PipeSpawner : MonoBehaviour
{
    public PlayerStats playerStats;
    public GameObject[] Pipes;
    public GameObject[] Obstacles;
    public GameObject[] PowerUps;
    public List<GameObject> createdPipes, createdObstacles, createdPowerUps;
    public int chosenPipeIndex, chosenObstacleIndex, chosenPowerUpIndex;
    public Transform Player;
    public Transform endMark;
    public Vector3 oldPosition;
    public float distanceTravelled = 0;
    public float distanceTravelledPowerUp = 0;
    public float pipeDeletionDistance, pipeSpawningDistance;
    public GameObject gameSettings;
    public GameObject anchorPoint;
    public float oldRotation;
    public float turningAngle;
    public float turningTime;
    public float turningTimeDelta;
    public bool isRotating = false;
    public bool isMoving = true;
    public bool isSmoothStart, isSmoothEnd, isRotated;
    public float smoothnessTime;
    public float timePassed;
    public float playerRotationSpeed;
    public float carHeight;
    public float pipeRadius;


    public Vector3 current_rotation;
    void Spawn_Pipe()
    {
        if (!isRotating)
        {
            endMark = GameObject.Find("END_MARK").transform;

            if (Vector3.Distance(Player.position, endMark.position) < pipeSpawningDistance)
            {
                chosenPipeIndex = Random.Range(0, Pipes.Length);

                if (Mathf.Abs((current_rotation + Pipes[chosenPipeIndex].GetComponent<PipeData>().rotationData).x) < 180 &&
                   Mathf.Abs((current_rotation + Pipes[chosenPipeIndex].GetComponent<PipeData>().rotationData).y) < 180 &&
                   Mathf.Abs((current_rotation + Pipes[chosenPipeIndex].GetComponent<PipeData>().rotationData).z) < 180)
                {
                    GameObject lastObject = Instantiate(Pipes[chosenPipeIndex], endMark.position, Quaternion.Euler(current_rotation));
                    Destroy(endMark.gameObject);
                    createdPipes.Add(lastObject);
                    SpawnObstacles(lastObject);
                    if (Convert.ToBoolean(Random.Range(0, 2)))
                        SpawnPowerUps(lastObject);

                    current_rotation += createdPipes[createdPipes.Count - 1].GetComponent<PipeData>().rotationData;
                }
            }
        }
    }
    void FixedUpdate()
    {
        Spawn_Pipe();
        DeletePipes();
        DeletePowerUps();

        anchorPoint = createdPipes[0].transform.GetChild(1).gameObject;

        MoveForward();
        RotatePipes();

        //    PlayerMovement();
    }

    private void Start()
    {

        //   InvokeRepeating("Spawn_Pipe", 0, 0.5f);
        //    InvokeRepeating("DeletePipes", 2, 0.5f);        
    }

    void DeletePipes()
    {

        if (!isRotating)
        {
            distanceTravelled = createdPipes[0].transform.position.x - Player.position.x;
            if (distanceTravelled > pipeDeletionDistance)
            {
                Destroy(createdPipes[0]);
                for (int i = 0; i < createdPipes.Count - 1; i++)
                {
                    createdPipes[i] = createdPipes[i + 1];
                }
                createdPipes.RemoveAt(createdPipes.Count - 1);
                createdPipes.TrimExcess();

                for (int i = 0; i < 1 + 2 * gameSettings.GetComponent<gameSettings>().gameDifficulty; i++)
                {
                    Destroy(createdObstacles[i]);
                }
                for (int i = 0; i < (1 + 2 * gameSettings.GetComponent<gameSettings>().gameDifficulty) * createdPipes.Count; i++)
                {
                    createdObstacles[i] = createdObstacles[i + 1 + 2 * gameSettings.GetComponent<gameSettings>().gameDifficulty];
                }

                for (int i = 0; i < 1 + 2 * gameSettings.GetComponent<gameSettings>().gameDifficulty; i++)
                {
                    createdObstacles.RemoveAt(createdObstacles.Count - 1 - i);
                }
                createdObstacles.TrimExcess();
            }
        }
    }

    void DeletePowerUps()
    {
        if (!isRotating)
        {
            if (createdPowerUps.Count > 0)
            {
                distanceTravelledPowerUp = createdPowerUps[0].transform.position.x - Player.position.x;

                if (distanceTravelledPowerUp > pipeDeletionDistance)
                {
                    Destroy(createdPowerUps[0]);
                    for (int i = 0; i < createdPowerUps.Count - 1; i++)
                    {
                        createdPowerUps[i] = createdPowerUps[i + 1];
                    }
                    createdPowerUps.RemoveAt(createdPowerUps.Count - 1);
                    createdPowerUps.TrimExcess();
                }

            }

        }
    }
    void SpawnObstacles(GameObject the_pipe)
    {
        switch (chosenPipeIndex)
        {
            case 0:
                for (int i = 0; i < 1 + 2 * gameSettings.GetComponent<gameSettings>().gameDifficulty; i++) //set the amount of obstacles per pipe
                {
                    Vector3 spawnPos = new Vector3(the_pipe.transform.position.x, the_pipe.transform.position.y, the_pipe.transform.position.z); // location for the obstacle to spawn
                    spawnPos += the_pipe.transform.right * Random.Range(-3, -27);

                    int spawnRot_x = Random.Range(-180, 180);
                    Vector3 spawnRot = new Vector3(spawnRot_x, 0, 0);
                    spawnRot += current_rotation;

                    chosenObstacleIndex = Random.Range(0, Obstacles.Length);
                    GameObject lastObstacle = Instantiate(Obstacles[chosenObstacleIndex], spawnPos, Quaternion.Euler(spawnRot));
                    createdObstacles.Add(lastObstacle);
                }
                break;

            case 1:
                for (int i = 0; i < 1 + 2 * gameSettings.GetComponent<gameSettings>().gameDifficulty; i++) //set the amount of obstacles per pipe
                {
                    Vector3 spawnPos = new Vector3(the_pipe.transform.position.x, the_pipe.transform.position.y, the_pipe.transform.position.z);
                    Vector3 spawnPosOffset = new Vector3(0, 0, 0);
                    Vector3 spawnRotOffset = new Vector3(0, 0, 0);
                    if (Convert.ToBoolean(Random.Range(0, 2)))
                    {
                        spawnPosOffset = the_pipe.transform.right * Random.Range(-3, -23);
                        spawnRotOffset = current_rotation;

                    }
                    else
                    {
                        spawnPosOffset = -the_pipe.transform.right * 30;
                        spawnPosOffset += the_pipe.transform.forward * Random.Range(9, 27);
                        spawnRotOffset = current_rotation + createdPipes[createdPipes.Count - 1].GetComponent<PipeData>().rotationData;

                    }
                    // location for the obstacle to spawn
                    spawnPos += spawnPosOffset;

                    // rotation for the obstacle to spawn
                    int spawnRot_x = Random.Range(-180, 180);
                    Vector3 spawnRot = new Vector3(spawnRot_x, 0, 0);
                    spawnRot += spawnRotOffset;

                    chosenObstacleIndex = Random.Range(0, Obstacles.Length);
                    GameObject lastObstacle = Instantiate(Obstacles[chosenObstacleIndex], spawnPos, Quaternion.Euler(spawnRot));
                    createdObstacles.Add(lastObstacle);
                }
                break;

            case 2:
                for (int i = 0; i < 1 + 2 * gameSettings.GetComponent<gameSettings>().gameDifficulty; i++) //set the amount of obstacles per pipe
                {
                    Vector3 spawnPos = new Vector3(the_pipe.transform.position.x, the_pipe.transform.position.y, the_pipe.transform.position.z);
                    Vector3 spawnPosOffset = new Vector3(0, 0, 0);
                    Vector3 spawnRotOffset = new Vector3(0, 0, 0);
                    if (Convert.ToBoolean(Random.Range(0, 2)))
                    {
                        spawnPosOffset = the_pipe.transform.right * Random.Range(-3, -23);
                        spawnRotOffset = current_rotation;
                    }
                    else
                    {
                        spawnPosOffset = -the_pipe.transform.right * 30;
                        spawnPosOffset += the_pipe.transform.forward * Random.Range(-9, -27);
                        spawnRotOffset = current_rotation + createdPipes[createdPipes.Count - 1].GetComponent<PipeData>().rotationData;
                    }
                    // location for the obstacle to spawn
                    spawnPos += spawnPosOffset;
                    int spawnRot_x = Random.Range(-180, 180);
                    Vector3 spawnRot = new Vector3(spawnRot_x, 0, 0);
                    spawnRot += spawnRotOffset;

                    chosenObstacleIndex = Random.Range(0, Obstacles.Length);

                    GameObject lastObstacle = Instantiate(Obstacles[chosenObstacleIndex], spawnPos, Quaternion.Euler(spawnRot));
                    createdObstacles.Add(lastObstacle);
                }
                break;
        }
    }

    void SpawnPowerUps(GameObject the_pipe)
    {
        switch (chosenPipeIndex)
        {
            case 0:
                {
                    Vector3 spawnPos = new Vector3(the_pipe.transform.position.x, the_pipe.transform.position.y, the_pipe.transform.position.z); // location for the obstacle to spawn
                    spawnPos += the_pipe.transform.right * Random.Range(-3, -27);

                    int spawnRot_x = Random.Range(-180, 180);
                    Vector3 spawnRot = new Vector3(spawnRot_x, 0, 0);
                    spawnRot += current_rotation;

                    chosenPowerUpIndex = Random.Range(0, PowerUps.Length);
                    GameObject lastPowerUp = Instantiate(PowerUps[chosenPowerUpIndex], spawnPos, Quaternion.Euler(spawnRot));
                    createdPowerUps.Add(lastPowerUp);
                    break;
                }


            case 1:
                {
                    Vector3 spawnPos = new Vector3(the_pipe.transform.position.x, the_pipe.transform.position.y, the_pipe.transform.position.z);
                    Vector3 spawnPosOffset = new Vector3(0, 0, 0);
                    Vector3 spawnRotOffset = new Vector3(0, 0, 0);
                    if (Convert.ToBoolean(Random.Range(0, 2)))
                    {
                        spawnPosOffset = the_pipe.transform.right * Random.Range(-3, -23);
                        spawnRotOffset = current_rotation;

                    }
                    else
                    {
                        spawnPosOffset = -the_pipe.transform.right * 30;
                        spawnPosOffset += the_pipe.transform.forward * Random.Range(9, 27);
                        spawnRotOffset = current_rotation + createdPipes[createdPipes.Count - 1].GetComponent<PipeData>().rotationData;

                    }
                    // location for the obstacle to spawn
                    spawnPos += spawnPosOffset;

                    // rotation for the obstacle to spawn
                    int spawnRot_x = Random.Range(-180, 180);
                    Vector3 spawnRot = new Vector3(spawnRot_x, 0, 0);
                    spawnRot += spawnRotOffset;

                    chosenPowerUpIndex = Random.Range(0, PowerUps.Length);
                    GameObject lastPowerUp = Instantiate(PowerUps[chosenPowerUpIndex], spawnPos, Quaternion.Euler(spawnRot));
                    createdPowerUps.Add(lastPowerUp); ;
                    break;
                }


            case 2:
                {
                    Vector3 spawnPos = new Vector3(the_pipe.transform.position.x, the_pipe.transform.position.y, the_pipe.transform.position.z);
                    Vector3 spawnPosOffset = new Vector3(0, 0, 0);
                    Vector3 spawnRotOffset = new Vector3(0, 0, 0);
                    if (Convert.ToBoolean(Random.Range(0, 2)))
                    {
                        spawnPosOffset = the_pipe.transform.right * Random.Range(-3, -23);
                        spawnRotOffset = current_rotation;
                    }
                    else
                    {
                        spawnPosOffset = -the_pipe.transform.right * 30;
                        spawnPosOffset += the_pipe.transform.forward * Random.Range(-9, -27);
                        spawnRotOffset = current_rotation + createdPipes[createdPipes.Count - 1].GetComponent<PipeData>().rotationData;
                    }
                    // location for the obstacle to spawn
                    spawnPos += spawnPosOffset;
                    int spawnRot_x = Random.Range(-180, 180);
                    Vector3 spawnRot = new Vector3(spawnRot_x, 0, 0);
                    spawnRot += spawnRotOffset;

                    chosenPowerUpIndex = Random.Range(0, PowerUps.Length);
                    GameObject lastPowerUp = Instantiate(PowerUps[chosenPowerUpIndex], spawnPos, Quaternion.Euler(spawnRot));
                    createdPowerUps.Add(lastPowerUp);
                    break;
                }

        }
    }
    void MoveForward()
    {
        if (!isRotating)
        {
            Vector3 forwardSpeed = Vector3.right * Time.deltaTime * gameSettings.GetComponent<gameSettings>().gameSpeed * playerStats.speedMultiplierAmount;
            for (int i = 0; i < createdPipes.Count; i++)
            {
                createdPipes[i].transform.position += forwardSpeed;
            }

            for (int i = 0; i < (1 + 2 * gameSettings.GetComponent<gameSettings>().gameDifficulty) * createdPipes.Count; i++) //set the amount of obstacles per pipe
            {
                createdObstacles[i].transform.position += forwardSpeed;
            }
            for (int i = 0; i < createdPowerUps.Count; i++)
            {
                createdPowerUps[i].transform.position += forwardSpeed;
            }


            if (anchorPoint.transform.position.x >= Player.position.x && !anchorPoint.GetComponent<AnchorData>().isUsed)
            {
                isMoving = false;
                isRotating = true;
                timePassed = 0;
            }
        }
    }

    void RotatePipes()
    {
        if (!isMoving)
        {
            turningAngle = Mathf.Lerp(0, -createdPipes[0].GetComponent<PipeData>().rotationData.y, turningTimeDelta * playerStats.turningSpeed);
            turningTimeDelta += Time.deltaTime / (14.137f / gameSettings.GetComponent<gameSettings>().gameSpeed);

            

            for (int i = 0; i < createdPipes.Count; i++)
            {
                createdPipes[i].transform.RotateAround(anchorPoint.transform.position, transform.up, turningAngle - oldRotation);
            }

            for (int i = 0; i < (1 + 2 * gameSettings.GetComponent<gameSettings>().gameDifficulty) * createdPipes.Count; i++) //set the amount of obstacles per pipe
            {
                createdObstacles[i].transform.RotateAround(anchorPoint.transform.position, transform.up, turningAngle - oldRotation);
            }

            for (int i = 0; i < createdPowerUps.Count; i++)
            {
                createdPowerUps[i].transform.RotateAround(anchorPoint.transform.position, transform.up, turningAngle - oldRotation);
            }


            oldRotation = turningAngle;

            if (Mathf.Abs(turningAngle - (-createdPipes[0].GetComponent<PipeData>().rotationData.y)) == 0)
            {
                isRotating = false;
                isMoving = true;
                oldRotation = 0;
                turningAngle = 0;
                turningTimeDelta = 0;
                anchorPoint.GetComponent<AnchorData>().isUsed = true;
                current_rotation -= createdPipes[0].GetComponent<PipeData>().rotationData;
            }
            timePassed += Time.deltaTime;
        }

    }

    void SmoothInForward()
    {
        if (isSmoothEnd)
        {
            Debug.Log("Inside Forward In");
            for (int i = 0; i < createdPipes.Count; i++)
            {
                createdPipes[i].transform.position += Vector3.right * Time.deltaTime * gameSettings.GetComponent<gameSettings>().gameSpeed * Mathf.Clamp((Mathf.Pow(timePassed, 2) / Mathf.Pow(smoothnessTime, 2)), 0, 1);
            }

            for (int i = 0; i < (1 + 2 * gameSettings.GetComponent<gameSettings>().gameDifficulty) * createdPipes.Count; i++) //set the amount of obstacles per pipe
            {
                createdObstacles[i].transform.position += Vector3.right * Time.deltaTime * gameSettings.GetComponent<gameSettings>().gameSpeed * Mathf.Clamp((Mathf.Pow(timePassed, 2) / Mathf.Pow(smoothnessTime, 2)), 0, 1);
            }
        }
    }

    void SmoothOutForward()
    {
        if (!isSmoothStart)
        {
            if (timePassed < smoothnessTime)
            {
                Debug.Log("Inside Forward Out");
                for (int i = 0; i < createdPipes.Count; i++)
                {
                    createdPipes[i].transform.position += Vector3.right * Time.deltaTime * gameSettings.GetComponent<gameSettings>().gameSpeed * Mathf.Clamp((Mathf.Pow(smoothnessTime, 2) - (Mathf.Pow(timePassed, 2))) / Mathf.Pow(smoothnessTime, 2), 0, 1);
                }

                for (int i = 0; i < (1 + 2 * gameSettings.GetComponent<gameSettings>().gameDifficulty) * createdPipes.Count; i++) //set the amount of obstacles per pipe
                {
                    createdObstacles[i].transform.position += Vector3.right * Time.deltaTime * gameSettings.GetComponent<gameSettings>().gameSpeed * (1 - (timePassed * timePassed));
                }
            }
        }
    }

    void SmoothInRotate()
    {
        if (!isSmoothStart)
        {
            if (timePassed < smoothnessTime)
            {
                Debug.Log("Inside Rotate In");
                turningAngle = Mathf.Lerp(0, -createdPipes[0].GetComponent<PipeData>().rotationData.y, turningTimeDelta);
                turningTimeDelta += Time.deltaTime / (14.137f / (gameSettings.GetComponent<gameSettings>().gameSpeed * Mathf.Clamp((Mathf.Pow(timePassed, 2) / Mathf.Pow(smoothnessTime, 2)), 0, 1)));


                for (int i = 0; i < createdPipes.Count; i++)
                {
                    createdPipes[i].transform.RotateAround(anchorPoint.transform.position, transform.up, turningAngle - oldRotation);
                }

                for (int i = 0; i < (1 + 2 * gameSettings.GetComponent<gameSettings>().gameDifficulty) * createdPipes.Count; i++) //set the amount of obstacles per pipe
                {
                    createdObstacles[i].transform.RotateAround(anchorPoint.transform.position, transform.up, turningAngle - oldRotation);
                }

                oldRotation = turningAngle;
            }
            else
                isSmoothStart = true;
        }
    }

    void SmoothOutRotate()
    {
        if (isSmoothEnd)
        {
            Debug.Log("Inside Rotate Out");
            turningAngle = Mathf.Lerp(0, -createdPipes[0].GetComponent<PipeData>().rotationData.y, turningTimeDelta);
            turningTimeDelta += Time.deltaTime / (14.137f / (gameSettings.GetComponent<gameSettings>().gameSpeed * Mathf.Clamp((Mathf.Pow(smoothnessTime, 2) - (Mathf.Pow(timePassed, 2))) / Mathf.Pow(smoothnessTime, 2), 0, 1)));

            for (int i = 0; i < createdPipes.Count; i++)
            {
                createdPipes[i].transform.RotateAround(anchorPoint.transform.position, transform.up, turningAngle - oldRotation);
            }

            for (int i = 0; i < (1 + 2 * gameSettings.GetComponent<gameSettings>().gameDifficulty) * createdPipes.Count; i++) //set the amount of obstacles per pipe
            {
                createdObstacles[i].transform.RotateAround(anchorPoint.transform.position, transform.up, turningAngle - oldRotation);
            }

            oldRotation = turningAngle;
        }
    }

    void PlayerMovement()
    {
        if (Input.GetKey(KeyCode.A))
        {
            for (int i = 0; i < createdPipes.Count; i++)
            {
                createdPipes[i].transform.RotateAround(Player.position + Player.up * (pipeRadius - carHeight), Player.forward, playerRotationSpeed * Time.deltaTime);
                current_rotation += new Vector3(playerRotationSpeed * Time.deltaTime, 0, 0);
            }

            for (int i = 0; i < (1 + 2 * gameSettings.GetComponent<gameSettings>().gameDifficulty) * createdPipes.Count; i++) //set the amount of obstacles per pipe
            {
                createdObstacles[i].transform.RotateAround(Player.position + Player.up * (pipeRadius - carHeight), Player.forward, playerRotationSpeed * Time.deltaTime);
            }

        }

        if (Input.GetKey(KeyCode.D))
        {
            for (int i = 0; i < createdPipes.Count; i++)
            {
                createdPipes[i].transform.RotateAround(Player.position + Player.up * (pipeRadius - carHeight), Player.forward, -playerRotationSpeed * Time.deltaTime);
                current_rotation += new Vector3(-playerRotationSpeed * Time.deltaTime, 0, 0);
            }

            for (int i = 0; i < (1 + 2 * gameSettings.GetComponent<gameSettings>().gameDifficulty) * createdPipes.Count; i++) //set the amount of obstacles per pipe
            {
                createdObstacles[i].transform.RotateAround(Player.position + Player.up * (pipeRadius - carHeight), Player.forward, -playerRotationSpeed * Time.deltaTime);
            }

        }
    }
}


