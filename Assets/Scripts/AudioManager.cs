using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource[] audioSources;
    // Start is called before the first frame update
    public IEnumerator PlayBGM(int index)
    {
        if(index != 0)
        {
            audioSources[index - 1].DOFade(0, 0.75f);
        }
        if(index == 3)
        {
            audioSources[index - 2].DOFade(0, 0.75f);
        }
        yield return new WaitForSeconds(0.45f);   //前の曲のボリュームが下がるのを待つ

        audioSources[index].Play();　　//新しい曲を再生する

        audioSources[index].DOFade(0.1f, 0.75f);

        if(index != 0)
        {
            yield return new WaitUntil(() => audioSources[index -1].volume == 0);
            audioSources[index - 1].Stop();
        }
    }
}
