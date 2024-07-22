using System;
using UnityEngine;

public class Zone : MonoBehaviour
{
    public event Action OnZoneEntered;
    public event Action OnZoneExited;

    private void OnTriggerEnter(Collider other)
    {
        OnZoneEntered();
    }

    private void OnTriggerExit(Collider other)
    {
        OnZoneExited();
    }
}
