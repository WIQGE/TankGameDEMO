using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    //看向的目标
    public Transform targetPlayer;
    public float H = 20;

    private Vector3 pos;

    //注意要写到LateUpdate里，避免渲染出现问题
    private void LateUpdate()
    {
        if (targetPlayer == null)
            return;
        pos.x = targetPlayer.position.x;
        pos.z = targetPlayer.position.z;
        pos.y = H;
        this.transform.position = pos;
    }
}
