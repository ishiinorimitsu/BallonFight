using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;


public class ResultPopUp : MonoBehaviour
{
    [SerializeField]
    private Text txtscore;

    [SerializeField]
    private CanvasGroup canvasGroup;


    [SerializeField]
    private CanvasGroup canvasGroupRestart;

    [SerializeField]
    private Button btnTitle;

    // Start is called before the first frame update
    public void SetUpResultPopUp(int score)
    {
        canvasGroup.alpha = 0;
        canvasGroup.DOFade(1.0f, 1.0f);
        txtscore.text = score.ToString();
        canvasGroupRestart.DOFade(0, 1.0f).SetEase(Ease.InOutQuad).SetLoops(-1, LoopType.Yoyo);
        btnTitle.onClick.AddListener(OnClickRestart);
    }

    public void OnClickRestart()
    {
        canvasGroup.DOFade(0, 1.0f).SetEase(Ease.Linear);
        StartCoroutine(ReStart());
    }

    private IEnumerator ReStart()
    {
        yield return new WaitForSeconds(1.0f);
        string gamescene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(gamescene);
    }

    
}
