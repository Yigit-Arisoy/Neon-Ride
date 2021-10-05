using UnityEngine;

public class get_size : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(GetComponent<Renderer>().bounds);
    }

    // Update is called once per frame
    void Update()
    {


    }
}
