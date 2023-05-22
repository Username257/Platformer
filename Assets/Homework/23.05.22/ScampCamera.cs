using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScampCamera : MonoBehaviour
{
    public GameObject cam;
    public GameObject targetToFollow;

    private void Start()
    {
        cam.transform.position = new Vector3 (targetToFollow.transform.position.x, targetToFollow.transform.position.y, -10);
    }
    private void Update()
    {
        cam.transform.position = new Vector3(targetToFollow.transform.position.x, targetToFollow.transform.position.y, -10);
    }
}
