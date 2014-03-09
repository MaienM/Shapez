using UnityEngine;
using System.Collections;

public class TrackController : MonoBehaviour {
	
	private AudioSource TrackSource;
	private GameObject AudioSourceObject;
	
	public void SetTrack(AudioClip TrackClip) {
		AudioSourceObject = new GameObject("MusicTrackObject");
		TrackSource = (AudioSource)AudioSourceObject.AddComponent("AudioSource");
		TrackSource.transform.parent = transform;
		TrackSource.transform.position = Camera.main.transform.position;
		TrackSource.clip = TrackClip;
		TrackSource.playOnAwake = false;
		TrackSource.loop = true;
	}
	
	public void Play() {
		TrackSource.Play();
	}
	
	public void Pause() {
		TrackSource.Pause();
	}
	
	public void Mute() {
		TrackSource.mute = true;
	}
	
	public void UnMute() {
		TrackSource.mute = false;
	}
	
	public void SetVolume(float Volume, float FadeDuration = 5f) {
		if(Volume < 0f)
			Volume = 0f;
		else if(Volume > 1f)
			Volume = 1f;
		//float absoluteVolumeDifference = Mathf.Abs(Volume - TrackSource.volume);
		StopCoroutine("TweenVolume");
		StartCoroutine(TweenVolume(Tweens.InOutQuint, Volume, FadeDuration));
	}
	
	private delegate float Tween( float t, float b, float c, float d );
	
	private IEnumerator TweenVolume(Tween tween, float FinalVolume, float TweenDuration) {
		float CurrentTime = 0f;
		float StartingVolume = TrackSource.volume;
		while(TrackSource.volume != FinalVolume) {
			TrackSource.volume = tween(CurrentTime, StartingVolume, FinalVolume, TweenDuration);
			yield return null;
			CurrentTime += Time.deltaTime;
		}
	}
}
