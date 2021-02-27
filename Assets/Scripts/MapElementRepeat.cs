using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapElementRepeat : MonoBehaviour
{
    [SerializeField] float speedMultiplier;
    RawImage rawImage;
    Rect uvRect;
    private void Start()
    {
        rawImage = GetComponent<RawImage>();
        uvRect = rawImage.uvRect;
        
    }

    private void Update()
    {
        uvRect.y -= speedMultiplier * Time.deltaTime;
        rawImage.uvRect = uvRect;

        if (uvRect.y <= -360)
            uvRect.y = 0;
    }
}
