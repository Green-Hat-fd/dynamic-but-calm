using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour
{
    [SerializeField] List<MusicPlaylist> allPlaylists;
    
    //The variable that keeps track of                                 
    //which playlist is playing at the moment
    MusicPlaylist playingNowPlaylist;

    [Space(20)]
    #region Tooltip()
    [Tooltip("The playlist (index) which will be played when the scene is loaded")]
    #endregion
    [SerializeField] int startMusicIndex = 0;



    private void Start()
    {
        //Creates a random music "playlist" and starts playing it
        allPlaylists[startMusicIndex].ShuffleNStartPlaylist();

        //Keeps track of the newly started playlist
        playingNowPlaylist = allPlaylists[startMusicIndex];
    }


    /// <summary>
    /// Changes the playlist using the </i><b>index</b><i> of the list of playlists
    /// </summary>
    /// <param name="playlistIndex">the playlist index</param>
    public void ChangeMusicPlaylist(int playlistIndex)
    {
        //Stops all music on the playlist playing...
        playingNowPlaylist.StopAllMusic();

        //Starts playing the new playlist...
        allPlaylists[playlistIndex].ShuffleNStartPlaylist();

        //...And keeps track of the newly started one
        playingNowPlaylist = allPlaylists[playlistIndex];
    }

    public void StopCurrentMusic()
    {
        playingNowPlaylist.StopAllMusic();
    }


    #region Custom Get functions

    public MusicPlaylist GetCurrentMusic() => playingNowPlaylist;
    public AudioSource GetAudioSourceCurrentMusic() => playingNowPlaylist.GetComponent<AudioSource>();

    #endregion


    #region EXTRA - Changing the Inspector

    private void OnValidate()
    {
        //Clamps the start music index to be always positives
        //& below the playlists count
        startMusicIndex = Mathf.Clamp(startMusicIndex, 0, allPlaylists.Count-1);
    }

    #endregion
}
