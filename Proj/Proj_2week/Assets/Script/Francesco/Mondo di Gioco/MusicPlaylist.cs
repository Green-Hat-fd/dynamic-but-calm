using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicPlaylist : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;

    [Space(10)]
    [SerializeField] List<AudioClip> mainMusicPlaylist;

    Queue<AudioClip> shuffledPlaylist = new Queue<AudioClip>();



    private void Awake()
    {
        musicSource = GetComponent<AudioSource>();

        shuffledPlaylist = ShuffleQueue(mainMusicPlaylist);
    }

    IEnumerator ChangeMusic()
    {
        while (true)
        {
            //Plays music in the "playlist", dequeuing each time
            AudioClip nowClip = shuffledPlaylist.Dequeue();
            musicSource.PlayOneShot(nowClip);


            yield return new WaitForSeconds(nowClip.length);


            //If the "playlist" is finished playing, generates a new and random one
            if (shuffledPlaylist.Count <= 0)
                ReshuffleMusic();
        }
    }


    #region Funct. to manipulate this Playlist

    /// <summary>
    /// Takes all AudioClips and create a new random "playlist"
    /// </summary>
    void ReshuffleMusic()
    {
        shuffledPlaylist = ShuffleQueue(mainMusicPlaylist);
    }


    public void ShuffleNStartPlaylist()
    {
        ReshuffleMusic();
        StartCoroutine(ChangeMusic());
    }

    /// <summary>
    /// Stops all coroutine and stop all audiotracks on this playlist
    /// </summary>
    public void StopAllMusic()
    {
        StopAllCoroutines();
        musicSource.Stop();
    }

    #endregion


    #region Funct. to Shuffle/Randomize the queue

    Queue<T> ShuffleQueue<T>(List<T> _list)
    {
        for (int i = 0; i < _list.Count; i++)
        {
            //Takes [i] audio and leaves it aside,
            //then picks a random index
            T temp = _list[i];
            int randomIndex = Random.Range(0, _list.Count);

            //Swaps the [i] audio and the random index one
            _list[i] = _list[randomIndex];
            _list[randomIndex] = temp;
        }


        //Creates a new queue and switches the elements from the shuffled list to the queue
        Queue<T> q = new Queue<T>();

        foreach (T elem in _list)
        {
            q.Enqueue(elem);
        }


        return q;
    }

    #endregion
}
