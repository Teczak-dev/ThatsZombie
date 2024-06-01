using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubtitlesSys : MonoBehaviour
{
    public GameObject TextPanel;
    public Text text;
    private float HowLong;

    public void SetSubtitle(string tekst, float HowL)
    {
        HowLong = HowL;
        
        TextPanel.SetActive(true);
        text.text = tekst;
        StartCoroutine("Hide");
    }

    IEnumerator Hide()
    {
        yield return new WaitForSeconds(HowLong);
        text.text = "";
        TextPanel.SetActive(false);
    }
    
    
}
