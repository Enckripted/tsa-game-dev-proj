using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
	public static SoundManager instance;

	private AudioSource audioSource;

	public void playSound(AudioClip audio)
	{
		audioSource.PlayOneShot(audio, 1f);
	}

	void Awake()
	{
		instance = this;
		audioSource = GetComponent<AudioSource>();
		audioSource.spatialBlend = 0;
	}
}