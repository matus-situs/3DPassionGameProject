using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    [SerializeField]
    Texture2D texture;
    // Start is called before the first frame update
    void Start()
    {
        texture.filterMode = FilterMode.Point;
    }
}
