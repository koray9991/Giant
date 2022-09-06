using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CinemachineCam : MonoBehaviour
{
    CinemachineVirtualCamera virtualCam;
    CinemachineTransposer transposer;
    bool up;
    float startTransposerX;
    void Start()
    {
        virtualCam = GetComponent<CinemachineVirtualCamera>();
        transposer = virtualCam.GetCinemachineComponent<CinemachineTransposer>();
        startTransposerX = transposer.m_FollowOffset.x;
    }

    
    void FixedUpdate()
    {
        transposer.m_FollowOffset = new Vector3(startTransposerX, transposer.m_FollowOffset.y, transposer.m_FollowOffset.z);
        if (!up)
        {
            startTransposerX -= 0.1f;
            if (startTransposerX < -10f)
            {
                up = true;
            }
        }
        if (up)
        {
            startTransposerX += 0.1f;
            if (startTransposerX > 10f)
            {
                up = false;
            }
        }
    }
}
