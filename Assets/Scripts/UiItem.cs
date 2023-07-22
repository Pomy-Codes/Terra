using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiItem : MonoBehaviour
{
    public void HighLightedEffect()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.Hover, transform.position);
    }
}
