using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text txtScore;

    [SerializeField]
    private Text txtInfo;

    [SerializeField]
    private CanvasGroup canvasGroupInfo;

    [SerializeField]
    private ResultPopUp resultPopUpPrefab;

    [SerializeField]
    private Transform canvasTran;

    [SerializeField]
    private Button btnInfo;

    [SerializeField]
    private Button btnTitle;

    [SerializeField]
    private Text lblStart;

    [SerializeField]
    private CanvasGroup canvasGroupTitle;

    private Tweener tweener;

    ///<param name = "score" ></ param>
    public void UpdateDisplayScore(int score)
    {
        txtScore.text = score.ToString();
    }

    public void DisplayGameOverInfo()
    {
        canvasGroupInfo.DOFade(1.0f,1.0f);
        txtInfo.DOText("Game Over...", 1.0f);
        btnInfo.onClick.AddListener(RestartGame);
    }

    public void GenerateResultPopUp(int score)
    {
        ResultPopUp resultPopUp = Instantiate(resultPopUpPrefab, canvasTran, false);
        resultPopUp.SetUpResultPopUp(score);
    }

    public void RestartGame()
    {
        btnInfo.onClick.RemoveAllListeners();
        string sceneName = SceneManager.GetActiveScene().name;
        canvasGroupInfo.DOFade(0f, 1.0f)
            .OnComplete(() =>
             {
                 Debug.Log("Restart");
                 SceneManager.LoadScene(sceneName);
             });
    }

    private void Start()
    {
        SwitchDisplayTitle(true,1.0f);
        btnTitle.onClick.AddListener(OnClickTitle);
    }

    public void SwitchDisplayTitle(bool isSwitch,float alpha)
    {
        if (isSwitch) canvasGroupTitle.alpha = 0;
        canvasGroupTitle.DOFade(alpha, 1.0f).SetEase(Ease.Linear).OnComplete(() =>
       {
           lblStart.gameObject.SetActive(isSwitch);
       });

        if(tweener == null)
        {
            //�����ɏ����������ꍇ��tweener�ɓ���Đ��䂷��iDOTween�j
            tweener = lblStart.gameObject.GetComponent<CanvasGroup>().DOFade(0, 1.0f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
        }
    }

    private void OnClickTitle()
    {
        btnTitle.onClick.RemoveAllListeners();
        SwitchDisplayTitle(false, 0.0f);
        StartCoroutine(DisplayGameStartInfo());
    }

    public IEnumerator DisplayGameStartInfo()
    {
        yield return new WaitForSeconds(0.5f);

        canvasGroupInfo.alpha = 0;
        canvasGroupInfo.DOFade(1.0f, 0.5f);
        txtInfo.text = "GameStart!";

        yield return new WaitForSeconds(1.0f);
        canvasGroupInfo.DOFade(0,0.5f);

        canvasGroupTitle.gameObject.SetActive(false);
    }
}
