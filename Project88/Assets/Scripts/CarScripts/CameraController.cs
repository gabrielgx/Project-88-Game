using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject objToFollow;
    public Vector3 offSet;
    public float followspeed = 50;
    public float lookSpeed = 10;

    public void LookAtTarget()
    {
        Vector3 _lookPosition = objToFollow.transform.position - transform.position;
        Quaternion _rot = Quaternion.LookRotation(_lookPosition, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, _rot, lookSpeed * Time.deltaTime);
    }

    public void MoveToTarget()
    {
        Vector3 _targetPos = objToFollow.transform.position + objToFollow.transform.forward * offSet.z + objToFollow.transform.right * offSet.x + objToFollow.transform.up * offSet.y;
        transform.position = Vector3.Lerp(transform.position, _targetPos, followspeed * Time.deltaTime);

    }

    private void FixedUpdate()
    {
        LookAtTarget();
        MoveToTarget();
    }
}
