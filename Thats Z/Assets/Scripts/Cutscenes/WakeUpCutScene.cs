using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class WakeUpCutScene : MonoBehaviour
{

    public GameObject Player;
    public GameObject PlayerCam;
    public GameObject AnimPlayerBody;
    public PlayableDirector playerAnim;
    public SubtitlesSys Sub;
    public GameObject PlaayerUI;
    
    
    // Start is called before the first frame update
    void Start()
    {
        playerAnim.Play();
        PlayerCam.GetComponent<Camera>().fieldOfView = PlayerPrefs.GetInt("FOV");
        Sub.SetSubtitle("What the fuck ... I'm still ALIVE???",5f);
        StartCoroutine("Anim");
    }

    IEnumerator Anim()
    {
        yield return new WaitForSeconds(11f);
        AnimPlayerBody.SetActive(false);
        Player.SetActive(true);
        PlaayerUI.SetActive(true);
        Sub.SetSubtitle("I must survive ...",5f);
        yield return new WaitForSeconds(5f);
        Destroy(this.gameObject);
    }
    
}
