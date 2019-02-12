using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public static UI Instance;

    public RectTransform HealthBar;
    public Text HealthText;

    public Text PointsText;

    public CanvasGroup EndScreen;
    public Text FinalPointsText;

    protected int _points;

    private void Awake()
    {
        Instance = this;
        AddPoints(0);
    }

    public void AddPoints(int amount)
    {
        _points += amount;
        PointsText.text = _points.ToString();
    }

    public void UpdateHealth(int health, int max)
    {
        var percent = (float)health / max;
        HealthBar.localScale = HealthBar.localScale.WithX(percent);
        HealthText.text = $"{health}/{max}";
    }

    public void ShowEndScreen()
    {
        StartCoroutine(ShowEndScreen_Coroutine());
    }

    protected IEnumerator ShowEndScreen_Coroutine()
    {
        FinalPointsText.text = _points.ToString();
        while(EndScreen.alpha < 1.0f)
        {
            EndScreen.alpha += .01f;
            yield return new WaitForSeconds(.01f);
        }

        yield return null;
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
