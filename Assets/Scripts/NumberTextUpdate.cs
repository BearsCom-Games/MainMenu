using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NumberTextUpdate : MonoBehaviour
{
    public int Number;

    public void UpdateNumber(int number)
    {
        Number = number;

        if(this.transform.GetChild(0).TryGetComponent<TextMeshProUGUI>(out var text))
        {
            text.text = "" + number;
        }

        if(number <= 0)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            this.gameObject.SetActive(true);
        }
    }
}
