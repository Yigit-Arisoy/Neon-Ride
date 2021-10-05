using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class CarRotation : MonoBehaviour
{


    public float pipeRadius;
    public float rotationSpeed;
    public float carHeight;
    public float stickingPower;
    public GameObject rotatingPoint;
    public gameSettings gameSettings;
    public float driftSpeed;
    public float lastClickTime, timeSinceLastClick;
    public float doubleClickTime;
    public float driftTime;
    void FixedUpdate()
    {
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
        Touch touch = Input.GetTouch(0);       
        

        if (Input.touchCount > 0)
        {
            timeSinceLastClick = Time.time - lastClickTime;
            if (touch.position.x < Screen.width / 2)
            {
                transform.RotateAround(rotatingPoint.transform.position, -Vector3.right, rotationSpeed * (-1) * Time.deltaTime);

                if (timeSinceLastClick < doubleClickTime)
                    StartCoroutine(doubleTapDrift(driftSpeed, driftTime));
            }
            
            if (touch.position.x > Screen.width / 2)
            {
                transform.RotateAround(rotatingPoint.transform.position, -Vector3.right, rotationSpeed * Time.deltaTime);
                if (timeSinceLastClick < doubleClickTime)
                    StartCoroutine(doubleTapDrift(-driftSpeed, driftTime));
            }

            if(touch.phase == TouchPhase.Ended)
                lastClickTime = Time.time;
        }
    }
    IEnumerator doubleTapDrift(float driftSpeed, float driftTime)
    {
        Debug.Log("Drifting");
        float duration = Time.time + driftTime;
        while(Time.time < duration)
        {
            transform.RotateAround(rotatingPoint.transform.position, -Vector3.right, -driftSpeed * Time.deltaTime);
            yield return null;
        }
    }

   

}
