using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FinishBox : MonoBehaviour
{
    public UIMenu uiMenu;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            uiMenu.OnFinishTutorial();
        }
    }
}
