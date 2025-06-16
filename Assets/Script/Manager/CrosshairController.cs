using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairController : MonoBehaviour
{
    [Tooltip("准星移动的平滑系数")]
    [Range(0f, 1f)]
    public float smoothSpeed = 0.125f;

    [Tooltip("准星与相机的Z轴距离")]
    public float zOffset = 10f;

    [Tooltip("准星放大系数")]
    public float scale = 6f;


    // 存储主相机引用
    private Camera mainCamera;
    // 准星的目标位置
    private Vector3 desiredPosition;

    private void Start()
    {
        transform.localScale = new Vector3(scale, scale, scale);
        // 隐藏默认鼠标光标
        //Cursor.visible = false;
        // 获取主相机引用
        mainCamera = Camera.main;
    }

    private void Update()
    {
        // 将鼠标屏幕坐标转换为世界坐标
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = zOffset;
        desiredPosition = mainCamera.ScreenToWorldPoint(mousePosition);

        // 平滑移动准星
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Cursor.visible = false;
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Cursor.visible = true;
        }
    }

    // 显示或隐藏准星的方法
    public void SetCrosshairVisible(bool visible)
    {
        gameObject.SetActive(visible);
        Cursor.visible = !visible;
    }
}
