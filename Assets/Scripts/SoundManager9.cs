using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager9 : MonoBehaviour
{
    public static SoundManager9 instance { get; private set; }
    public bool soundEnabled { get; private set; }

    [SerializeField] private GameObject musicButton, muteButton;
    [SerializeField] private AudioClip[] audioClips;
    private AudioSource audioSource;

    private void Awake()
    {
        if (instance != null && instance != this) Destroy(gameObject);
        else instance = this;

        audioSource = gameObject.AddComponent<AudioSource>();
        soundEnabled = PlayerPrefs.GetInt("SoundEnabled", 1) == 1;
        if (audioClips.Length > 0) PlaySound(0);
        UpdateSound();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ToggleSound()
    {
        soundEnabled = !soundEnabled;
        PlayerPrefs.SetInt("SoundEnabled", soundEnabled ? 1 : 0);
        UpdateSound();
    }

    public void UpdateSound()
    {
        audioSource.mute = !soundEnabled;
        if (musicButton && muteButton)
        {
            musicButton.SetActive(soundEnabled);
            muteButton.SetActive(!soundEnabled);
        }
    }

    public void PlaySound(int index)
    {
        if (!soundEnabled || index < 0 || index >= audioClips.Length) return;

        if (index == 0)
        {
            audioSource.clip = audioClips[0];
            audioSource.loop = true;
            audioSource.Play();
        }
        else audioSource.PlayOneShot(audioClips[index]);
    }

    public void SoundClick() => PlaySound(1);
    public void SoundMove() => PlaySound(5);
}
