using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  public static GameManager Instance;
   int currentScore;

    void Awake()
    {
         if(Instance ==null) Instance = this;
          else Destroy(gameObject);


    }

    public void AddSource(int value)
    {
        currentScore += value;
        Debug.Log("加分了！");
    }

}
