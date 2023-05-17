using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerCreator : MonoBehaviour
{
    private static ManagerCreator instance;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
