using System;
using UnityEngine;

public class AlarmZone : MonoBehaviour
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
