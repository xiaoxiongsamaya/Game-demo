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

    // 层级
    public LayerMask groundLayer = ~0;
    // 组件
    public GameObject indicator;

    private Rigidbody indicatorRigidbody;
    private Collider indicatorCollider;
    private Camera mainCamera;


    void Start()
    {
        mainCamera = Camera.main;

        indicatorCollider = indicator.GetComponent<Collider>();

        indicatorRigidbody = indicator.GetComponent<Rigidbody>();
        if(indicatorRigidbody == null)
        {
            indicatorRigidbody = indicator.AddComponent<Rigidbody>();
        }
        indicatorRigidbody.isKinematic = false;
        indicatorRigidbody.useGravity = false;
        indicatorRigidbody.constraints = RigidbodyConstraints.FreezeRotation;
    }


    void Update()
    {
        HandleInput();
        PositionCalculation();
    }

    // 瞬移按键
    void HandleInput()
    {
        if(Input.GetKeyDown(teleportKey))
        {
            isJudgment = !isJudgment;
            indicator.SetActive(isJudgment);
        }
    }

    // 位置计算
    void PositionCalculation()
    {
        if(!isJudgment) return;

        // 判空检查
        if (indicator == null)
        {
            Debug.LogError("indicator 为空！");
            return;
        }
        if (indicatorCollider == null)
        {
            Debug.LogError("indicatorCollider 为空！");
            return;
        }
        if (indicatorRigidbody == null)
        {
            Debug.LogError("indicatorRigidbody 为空！");
            return;
        }

        Vector3 forwardPos = mainCamera.transform.position + mainCamera.transform.forward * maxTeleportDistance;

        indicatorCollider.enabled = false;
        if(Physics.Raycast(forwardPos,Vector3.down,out RaycastHit hit,maxGroundJudgmentDistance,groundLayer))
        {
            teleportPos = hit.point + Vector3.up * 0.05f;
        }
        indicatorCollider.enabled = true;

        indicatorRigidbody.MovePosition(teleportPos);
    }
}
