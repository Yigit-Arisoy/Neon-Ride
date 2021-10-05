using UnityEngine;

public class test123 : MonoBehaviour
{
    public GameObject the_pipe;
    public GameObject anchorPoint;
    public GameObject obs1, obs2;

    void Start()
    {

        anchorPoint = the_pipe.transform.GetChild(1).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        the_pipe.transform.RotateAround(anchorPoint.transform.position, transform.up, 90 * Time.deltaTime / 10);
        obs1.transform.RotateAround(anchorPoint.transform.position, transform.up, 90 * Time.deltaTime / 10);
        obs2.transform.RotateAround(anchorPoint.transform.position, transform.up, 90 * Time.deltaTime / 10);
    }
}
