using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioPanelPlayButton : MonoBehaviour
{

    private AudioSource m_CurrentPlayingClip;
    private AudioClip m_AssignedClip;

    [SerializeField]
    private Sprite m_PlaySprite, m_StopSprite;

    private Image m_CurrentImage;

    private void Start()
    {
        m_CurrentImage = GetComponent<Image>();
    }

    public void AssignClip(AudioClip clip)
    {
        m_AssignedClip = clip;
    }

    public void PlayAudio()
    {
        m_CurrentImage.sprite = m_StopSprite;

        AudioManager.Instance.Play2D(m_AssignedClip, AudioManager.AudioType.Additive, null, 0, null, () =>
         {
         });
    }

    public void StopAudio()
    {
        m_CurrentImage.sprite = m_PlaySprite;

    }

    public void ToggleAudio()
    {
        if (m_CurrentPlayingClip == null || m_CurrentPlayingClip.isPlaying)
        {
            PlayAudio();
        }
        else
        {
            StopAudio();
        }
    }

}
