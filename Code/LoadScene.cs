using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameEnterManager : MonoBehaviour
{
    [SerializeField] private GameObject loadingPagePanel;
    [SerializeField] private Slider loadingToMenuScene;
    [SerializeField] private Text progressText;
    
    

    void Start()
    {
        loadingPagePanel.SetActive(false);
    }

    public void goToGameMenu()
    {
        loadingPagePanel.SetActive(true);
        loadAsyncScene(1, loadingToMenuScene);
    }
    
    void loadAsyncScene(int sceneIndex, Slider sliderName)
    {
        StartCoroutine(Async(sceneIndex, sliderName));
    }

    IEnumerator Async(int sceneIndex, Slider sliderName)
    {

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            sliderName.value = progress;
            progressText.text = progress * 100f + "%";
            yield return null;
        }

    }
}
