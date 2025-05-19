using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePositon : MonoBehaviour
{
   [SerializeField] private Camera mainCamera;

    public static MousePositon instance;

   public Vector3 mousePos;

    void Start()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Update()


    {
         mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
         mousePos.z = 0;


    }
}
