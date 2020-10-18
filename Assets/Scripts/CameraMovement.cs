using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public Transform target;
    public float smoothing;
    public Vector2 maxPosition;
    public Vector2 minPosition;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(target.position.x , target.position.y , transform.position.z);
    }

    // Update is called once per frame
    // Makes camera fire after Player
    // LateUpdate
    void FixedUpdate()
    {
       // if camera position is not the target position then move towards it 
       if (transform.position != target.position)
       {
           //syntax error?
           Vector3 targetPosition = new Vector3 (target.position.x , target.position.y , transform.position.z);
           
           targetPosition.x = Mathf.Clamp(targetPosition.x, minPosition.x, maxPosition.x);

           targetPosition.y = Mathf.Clamp(targetPosition.y, minPosition.y, maxPosition.y); 

           // this takes 3 arruguments
           // position currently at "transform.psoition
           // position we want to go to "target.position"
           // amount we want to cover "smoothing"
           transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing);
       } 
    }
}
