using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnBotton : MonoBehaviour
{


   public void ReturnMain()
    {
        SceneManager.LoadScene("Mnue");
    }
}
