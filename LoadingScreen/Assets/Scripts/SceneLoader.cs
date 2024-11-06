using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // UI Elements
    public CanvasGroup publisherLogo, gameLogo, mainMenuCanvas, loadingScreenCanvas;
    public TextMeshProUGUI startGameText, tapToBeginText, versionText, taskTextTMP, percentageTextTMP;
    public Button exitGameButton, settingsButton, fileVerificationButton;
    public Slider progressBar;

    // Parameters
    public float fadeDuration = 1f, blinkingSpeed = 0.5f;
    public string gameVersion = "v1.0.0";

    private void Start()
    {
        // Initialize UI
        versionText.text = gameVersion;
        SetAllActive(false);
        StartCoroutine(BrandingSequence());
    }

    private IEnumerator BrandingSequence()
    {
        // Publisher Logo
        yield return DisplayLogo(publisherLogo);
        yield return new WaitForSeconds(0.5f);

        // Game Logo
        yield return DisplayLogo(gameLogo);

        // Start main menu sequence
        yield return MainMenuSequence();
    }

    private IEnumerator DisplayLogo(CanvasGroup logo)
    {
        SetGameObjectActive(logo.gameObject, true);
        yield return FadeCanvasGroup(logo, 0, 1, fadeDuration);
        yield return new WaitForSeconds(fadeDuration);
        yield return FadeCanvasGroup(logo, 1, 0, fadeDuration);
        SetGameObjectActive(logo.gameObject, false);
    }

    private IEnumerator MainMenuSequence()
    {
        SetMainMenuActive(true);
        StartCoroutine(BlinkText(tapToBeginText));

        // Wait for tap to start game
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        SetMainMenuActive(false);
        yield return FadeCanvasGroup(mainMenuCanvas, 1, 0, fadeDuration);

        yield return LoadScreenSequence();
    }

    private void SetMainMenuActive(bool isActive)
    {
        SetGameObjectActive(mainMenuCanvas.gameObject, isActive);
        SetGameObjectActive(startGameText.gameObject, isActive);
        SetGameObjectActive(tapToBeginText.gameObject, isActive);
        SetGameObjectActive(versionText.gameObject, isActive);
        SetGameObjectActive(exitGameButton.gameObject, isActive);
    }

    private IEnumerator LoadScreenSequence()
    {
        SetLoadingScreenActive(true);
        progressBar.value = 0;

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("GameScene");
        asyncLoad.allowSceneActivation = false;

        // Loading progress
        while (asyncLoad.progress < 0.9f)
        {
            progressBar.value = asyncLoad.progress / 0.9f;
            percentageTextTMP.text = $"{Mathf.FloorToInt(progressBar.value * 100)}%";
            yield return null;
        }

        SetLoadingScreenMidPoint();

        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));

        yield return FadeCanvasGroup(loadingScreenCanvas, 1, 0, fadeDuration);
        asyncLoad.allowSceneActivation = true;
    }

    private void SetLoadingScreenActive(bool isActive)
    {
        SetGameObjectActive(loadingScreenCanvas.gameObject, isActive);
        SetGameObjectActive(progressBar.gameObject, isActive);
        SetGameObjectActive(taskTextTMP.gameObject, isActive);
        SetGameObjectActive(percentageTextTMP.gameObject, isActive);
        SetGameObjectActive(versionText.gameObject, isActive);
    }

    private void SetLoadingScreenMidPoint()
    {
        SetGameObjectActive(settingsButton.gameObject, true);
        SetGameObjectActive(fileVerificationButton.gameObject, true);
        SetGameObjectActive(exitGameButton.gameObject, true);

        SetGameObjectActive(percentageTextTMP.gameObject, false);
        SetGameObjectActive(progressBar.gameObject, false);
        taskTextTMP.rectTransform.anchoredPosition += new Vector2(0, -50);
        taskTextTMP.text = "Tap to begin";
    }

    private IEnumerator FadeCanvasGroup(CanvasGroup canvasGroup, float startAlpha, float endAlpha, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = endAlpha;
    }

    private IEnumerator BlinkText(TextMeshProUGUI text)
    {
        while (mainMenuCanvas.alpha > 0.3)
        {
            text.alpha = Mathf.PingPong(Time.time * blinkingSpeed, 1f);
            yield return null;
        }
    }

    public void ExitGame() => Application.Quit();

    private void SetAllActive(bool isActive)
    {
        SetGameObjectActive(publisherLogo.gameObject, isActive);
        SetGameObjectActive(gameLogo.gameObject, isActive);
        SetGameObjectActive(mainMenuCanvas.gameObject, isActive);
        SetGameObjectActive(loadingScreenCanvas.gameObject, isActive);
        SetGameObjectActive(versionText.gameObject, isActive);
        SetGameObjectActive(startGameText.gameObject, isActive);
        SetGameObjectActive(tapToBeginText.gameObject, isActive);
        SetGameObjectActive(progressBar.gameObject, isActive);
        SetGameObjectActive(taskTextTMP.gameObject, isActive);
        SetGameObjectActive(percentageTextTMP.gameObject, isActive);
        SetGameObjectActive(exitGameButton.gameObject, isActive);
        SetGameObjectActive(settingsButton.gameObject, isActive);
        SetGameObjectActive(fileVerificationButton.gameObject, isActive);
    }

    private void SetGameObjectActive(GameObject go, bool isActive)
    {
        go.SetActive(isActive);
    }
}
