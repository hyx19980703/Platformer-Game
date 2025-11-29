using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NewBeeGuild : MonoBehaviour
{

   [SerializeField] private TextMeshProUGUI targetText;

    [SerializeField] private float showDuration;
    // Start is called before the first frame update
    void Start()
    {
       //targetText= GetComponentInChildren<TextMeshProUGUI>();
        
         StartCoroutine(ShowTimeContorl(showDuration));


    }

    // Update is called once per frame

    IEnumerator ShowTimeContorl(float _showDuration)
    {
        yield return new WaitForSeconds(_showDuration);
        targetText.gameObject.SetActive(false);
    }

    public void OnMouseEnter ()
    {
        if(targetText!=null)
        targetText.gameObject.SetActive(true);
        Debug.Log("鼠标进入");

    }


        public void OnMouseExit ()
    {
        if(targetText!=null)
        targetText.gameObject.SetActive(false);
         Debug.Log("鼠标离开");
    }
}
