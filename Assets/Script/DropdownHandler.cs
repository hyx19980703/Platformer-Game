using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropdownHandler : MonoBehaviour
{
    public void OnDropdownChanged(int index)
    {
        Dropdown dropdown = GetComponent<Dropdown>();
        if (dropdown != null)
        {
            string selectText = dropdown.options[index].text;
            Debug.Log("选中项" + selectText);
        }
   }
}
