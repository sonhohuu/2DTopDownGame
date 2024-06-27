using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealthUI : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject); // Persist this GameObject across scene loads
    }
}
