using UnityEngine;

public class rottest : MonoBehaviour
{
    public GameObject pipe_spawner;
    public GameObject anchorPoint;
    public Transform playerBody;
    public GameObject gameSettings;
    public float gameSpeed;

    // Update is called once per frame
    void LateUpdate()
    {
        anchorPoint = pipe_spawner.GetComponent<PipeSpawner>().createdPipes[0].transform.GetChild(1).gameObject;

        if (anchorPoint.transform.position.x >= playerBody.position.x)
        {
            if ((pipe_spawner.GetComponent<PipeSpawner>().createdPipes[0].GetComponent<PipeData>().rotationData.y < 0) && ((pipe_spawner.GetComponent<PipeSpawner>().createdPipes[0].transform.rotation.eulerAngles.y - 360 > pipe_spawner.GetComponent<PipeSpawner>().createdPipes[0].GetComponent<PipeData>().rotationData.y) || (pipe_spawner.GetComponent<PipeSpawner>().createdPipes[0].transform.rotation.eulerAngles.y == 0)) ||
                ((pipe_spawner.GetComponent<PipeSpawner>().createdPipes[0].GetComponent<PipeData>().rotationData.y > 0) && (pipe_spawner.GetComponent<PipeSpawner>().createdPipes[0].transform.rotation.eulerAngles.y < pipe_spawner.GetComponent<PipeSpawner>().createdPipes[0].GetComponent<PipeData>().rotationData.y)))
            {
                for (int i = 0; i < pipe_spawner.GetComponent<PipeSpawner>().createdPipes.Count; i++)
                {
                    pipe_spawner.GetComponent<PipeSpawner>().createdPipes[i].transform.RotateAround(anchorPoint.transform.position, transform.up, (int)pipe_spawner.GetComponent<PipeSpawner>().createdPipes[0].GetComponent<PipeData>().rotationData.y * Time.deltaTime);
                }

                for (int i = 0; i < (1 + 2 * gameSettings.GetComponent<gameSettings>().gameDifficulty) * pipe_spawner.GetComponent<PipeSpawner>().createdPipes.Count; i++) //set the amount of obstacles per pipe
                {
                    pipe_spawner.GetComponent<PipeSpawner>().createdObstacles[i].transform.RotateAround(anchorPoint.transform.position, transform.up, (int)pipe_spawner.GetComponent<PipeSpawner>().createdPipes[0].GetComponent<PipeData>().rotationData.y * Time.deltaTime);
                }
            }
        }

        for (int i = 0; i < pipe_spawner.GetComponent<PipeSpawner>().createdPipes.Count; i++)
        {
            pipe_spawner.GetComponent<PipeSpawner>().createdPipes[i].transform.position += Vector3.right * Time.deltaTime * gameSpeed;
        }

        for (int i = 0; i < (1 + 2 * gameSettings.GetComponent<gameSettings>().gameDifficulty) * pipe_spawner.GetComponent<PipeSpawner>().createdPipes.Count; i++) //set the amount of obstacles per pipe
        {
            pipe_spawner.GetComponent<PipeSpawner>().createdObstacles[i].transform.position += Vector3.right * Time.deltaTime * gameSpeed;
        }

    }


}