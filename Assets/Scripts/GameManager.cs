using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // https://www.youtube.com/playlist?list=PL4vbr3u7UKWp0iM1WIfRjCDTI03u43Zfu
    // https://www.youtube.com/playlist?list=PLiyfvmtjWC_X6e0EYLPczO9tNCkm2dzkm
    // https://www.youtube.com/playlist?list=PLS6sInD7ThM3JnoOsur24_3h3dvPpfNA9
    // https://www.youtube.com/playlist?list=PLM83Z6G5iM3k48356VU6e-oXWl_uwwq4F

    // https://learn.unity.com/tutorial/2d-roguelike-setup-and-assets?uv=5.x&projectId=5c514a00edbc2a0020694718
    // https://stdpub.com/unity3d/sozdanie-pikselnoj-rpg-s-pomoshhyu-unity-3d?ysclid=lb5870f5ny157448685  
    public static GameManager instance;
    private Hud hud;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}