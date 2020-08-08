#region File Description
//-----------------------------------------------------------------------------
// AudioSongFader.cs
//
// Copyright (C) Allegro Interactive Games. All rights reserved.
//
// DEVREF: 
// http://clearcutgames.net/home/?p=437
// http://wiki.unity3d.com/index.php/Fading_Audio_Source
//-----------------------------------------------------------------------------
#endregion

#region using
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

using Game.Enums;
#endregion

[RequireComponent(typeof(AudioSource))]
public class AudioSongFader : MonoBehaviour 
{
	
#region enums
    public enum FadeState
    {
        None,
        FadingOut,
        FadingIn
    }
#endregion
	
#region fields
	bool isActive;

	    /// <summary>
    ///   Actual audio source.
    /// </summary>
    private AudioSource audioSource;

    /// <summary>
    ///   Whether the audio source is currently fading, in or out.
    /// </summary>
    private FadeState fadeState = FadeState.None;

    /// <summary>
    ///   Next clip to fade to.
    /// </summary>
    private AudioClip nextClip;

    /// <summary>
    ///   Whether to loop the next clip.
    /// </summary>
    private bool nextClipLoop;

    /// <summary>
    ///   Target volume to fade the next clip to.
    /// </summary>
    private float nextClipVolume;

    GameObject activeEventManager;
    GameObject activeAppManager;
#endregion
	
#region properties
    public AudioClip activeSong;
	public AudioClip[] bgTrackList;
     
    // Static singleton property
    public static AudioSongFader Instance { get; private set; }

     // Flags
    public bool onAwakeFadeIn;
    public bool onLevelStartFadeOut;

    /// <summary>
    ///   Volume to end the previous clip at.
    /// </summary>
    public float FadeOutThreshold = 0.05f;

    /// <summary>
    ///   Volume change per second when fading.
    /// </summary>
    public float FadeSpeed = 0.05f;

    public float durationFadeOut = 0.20f;
    public bool stopWhenFadeOutDone = false;

    public float cvBackgroundSong;

#endregion

#region properties_readonly
    /// <summary>
    ///   Current clip of the audio source.
    /// </summary>
    public AudioClip Clip
    {
        get
        {
            return this.audioSource.clip;
        }
    }

    /// <summary>
    ///   Whether the audio source is currently playing a clip.
    /// </summary>
    public bool IsPlaying
    {
        get
        {
            return this.audioSource.isPlaying;
        }
    }

    /// <summary>
    ///   Whether the audio source is looping the current clip.
    /// </summary>
    public bool Loop
    {
        get
        {
            return this.audioSource.loop;
        }
    }

    /// <summary>
    ///   Current volume of the audio source.
    /// </summary>
    public float Volume
    {
        get
        {
            return this.audioSource.volume;
        }
    }

#endregion
	
#region events
	 void OnEnable() 
    {
        // make event subscriptions
        EventManager.OnLevelStart += LevelStartEvent;
        EventManager.OnLevelStop +=LevelStopEvent;
        EventManager.OnSceneLoadComplete += SceneLoadCompleteEvent; 

        EventManager.OnEndRound += EndRoundEvent;
        EventManager.OnBeginRound += BeginRoundEvent;
    }

    void OnDisable()
    {
        // remove event subscriptions
        EventManager.OnLevelStart -= LevelStartEvent;
        EventManager.OnLevelStop -=LevelStopEvent;
        EventManager.OnSceneLoadComplete -= SceneLoadCompleteEvent;

        EventManager.OnEndRound -= EndRoundEvent;
        EventManager.OnBeginRound -= BeginRoundEvent;

    }

    void LevelStartEvent() 
    {
        if ( onLevelStartFadeOut) 
        {
            this.FadeOutCurrentSong();
        }
    }

    void LevelStopEvent() 
    {
    }
    
    void SceneLoadCompleteEvent() 
    {
        this.audioSource.enabled = true;
    }

    void EndRoundEvent(EndRoundInfoEventArgs e)
    {
        this.FadeOutCurrentSong();
    }

    void BeginRoundEvent()
    {   
        
    }

    void OnMouseDownEvent( MouseInfoEventArgs e) 
    {
    }
	
#endregion
		
#region Initialize
	//The Start function is called after all Awake functions on all script instances have been called. 
	void Start() 
	{
        activeEventManager = GameObject.Find("EventManager");
        if ( !activeEventManager ) 
        { 
            EventManager.DebugLog("Start()", "unable to find 'EventManager' reporting object: " + transform.name);
        }

        activeAppManager = GameObject.Find("AppManager");
        if ( !activeAppManager ) 
        { 
            EventManager.DebugLog("Start()", "unable to find 'AppManager' reporting object: " + transform.name);
        }
	}
	
	// Use Awake to set up references between scripts, and use Start to pass any information back and forth.
	void Awake() 
	{
		   // First we check if there are any other instances conflicting
        if(Instance != null && Instance != this)
        {
            // If that is the case, we destroy other instances
            Destroy(gameObject);
        }
 
        // Here we save our singleton instance
        Instance = this;

        AudioAwake();
	}
#endregion
	
#region methods
	
    void FadeInOut(AudioClip clip, float volume, bool loop)
    {

        if (clip == null )
        {
            return;
        }

        this.nextClip = clip;
        this.nextClipVolume = cvBackgroundSong;
        this.nextClipLoop = loop;

        if (this.audioSource.enabled)
        {
            if (this.IsPlaying)
            {
                this.fadeState = FadeState.FadingOut;
            }
            else
            {
                this.FadeToNextClip();
            }
        }
    }

    public void FadeOutCurrentSong()
    {
        FadeOut(activeSong, durationFadeOut, true);
    }

    public void FadeOut(AudioClip clip, float volume, bool loop)
    {
        // is this the right clip
        if ( clip != this.audioSource.clip)
        {
            return;
        }

        if (this.audioSource.enabled)
        {
            if (this.IsPlaying)
            {
                this.fadeState = FadeState.FadingOut;
            }
        }
    }

    /// <summary>
    ///   If the audio source is enabled and playing, fades out the current clip and fades in the specified one, after.
    ///   If the audio source is enabled and not playing, fades in the specified clip immediately.
    ///   If the audio source is not enabled, fades in the specified clip as soon as it gets enabled.
    /// </summary>
    /// <param name="clip">Clip to fade in.</param>
    /// <param name="volume">Volume to fade to.</param>
    /// <param name="loop">Whether to loop the new clip, or not.</param>
    public void Fade(AudioClip clip, float volume, bool loop)
    {
        if (clip == null || clip == this.audioSource.clip)
        {
            return;
        }

        this.nextClip = clip;
        this.nextClipVolume = cvBackgroundSong;
        this.nextClipLoop = loop;

        if (this.audioSource.enabled)
        {
            if (this.IsPlaying)
            {
                this.fadeState = FadeState.FadingOut;
            }
            else
            {
                this.FadeToNextClip();
            }
        }
        else
        {
            this.FadeToNextClip();
        }
    }

    /// <summary>
    ///   Continues fading in the current audio clip.
    /// </summary>
    public void Play()
    {
        this.fadeState = FadeState.FadingIn;
        this.audioSource.Play();
    }

    /// <summary>
    ///   Stop playing the current audio clip immediately.
    /// </summary>
    public void Stop()
    {
        this.audioSource.Stop();
        this.fadeState = FadeState.None;
    }

    private void AudioAwake()
    {
         // using the same code for Gameplay and Main scenes
        if ( bgTrackList.Length != 0 )
        {
            string shapeName = Scenes.GetParam("ShapeStyle");
            switch ( shapeName.ToUpper() )
            {
                case "TEE":
                case "BAR":
                case "CENTER_CORNER":
                    activeSong = bgTrackList[0];
                    break;

                case "CORNERS":
                case "ZEE":
                case "SIX":
                    activeSong = bgTrackList[0];
                    break;

                case "ODD_TWO":
                case "HOOK":
                case "WHY":
                    activeSong = bgTrackList[0];
                    break;

                case "ODD_ONE":
                case "ODD_THREE":
                case "ODD_FOUR":
                    activeSong = bgTrackList[0];
                    break;

                default:
                    activeSong = bgTrackList[0];
                    break;
            }
        } else {
            // User track as specified in Unity Editor
        }
        
        this.audioSource = this.GetComponent<AudioSource>();
        this.audioSource.volume = 0f;

        this.FadeInOut(activeSong, 0.5f, true);

    }

    private void FadeToNextClip()
    {
        this.audioSource.clip = this.nextClip;
        this.audioSource.loop = this.nextClipLoop;

        this.fadeState = FadeState.FadingIn;

        if (this.audioSource.enabled)
        {
            this.audioSource.Play();
        }
    }

    private void Update()
    {
        if (!this.audioSource.enabled)
        {
            return;
        }

        if (this.fadeState == FadeState.FadingOut)
        {
            if (this.audioSource.volume > this.FadeOutThreshold)
            {
                // Fade out current clip.
                this.audioSource.volume -= this.FadeSpeed * Time.deltaTime;
            }
            else
            {
                // Start fading in next clip.
                //this.FadeToNextClip();
                if ( stopWhenFadeOutDone) 
                {
                    this.Stop();
                }
            }
        }
        else if (this.fadeState == FadeState.FadingIn)
        {
            //if (this.audioSource.volume < this.nextClipVolume)
            if ( GameControl.Instance() ) {
                if (this.audioSource.volume < GameControl.Instance().activeVolume.volume)
                {
                    // Fade in next clip.
                    this.audioSource.volume += this.FadeSpeed * Time.deltaTime;
                }
                else
                {
                    // Stop fading in.
                    this.fadeState = FadeState.None;
                }
            }
        }
    }
#endregion
}
