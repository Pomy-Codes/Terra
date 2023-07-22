using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FMODEvents : MonoBehaviour
{
    [field: Header("Music")]
    [field: SerializeField] public EventReference GamePlayMusic { get; set; }
    [field: SerializeField] public EventReference MenuMusic { get; set; }


    [field: Header("SFX")]
    [field: SerializeField] public EventReference Jump { get; private set; }
    [field: SerializeField] public EventReference Attack { get; private set; }
    [field: SerializeField] public EventReference Hit { get; private set; }
    [field: SerializeField] public EventReference Hover { get; private set; }
    public static FMODEvents instance { get; private set; }
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of FMODEvents found!");
        }
        instance = this;
    }
}
