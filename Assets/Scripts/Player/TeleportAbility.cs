using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportAbility : MonoBehaviour
{
    // 瞬移键
    public KeyCode teleportKey = KeyCode.E;

    // 最大瞬移距离
    public float maxTeleportDistance = 10f;
    public float maxGroundJudgmentDistance = 50f;

    // 瞬移位置
    private Vector3 teleportPos;

    // 状态
    private bool isJudgment;

    // 组件
    public GameObject indicator;
    public LayerMask groundLayer = ~0;
    private Camera mainCamera;


    void Start()
    {
        mainCamera = Camera.main;
    }


    void Update()
    {
        TeleportJudgment();
        PositionCalculation();
    }

    // 瞬移预览
    void TeleportJudgment()
    {
        if(Input.GetKeyDown(teleportKey))
        {
            if(isJudgment == false)
            {
                isJudgment = true;
                indicator.SetActive(true);
            }
            else
            {
                isJudgment = false;
                indicator.SetActive(false);
            }
        }
    }

    // 位置计算
    void PositionCalculation()
    {
        if(isJudgment == true)
        {
            Vector3 forwardPos = mainCamera.transform.position + mainCamera.transform.forward * maxTeleportDistance;

            if(Physics.Raycast(forwardPos,Vector3.down,out RaycastHit hit,maxGroundJudgmentDistance,groundLayer))
            {
                teleportPos = hit.point;
            }

            indicator.transform.position = teleportPos + Vector3.up * 0.05f;
        }

    }
}
