using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class StatBarView<T> : MonoBehaviour where T : Stat
{
    public T stat;
    public float currentView;
    public float lerpDuration = 0.5f;
    [SerializeField] protected TMP_Text valueText;

    protected float lastValue;
    protected Image valueImage;
    protected Coroutine coroutine;

    public virtual void Awake()
    {
        valueImage = GetComponent<Image>();
    }

    public virtual void Initialize(T stat)
    {
        this.stat = stat;
        SubscribeEvents();
        currentView = stat.Current;
        lastValue = currentView;
        UpdateText();
        UpdateFill();
    }

    protected abstract void SubscribeEvents();

    private void UpdateText()
    {
        if (valueText != null)
            valueText.text = $"{Mathf.FloorToInt(stat.Current)} / {Mathf.FloorToInt(stat.Total)}";
    }

    private void UpdateFill()
    {
        float total = currentView / stat.Total;

        if (total < 0 || float.IsNaN(total))
        {
            valueImage.fillAmount = 0;
        }
        else
        {
            valueImage.fillAmount = total;
        }
    }

    public void StartLerp(float amount)
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }

        lastValue = currentView;
        UpdateText();
        coroutine = StartCoroutine(LerpStat());
    }

    private IEnumerator LerpStat()
    {
        float elapsedTime = 0f;
        float start = currentView;
        float target = stat.Current;

        while (elapsedTime < lerpDuration)
        {
            float t = elapsedTime / lerpDuration;
            currentView = Mathf.Lerp(start, target, t);
            UpdateFill();
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        currentView = target;
        UpdateFill();
        coroutine = null;
    }
}
