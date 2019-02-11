using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public static UI Instance;

    public RectTransform HealthBar;
    public Text HealthText;

    public Text PointsText;

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

}
