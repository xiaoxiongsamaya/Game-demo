using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // 组件
    private CharacterController characterController;

    // 速度
    [Header("速度")]
    public float Speed = 5f;

    // 鼠标
    [Header("鼠标")]
    public float Senstivity = 5f;
    public float minLookAngle = -90f;
    public float maxLookAngle = 90f;

    private float verticalRotation;


    void Start()
    {
        // 组件
        characterController = GetComponent<CharacterController>();

        // 鼠标锁定隐藏
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // 移动
        Movement();

        // 摄像头
        HandleCamera();
    }

    void Movement()
    {
        // 移动控制
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        // 移动
        Vector3 moveDir = transform.forward * vertical + transform.right * horizontal;
        characterController.Move(moveDir.normalized * Speed * Time.deltaTime);
    }

    void HandleCamera()
    {
        // 鼠标控制
        float mouseX = Input.GetAxis("Mouse X") * Senstivity;
        float mouseY = Input.GetAxis("Mouse Y") * Senstivity;

        // 水平旋转
        transform.Rotate(Vector3.up * mouseX);

        // 垂直旋转
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, minLookAngle, maxLookAngle);
        Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
    }

}
