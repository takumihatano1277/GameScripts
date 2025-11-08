using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

/// <summary>
/// ビデオ表示透過
/// </summary>
[RequireComponent(typeof(RawImage), typeof(VideoPlayer), typeof(AudioSource))]
public class VideoPlayerOnUGUI : MonoBehaviour
{
    private RawImage image;
    private VideoPlayer player;
    void Awake()
    {
        image = GetComponent<RawImage>();
        player = GetComponent<VideoPlayer>();
        var source = GetComponent<AudioSource>();
        player.EnableAudioTrack(0, true);
        player.SetTargetAudioSource(0, source);
    }
    void Update()
    {
        if (player.isPrepared)
        {
            image.texture = player.texture;
        }
    }
}