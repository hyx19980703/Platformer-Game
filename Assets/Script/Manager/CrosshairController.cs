using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairController : MonoBehaviour
{
    [Tooltip("׼���ƶ���ƽ��ϵ��")]
    [Range(0f, 1f)]
    public float smoothSpeed = 0.125f;

    [Tooltip("׼���������Z�����")]
    public float zOffset = 10f;

    [Tooltip("׼�ǷŴ�ϵ��")]
    public float scale = 6f;


    // �洢���������
    private Camera mainCamera;
    // ׼�ǵ�Ŀ��λ��
    private Vector3 desiredPosition;

    private void Start()
    {
        transform.localScale = new Vector3(scale, scale, scale);
        // ����Ĭ�������
        //Cursor.visible = false;
        // ��ȡ���������
        mainCamera = Camera.main;
    }

    private void Update()
    {
        // �������Ļ����ת��Ϊ��������
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = zOffset;
        desiredPosition = mainCamera.ScreenToWorldPoint(mousePosition);

        // ƽ���ƶ�׼��
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

    // ��ʾ������׼�ǵķ���
    public void SetCrosshairVisible(bool visible)
    {
        gameObject.SetActive(visible);
        Cursor.visible = !visible;
    }
}
