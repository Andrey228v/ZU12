using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Alarm : MonoBehaviour
{
    [SerializeField] private Zone _zone;

    private AudioSource _audioSource;
    private bool _isActivated = true;
    private float _currentVolume = 0;
    private float _minVolume = 0;
    private float _maxVolume = 1;
    private float _step = 0.2f;

    private Coroutine _volumeChangerCoroutine;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        _zone.OnZoneEntered += Activeted;
        _zone.OnZoneExited += Diactivated;
    }

    private void OnDisable()
    {
        _zone.OnZoneEntered -= Activeted;
        _zone.OnZoneExited -= Diactivated;
    }

    private void Activeted()
    {
        StopCurrentCoroutine(_volumeChangerCoroutine);

        _audioSource.volume = _currentVolume;
        _audioSource.Play();

        _volumeChangerCoroutine = StartCoroutine(VolumeChanger(_maxVolume, _step));
    }

    private void Diactivated()
    {
        StopCurrentCoroutine(_volumeChangerCoroutine);

        _volumeChangerCoroutine = StartCoroutine(VolumeChanger(_minVolume, _step));
    }

    private void StopCurrentCoroutine(Coroutine coroutine)
    {
        if(coroutine != null )
        {
            StopCoroutine(coroutine);
        }
    }

    private IEnumerator VolumeChanger(float comingVolume, float step)
    {   
        while (_isActivated)
        {
            _audioSource.volume = Mathf.MoveTowards(_currentVolume, comingVolume, step * Time.deltaTime);
            _currentVolume = _audioSource.volume;

            if (_currentVolume <= _minVolume)
            {
                _audioSource.Stop();

                yield break;
            }

            yield return _currentVolume;
        }
    }
}
