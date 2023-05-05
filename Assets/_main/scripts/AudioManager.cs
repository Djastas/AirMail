  using System;
  using System.Collections.Generic;
  using System.Linq;
  using StvDEV.StarterPack;
  using UnityEngine;
 
  namespace BrainyJunior.MyGame.Scripts.Managers
  {
      public class AudioManager : MonoBehaviourSingleton<AudioManager> {
          
          [SerializeField] private List<AudioObject> audioList;
          [SerializeField] private List<AudioObject> backgroundMusics;
          
          [SerializeField] private GameObject audioSourcePrefab;
          [SerializeField] private bool playBackgroundMusicOnStart;

          [HideInInspector] public AudioSource background;

          private List<AudioSource> _audioSources;


          protected override void Start()
          {
              _audioSources = new List<AudioSource>();
              if (playBackgroundMusicOnStart)
              {
                  PlayAudioById("background", true , true);
              }
          }
          
          public int PlayAudioById(string id, bool loop = false , bool isBackground = false)
          {
              AudioObject audioObj;
              if (isBackground)
              {
                  audioObj = backgroundMusics.Find(obj => obj.id == id);
                  if (audioObj != null) SetBackgroundMusic(audioObj);
                  
              }
              else
              {
                  audioObj = audioList.Find(obj => obj.id == id);
                  
                  if (audioObj != null)
                  {
                      return PlayAudioObject(audioObj,loop);
                  }
              }
             
 
              Debug.LogWarning("name of the audio track incorrect");

              return 0;
          }
         
          

          public int PlayAudioObject(AudioObject audioObject, bool loop = false)
          {
              var audioSource = Instantiate(audioSourcePrefab,gameObject.transform);
              
              _audioSources.Add(audioSource.GetComponent<AudioSource>());
              _audioSources[^1].gameObject.name = audioObject.id;
              _audioSources[^1].clip = audioObject.audio;
              _audioSources[^1].loop = loop;
              _audioSources[^1].Play();
              
              return _audioSources.Count - 1;
          }

          public void SetBackgroundMusic(AudioObject audioObject)
          {
              if (background == null)
              {
                  var audioSource = Instantiate(audioSourcePrefab,gameObject.transform);
                  background = audioSource.GetComponent<AudioSource>();
              }
              
              
              background.gameObject.name = audioObject.id;
              background.clip = audioObject.audio;
              background.Play();

          }

          public void DeleteAudioNum(int num)
          {
              Destroy(_audioSources[num].gameObject);
          }
          public void StopAudioNum(int num)
          {
              _audioSources[num].Stop();
          }

          public void PlayAudioNum(int num)
          {
              _audioSources[num].Play();
          }

          [Serializable]
          public class AudioObject {
              public string id;
              public AudioClip audio;
          }
      }
  }

