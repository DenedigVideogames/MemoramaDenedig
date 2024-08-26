using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class Cutscene : MonoBehaviour
{
    public PlayableDirector director;
    public AudioSource source;
    
    public void Awake()
    {
        /*director.Play();
        source.Play();*/
        Settings.Instance.PlayMusic("MainMenu");
    }

    private void Start()
    {
        source = GetComponent<AudioSource>();
        source.volume = 0f;
        StartCoroutine(Fade(true, source, 12f, 1f));
        /*StartCoroutine(Fade(true, _source, 2f, 0f));*/
    }
    
    public IEnumerator Fade(bool fadeIn, AudioSource source, float duration, float targetVolume)
    {
        if (!fadeIn)
        {
            double lengthSource = (double)source.clip.samples / source.clip.frequency;
            yield return new WaitForSecondsRealtime((float)(lengthSource - duration));
        }

        float time = 0f;
        float startVol = source.volume;
        while (time<duration)
        {
            string fadeSituation = fadeIn ? "fadeIn" : "fadeOut";
            time += Time.deltaTime;
            source.volume = Mathf.Lerp(startVol, targetVolume, time / duration);
            yield return null;
        }
        
        yield break;
    }
}
