using UnityEngine;
using System.Collections;

public class AdaptiveMusicController : MonoBehaviour {

	public AudioClip[] TrackClips;
	public bool AutoPlay = false;

	private GameObject[] TrackControllerObjects;
	private TrackController[] TrackControllers;
	
	void Start () {
		InitialiseTracks();
		if(AutoPlay)
			PlayAll();
	}

	void InitialiseTracks() {
		TrackControllerObjects	= new GameObject[TrackClips.Length];
		TrackControllers		= new TrackController[TrackClips.Length];
		for(int i = 0; i < TrackClips.Length; i++) {
			TrackControllerObjects[i] = new GameObject("TrackController");
			TrackControllerObjects[i].transform.parent = transform;
			TrackControllers[i] = (TrackController)TrackControllerObjects[i].AddComponent("TrackController");
			TrackControllers[i].SetTrack(TrackClips[i]);
		}
	}

	public void PlayAll() {
		foreach(TrackController track in TrackControllers)
			track.Play();
	}

	public void PauseAll() {
		foreach(TrackController track in TrackControllers)
			track.Pause();
	}

	public void MuteAll() {
		foreach(TrackController track in TrackControllers)
			track.Mute();
	}

	public void UnMuteAll() {
		foreach(TrackController track in TrackControllers)
			track.UnMute();
	}
	
	public void MuteTrack(int TrackNumber) {
		TrackControllers[TrackNumber].Mute();
	}
	
	public void UnMuteTrack(int TrackNumber) {
		TrackControllers[TrackNumber].UnMute();
	}

	public void SetVolumeOfTrack(int TrackNumber, float Volume) {
		if(TrackNumber < 0 || TrackNumber >= TrackControllers.Length)
			throw new UnityException("There is no track assigned with number "+TrackNumber+".");
		TrackControllers[TrackNumber].SetVolume(Volume);
	}
}

