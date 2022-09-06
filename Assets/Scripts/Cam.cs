using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    public Transform giant;
    [SerializeField] float zIndex;
    private void Start()
    {
        Application.targetFrameRate = 60;
    }
    void LateUpdate()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, giant.transform.position.z + zIndex);
    }
}
