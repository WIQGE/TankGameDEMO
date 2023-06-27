using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    //�����Ŀ��
    public Transform targetPlayer;
    public float H = 20;

    private Vector3 pos;

    //ע��Ҫд��LateUpdate�������Ⱦ��������
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
