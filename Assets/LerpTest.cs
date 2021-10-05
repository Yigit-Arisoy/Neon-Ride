using UnityEngine;

public class LerpTest : MonoBehaviour
{
    float turn_time;
    float _angle;
    void Update()
    {
        _angle = Mathf.Lerp(0, -90, turn_time);
        Debug.Log(_angle);
        turn_time += Time.deltaTime / 5;

        Debug.Log(Mathf.Abs(_angle - (-90)));

        if (Mathf.Abs(_angle - (-90)) == 0)
            Debug.Log("Finished");


    }
}
