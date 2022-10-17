using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CinemachineCam : MonoBehaviour
{
    public static CinemachineCam instance;
    CinemachineVirtualCamera virtualCam;
    CinemachineTransposer transposer;
    public CinemachineBasicMultiChannelPerlin noise;
    bool up;
    float startTransposerX,startTransposerY;
    public GameObject rotatingProp;
   public float minY, maxY, ySpeed, maxX, xSpeed;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        virtualCam = GetComponent<CinemachineVirtualCamera>();
        transposer = virtualCam.GetCinemachineComponent<CinemachineTransposer>();
        noise= virtualCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        startTransposerX = transposer.m_FollowOffset.x;
        startTransposerY = transposer.m_FollowOffset.y;
    }

    
    void FixedUpdate()
    {
        transposer.m_FollowOffset = new Vector3(startTransposerX, startTransposerY, transposer.m_FollowOffset.z);
        if (GameControl.instance.gunBool)
        {
            if (!rotatingProp.activeSelf)
            {
              //  rotatingProp.SetActive(true);
            }
            
            if (!up)
            {
                startTransposerX -= xSpeed;
                if (startTransposerX < -maxX)
                {
                    up = true;
                }
            }
            if (up)
            {
                startTransposerX += xSpeed;
                if (startTransposerX > maxX)
                {
                    up = false;
                }
            }
            if (startTransposerY < maxY)
            {
                startTransposerY += ySpeed;
                if (startTransposerY > maxY)
                {
                    startTransposerY = maxY;
                }
            }
        }
        if (GameControl.instance.ropeBool)
        {
            if (startTransposerX < 0)
            {
                startTransposerX += xSpeed;
                if (startTransposerX > 0f)
                {
                    startTransposerX = 0;
                }
            }
            if (startTransposerX > 0)
            {
                startTransposerX -= xSpeed;
                if (startTransposerX < 0f)
                {
                    startTransposerX = 0;
                }
            }
            if (startTransposerY > minY)
            {
                startTransposerY -= ySpeed;
                if (startTransposerY < minY)
                {
                    rotatingProp.SetActive(false);
                    startTransposerY = minY;
                }
            }
        }





    }
}
