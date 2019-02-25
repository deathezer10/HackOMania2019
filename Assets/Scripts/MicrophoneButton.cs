using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Photon.Pun;
using ExitGames.Client.Photon;

public class MicrophoneButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    private Sprite m_DefaultSprite, m_OnPressedSprite;

    private AudioClip m_RecordedAudio;

    private int m_MaxMicrophoneFrequency;

    public List<KeyValuePair<AudioClip, byte[]>> m_VoiceList = new List<KeyValuePair<AudioClip, byte[]>>();

    public void OnPointerDown(PointerEventData eventData)
    {
        GetComponent<Image>().sprite = m_OnPressedSprite;
        m_RecordedAudio = Microphone.Start("", false, 10, m_MaxMicrophoneFrequency);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        transform.GetChild(0).gameObject.SetActive(true);

        // Start clip creation
        int micDuration = Microphone.GetPosition("");
        Microphone.End("");
        float[] samples = new float[m_RecordedAudio.samples];
        m_RecordedAudio.GetData(samples, 0);
        float[] clipSamples = new float[micDuration];

        if (clipSamples.Length == 0)
        {
            ResetSprite();
            transform.GetChild(0).gameObject.SetActive(false);
            return;
        }

        System.Array.Copy(samples, clipSamples, clipSamples.Length - 1);
        var clip = AudioClip.Create("playRecordClip", clipSamples.Length, 1, m_MaxMicrophoneFrequency, false);
        clip.SetData(clipSamples, 0);
        // End clip creation

        var bytes = WavUtility.FromAudioClip(clip);
        m_VoiceList.Add(new KeyValuePair<AudioClip, byte[]>(clip, bytes)); // Add voice data to collection for later use

        var role = FindObjectOfType<PhotonGameplay>().player_Role;
        PhotonNetwork.RaiseEvent(((role == PhotonGameplay.PlayerRole.Support) ? (byte)EventCodes.AudioSupportToDiffuse : (byte)EventCodes.AudioDiffuseToSupport),
        bytes, Photon.Realtime.RaiseEventOptions.Default, SendOptions.SendReliable);
    }

    void Start()
    {
        m_DefaultSprite = GetComponent<Image>().sprite;
        Microphone.GetDeviceCaps("", out var minFreq, out m_MaxMicrophoneFrequency);

        if (m_MaxMicrophoneFrequency > 16000)
            m_MaxMicrophoneFrequency = 16000;
    }

    public void ResetSprite()
    {
        GetComponent<Image>().sprite = m_DefaultSprite;
    }

}
