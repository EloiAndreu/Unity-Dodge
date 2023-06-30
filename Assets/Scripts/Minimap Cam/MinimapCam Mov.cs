using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCamMov : MonoBehaviour
{
    float distanceFromPlayer = 5f;

    void Start()
    {
        Vector3 cantonada = new Vector3(5f, 1f, -3.5f);
        int x = GameManager.Instance.xDisparadors;
        int y = GameManager.Instance.yDisparadors;
        Vector3 initialPos = cantonada + new Vector3(0f, y/2f, x/2f);
        transform.position = new Vector3(initialPos.x + distanceFromPlayer, initialPos.y, initialPos.z);
    }
}
