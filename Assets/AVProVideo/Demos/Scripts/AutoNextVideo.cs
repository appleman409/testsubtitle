using RenderHeads.Media.AVProVideo;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using System.IO;

public class AutoNextVideo : MonoBehaviour
{
    [SerializeField] MediaPlayer _mediaPlayer;
    [SerializeField] MediaPlayer _mediaPlayerB;
    [SerializeField] DisplayUGUI displayUGUI;
    [SerializeField] SubtitlesUGUI subtitlesUGUI;
    [SerializeField] int SceneID;
    [SerializeField] SceneData sceneData;
    [SerializeField] GameObject Canvas;
    [SerializeField] ChooseController ChooseControllers;
    [SerializeField] List<GameObject> listChooseObject;
    [SerializeField] TextMeshProUGUI TextChoose;
    [SerializeField] EndingControler EndingControlers;
    public MediaPlayer.FileLocation _location = MediaPlayer.FileLocation.RelativeToStreamingAssetsFolder;
    bool frist = false;
    public MediaPlayer _loadingPlayer;


    public string GetCurScene()
    {
        return SceneID.ToString();
    }

    public string GetnextScene()
    {
        return sceneData.nextSceneID.ToString();
    }

    public MediaPlayer PlayingPlayer
    {
        get
        {
            if (LoadingPlayer == _mediaPlayer)
            {
                return _mediaPlayerB;
            }
            return _mediaPlayer;
        }
    }

    public MediaPlayer LoadingPlayer
    {
        get
        {
            return _loadingPlayer;
        }
    }

    private void SwapPlayers()
    {
        // Pause the previously playing video
        PlayingPlayer.Control.Pause();
        PlayingPlayer.Control.SetLooping(false);

        // Swap the videos
        if (LoadingPlayer == _mediaPlayer)
        {
            if(sceneData.hasSubtitle) subtitlesUGUI.ChangeMediaPlayer(_mediaPlayer);
            if (sceneData.minigameOption)
            {
                var minigame = Instantiate(sceneData.minigameOption, Canvas.transform);
                minigame.GetComponent<HoldButton>().mediaPlayer = _loadingPlayer;
            } 
                
            _loadingPlayer = _mediaPlayerB;
            _loadingPlayer.DisableSubtitles();
        }

        else
        {
            if (sceneData.hasSubtitle) subtitlesUGUI.ChangeMediaPlayer(_mediaPlayerB);
            if (sceneData.minigameOption)
            {
                var minigame = Instantiate(sceneData.minigameOption, Canvas.transform);
                minigame.GetComponent<HoldButton>().mediaPlayer = _loadingPlayer;
            }

            _loadingPlayer = _mediaPlayer;
            _loadingPlayer.DisableSubtitles();
        }

        displayUGUI.CurrentMediaPlayer = PlayingPlayer;
    }


    private void Awake()
    {
        //_loadingPlayer = _mediaPlayerB;
        NextScene();

        frist = true;

        _mediaPlayer.Events.AddListener(OnVideoEvent);
        _mediaPlayerB.Events.AddListener(OnVideoEvent);
    }


    private void Update()
    {
        //if(!PlayingPlayer.Control.IsPlaying()) PlayingPlayer.Play();
    }

    public void OnVideoEvent(MediaPlayer mp, MediaPlayerEvent.EventType et, ErrorCode errorCode)
    {
        switch (et)
        {
            case MediaPlayerEvent.EventType.FinishedPlaying:
                NextScene();
                break;
            case MediaPlayerEvent.EventType.FirstFrameReady:
                SwapPlayers();
                break;
        }
    }

    public void NextScene()
    {
        SceneData nextsceneData = Resources.Load<SceneData>($"Scene/{sceneData.nextSceneID}");
        Debug.Log(sceneData.nextSceneID);
        if(nextsceneData == null)
        {
            if (sceneData.checkChoose == true)
            {
                CheckChooses();
            }
            else if (sceneData.endScene == true)
            {
                EndingControlers.EndingSceneStart(SceneID);
            }
        }
        else
        {
            if (nextsceneData.chooses.Count == 0 && !nextsceneData.minigameOption)
        {
                PlayScene(); return;
            }
            else if (nextsceneData.chooses.Count > 0)
            {
                chooseScene();
                return;
            }
            if(nextsceneData.minigameOption)
            {
                PlayMinigame();
                return;
            }
        }
        
        
    }

    public void PlayMinigame()
    {
        PlayScene();
        

    }

    public void PlayScene()
    {
        SceneID = sceneData.nextSceneID;
        string path = "Video/" + sceneData.nextSceneID + ".mp4";
        _loadingPlayer.OpenVideoFromFile(_location, path, true);
        string subtitlepath = Application.dataPath + "/StreamingAssets/Subtitles/" + sceneData.nextSceneID + ".srt";
        if (File.Exists(subtitlepath))
        {
            _loadingPlayer.EnableSubtitles(_location, "Subtitles/" + sceneData.nextSceneID + ".srt");
        }
        sceneData = Resources.Load<SceneData>($"Scene/{sceneData.nextSceneID}");
    }

    public void chooseScene()
    {
        SceneID = sceneData.nextSceneID;
        
        string path = "Video/" + sceneData.nextSceneID + ".mp4";
        _loadingPlayer.OpenVideoFromFile(_location, path, true);
        string subtitlepath = Application.dataPath + "/StreamingAssets/Subtitles/" + sceneData.nextSceneID + ".srt";
        if (File.Exists(subtitlepath))
        {
            _loadingPlayer.EnableSubtitles(_location, "Subtitles/" + sceneData.nextSceneID + ".srt");
        }
        _loadingPlayer.Control.SetLooping(true);
        sceneData = Resources.Load<SceneData>($"Scene/{sceneData.nextSceneID}");
        for (int i = 0; i < sceneData.chooses.Count; i++)
        {
            var choose = Instantiate(TextChoose, Canvas.transform);
            choose.transform.localPosition = sceneData.chooses[i].coords;
            choose.text = sceneData.chooses[i].OptionTitle;
            choose.GetComponent<TextGlowEffect>().controller = this;
            listChooseObject.Add(choose.gameObject);
        }
    }

    public void ChooseEnter(string text)
    {
        for (int i = 0; i < sceneData.chooses.Count; i++)
        {
            Destroy(listChooseObject[i]);
            
        }
        listChooseObject.Clear();
        for (int i = 0; i < sceneData.chooses.Count; i++)
        {
            
            if (text == sceneData.chooses[i].OptionTitle)
            {
                if(sceneData.ChooseID != 0)
                {
                    if (sceneData.chooses[i].istrue) ChooseControllers.UpdateCurIndex(sceneData.ChooseID);
                }
                
                PlayScene(sceneData.chooses[i].SceneIDForOption);
            }
        }
    }

    private void CheckChooses()
    {
        ChooseData chooseData = ChooseControllers.GetChooseDataByID(sceneData.ChooseID);
        if (chooseData.IsCurIndexEqualToNeedIndex())
        {
            PlayScene(chooseData.SceneID);
        }else PlayScene(chooseData.EndSceneID);

    }

    public void PlayScene(int ID)
    {
        string path = "Video/" + ID + ".mp4";
        _loadingPlayer.OpenVideoFromFile(_location, path, true);
        string subtitlepath = Application.dataPath + "/StreamingAssets/Subtitles/" + ID + ".srt";
        if (File.Exists(subtitlepath))
        {
            _loadingPlayer.EnableSubtitles(_location, "Subtitles/" + ID + ".srt");
        }
        SceneID = ID;
        sceneData = Resources.Load<SceneData>($"Scene/{ID}");
    }

    public void TryAgain()
    {
        EndingData endingData = Resources.Load<EndingData>($"EndingScene/{SceneID}");
        sceneData = Resources.Load<SceneData>($"Scene/{endingData.GoBackID-1}");
        ChooseControllers.ResetCurIndex(endingData.ChooseID);
        NextScene();
    }

}
