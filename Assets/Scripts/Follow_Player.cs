using UnityEngine;

public class Follow_Player : MonoBehaviour
{
    public Transform player_body;
    public Vector3 offset = new Vector3(0, 0, 0);
    public float y_offset, z_offset;

    public float pos_smoothingFactor, rot_smoothingFactor;

    public Vector3 desiredPos;
    public Vector3 desiredRot;

    void Follow()
    {
        offset += player_body.forward * z_offset;
        offset += player_body.up * y_offset;

        desiredPos = player_body.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPos, pos_smoothingFactor * Time.deltaTime);

        transform.rotation = Quaternion.Lerp(transform.rotation, player_body.rotation, rot_smoothingFactor * Time.deltaTime);

        offset = Vector3.zero;
    }
    void Start()
    {


    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Follow();
    }
}
