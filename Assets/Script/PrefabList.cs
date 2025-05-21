using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PrefabList : MonoBehaviour
{
   [SerializeField] private GameObject preFab;
    private List<GameObject> listOfpreFab = new List<GameObject>();

    public static PrefabList prefabList;

    void Awake()
    {
        if (prefabList == null)
            prefabList = this;
        else Destroy(gameObject);
    }

    public GameObject getInstance()
    {
        GameObject instance = Instantiate(preFab);
        listOfpreFab.Add(instance);
        return instance;
    }

    public void RetrunObject(GameObject _instance)
    {
        listOfpreFab.Remove(_instance);
        Destroy(_instance,0.1f);

    }

    public int GetInstanceCount()
    {
        return listOfpreFab.Count;
    }
}
