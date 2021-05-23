using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frogs : MonoBehaviour
{
    public static Frogs instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);

        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
