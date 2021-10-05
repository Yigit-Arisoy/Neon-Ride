using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public GameObject Player;
    public float forwardPower;
    public float sidewaysPower;
    public float stickingPower;
    public float stickingSmoothness;
    public float pipeRadius;
    public Vector3 pipeCenter;
    public float car_height;
    Quaternion car_offset_quat;
    void move()
    {
        this.GetComponent<Rigidbody>().velocity = transform.forward * forwardPower;

        if (Input.GetKey(KeyCode.A))
        {
            Ray ray = new Ray(transform.position, transform.up);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo))
            {
                pipeCenter = hitInfo.point - transform.up * pipeRadius;

                if (hitInfo.collider.CompareTag("PIPE"))
                {
                    //    Debug.Log("TRANSFORM.UP" + transform.up);
                    //    Debug.Log("TOP OF THE PIPE " + hitInfo.point);
                    //    Debug.Log("PIPE CENTER " + pipeCenter);
                    Debug.DrawLine(transform.position, pipeCenter, Color.green);
                    transform.RotateAround(pipeCenter, transform.forward, sidewaysPower * (-1) * Time.deltaTime);
                }
            }
        }
        if (Input.GetKey(KeyCode.D))
        {
            Ray ray = new Ray(transform.position, transform.up);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo))
            {
                pipeCenter = hitInfo.point - transform.up * pipeRadius;

                if (hitInfo.collider.CompareTag("PIPE"))
                {
                    //   Debug.Log("TRANSFORM.UP" + transform.up);
                    //   Debug.Log("TOP OF THE PIPE " + hitInfo.point);
                    //   Debug.Log("PIPE CENTER " + pipeCenter);
                    Debug.DrawLine(transform.position, pipeCenter, Color.green);
                    transform.RotateAround(pipeCenter, transform.forward, sidewaysPower * Time.deltaTime);
                }
            }
        }
        Ray ray1 = new Ray(transform.position, -transform.up);
        RaycastHit hitInfo1;

        if (Physics.Raycast(ray1, out hitInfo1))
        {
            if (hitInfo1.collider.CompareTag("PIPE"))
            {
                //  transform.position = hitInfo1.point + hitInfo1.normal * .2f;
                this.GetComponent<Rigidbody>().AddForce(Mathf.Pow(1 + hitInfo1.distance, 7) * stickingPower * (-transform.up) * Time.deltaTime);

                Debug.DrawLine(hitInfo1.point, hitInfo1.point + hitInfo1.normal * 100);


            }
        }



    }


    void FixedUpdate()
    {
        move();
    }

    private void Start()
    {
        Debug.Log(transform.position);

    }
}
