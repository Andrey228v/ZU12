using System;
using UnityEngine;

public class Zone : MonoBehaviour
{
    public event Action EnteredZone;
    public event Action LeftZone;

    private void OnTriggerEnter(Collider other)
    {
        EnteredZone?.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        LeftZone?.Invoke();
    }
}
