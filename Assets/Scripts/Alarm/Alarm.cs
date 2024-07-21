using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Alarm : MonoBehaviour
{
    [SerializeField] private AlarmDevice _device;
    [SerializeField] private AlarmZone _zone;

    private AudioSource _audioSource;
    private bool _isActivated = false;
    private float _currentVolume = 0;
    private float _minVolume = 0;
    private float _maxVolume = 1;


    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        _zone.OnZoneEntered += AlarmActiveted;
        _zone.OnZoneExited += AlarmDiactivated;
    }

    private void OnDisable()
    {
        _zone.OnZoneEntered -= AlarmActiveted;
        _zone.OnZoneExited -= AlarmDiactivated;
    }

    private void AlarmActiveted()
    {
        _isActivated = true;
        _audioSource.volume = _currentVolume;
        _audioSource.Play();

        StartCoroutine(VolumeChanger(_currentVolume, _maxVolume));
    }

    private void AlarmDiactivated()
    {
        _isActivated = false;
    }

    private IEnumerator VolumeChanger(float currentVolume, float comingVolume)
    {   
        while (_isActivated)
        {
            _audioSource.volume = Mathf.MoveTowards(currentVolume, comingVolume, 0.2f * Time.deltaTime);
            currentVolume = _audioSource.volume;

            yield return currentVolume;
        }

        while (_isActivated == false)
        {
            _audioSource.volume = Mathf.MoveTowards(currentVolume, 0, 0.2f * Time.deltaTime);
            currentVolume = _audioSource.volume;

            if(currentVolume <= _minVolume)
            {
                _audioSource.Stop();

                yield break;
            }

            yield return currentVolume;
        }
    }
}
