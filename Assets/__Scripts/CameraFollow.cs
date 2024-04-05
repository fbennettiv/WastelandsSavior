using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float xMargin = 1;
    public float yMargin = 1;
    public float xSmooth = 4;
    public float ySmooth = 4;
    public Vector3 maxXAndY;
    public Vector3 minXAndY;
    public Transform player;

    bool CheckXMargin()
    {
        return Mathf.Abs(transform.position.x - player.position.x) > xMargin;
    }

    bool CheckZMargin()
    {
        return Mathf.Abs(transform.position.z - player.position.z) > yMargin;
    }

    void FixedUpdate()
    {
        TrackPlayer();
    }

    void TrackPlayer()
    {
        float targetX = transform.position.x;
        float targetZ = transform.position.z;

        if (CheckXMargin())
        {
            targetX = Mathf.Lerp(transform.position.x,
                player.position.x,
                xSmooth * Time.deltaTime);
        }

        if (CheckZMargin())
        {
            targetZ = Mathf.Lerp(transform.position.z,
                player.position.z,
                ySmooth * Time.deltaTime);
        }

        targetX = Mathf.Clamp(targetX, minXAndY.x, maxXAndY.x);
        targetZ = Mathf.Clamp(targetZ, minXAndY.z, maxXAndY.z);

        transform.position = new Vector3(targetX, transform.position.y,
            targetZ);
    }
}
