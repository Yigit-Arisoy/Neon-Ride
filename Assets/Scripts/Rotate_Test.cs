using UnityEngine;

public class Rotate_Test : MonoBehaviour
{

    public Vector3 current_up;

    void FixedUpdate()
    {
        //  transform.RotateAround(new Vector3(5, 0, 0), new Vector3(0, 0, 1), 60 * Time.deltaTime);
        current_up = this.transform.TransformDirection(Vector3.up);
        Ray ray = new Ray(this.transform.position, current_up);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo))
        {
            if (hitInfo.transform.gameObject.CompareTag("PipeRef"))
            {
                Debug.DrawLine(transform.position, hitInfo.point, Color.green);
                Debug.Log(hitInfo.point);
            }


            else
                Debug.DrawLine(transform.position, current_up * 100, Color.red);

        }

        if (Input.GetKey(KeyCode.A))
        {
            Vector3 rotation_axis = Vector3.Cross((transform.position - hitInfo.point).normalized, transform.TransformDirection(Vector3.forward));


            transform.RotateAround(hitInfo.point, rotation_axis, 60 * Time.deltaTime);
        }

    }
}
