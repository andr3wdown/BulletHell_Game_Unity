using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Andr3wDown.Math;

public class HubWorldPlayerController : MonoBehaviour
{
    public float speed;
    public float cameraSmoothing = 5f;
    public float zOffset = -10f;
    public Transform pivot;
    [HideInInspector]
    public bool isActive = true;


    public Transform cameraMax, cameraMin;
    private void FixedUpdate()
    {
        if (isActive)
        {
            transform.rotation = MathOperations.LookAt2D(Camera.main.ScreenToWorldPoint(Input.mousePosition), pivot.position, -90);
            if (Input.GetMouseButton(0))
            {
                Vector3 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                dir.z = 0;
                transform.position = Vector3.MoveTowards(transform.position, dir, speed * Time.deltaTime);
            }
        }
        
        Vector3 camVector = transform.position;
        camVector.z = zOffset;
        Vector3 camDir = Vector3.Lerp(Camera.main.transform.position, camVector, cameraSmoothing * Time.deltaTime);//Vector3.MoveTowards(Camera.main.transform.position, camVector, (speed - 0.1f) * Time.deltaTime);
        camDir = CameraClamp(camDir);

        Camera.main.transform.position = camDir;
    }

    Vector3 CameraClamp(Vector3 vector)
    {
        float screenVertHalf = Camera.main.orthographicSize;
        float screenHoriHalf = ((float)Screen.width / (float)Screen.height) * screenVertHalf ;
        
        vector.x = Mathf.Clamp(vector.x, cameraMin.position.x + screenHoriHalf, cameraMax.position.x - screenHoriHalf);
        vector.y = Mathf.Clamp(vector.y, cameraMin.position.y + screenVertHalf, cameraMax.position.y - screenVertHalf);
        vector.z = zOffset;
        return vector;

    }
}
