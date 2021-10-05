using UnityEngine;

public class PipeData : MonoBehaviour
{
    public Vector3 rotationData;
    void Awake()
    {
        switch (gameObject.name)
        {
            case "PIPE_STRAIGHT_NEON_30M(Clone)":
                rotationData = new Vector3(0, 0, 0);
                break;

            case "PIPE_LEFT_SOFT_1_NEON_30M(Clone)":
                rotationData = new Vector3(0, 90, 0);
                break;

            case "PIPE_RIGHT_SOFT_1_NEON_30M(Clone)":
                rotationData = new Vector3(0, -90, 0);
                break;
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
