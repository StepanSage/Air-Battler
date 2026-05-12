using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    public void Awake()
    {
        ServiceLocator.Initialization();
        

    }
}
