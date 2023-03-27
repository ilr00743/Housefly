using UnityEngine;

public class Screamer : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    private AudioSource _audioSource;
    private bool _isVibrating;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (_isVibrating)
        {
            Vibration.Vibrate(3000, 10);
        }
    }

    public void Enable()
    {
        _canvas.enabled = false;
        gameObject.SetActive(true);
        _audioSource.Play();
        _isVibrating = true;
    }
}
