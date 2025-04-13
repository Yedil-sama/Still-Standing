using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public abstract class StatBarView<T> : MonoBehaviour where T : Stat
{
    public T stat;
    public float currentView;
    public float lerpDuration = 0.5f;

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
        UpdateView();
    }

    protected abstract void SubscribeEvents();

    public void UpdateView()
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
        coroutine = StartCoroutine(LerpStat());
    }

    private IEnumerator LerpStat()
    {
        float elapsedTime = 0;
        float start = lastValue;
        float target = stat.Current;

        while (elapsedTime < lerpDuration)
        {
            currentView = Mathf.Lerp(start, target, elapsedTime / lerpDuration);
            UpdateView();
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        currentView = target;
        UpdateView();
        coroutine = null;
    }
}
