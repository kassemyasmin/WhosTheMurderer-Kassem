using UnityEngine;

public abstract class TipCondicional : MonoBehaviour
{
    private Timer timer;

    public GameObject tip;
    public float time;

    private bool shown = false;

    protected virtual void Start()
    {
        timer = FindObjectOfType<Timer>();
    }

    void Update()
    {
        if (shown)
        {
            if (tip.activeSelf)
            {
                if (MustHide())
                {
                    tip.SetActive(false);
                }
            }
        }
        else
        {
            if (!tip.activeSelf)
            {
                if (MustShow())
                {
                    shown = true;
                    tip.SetActive(true);
                }
            }
        }
    }

    private bool MustShow()
    {
        if (timer.tiempoRestante > time)
        {
            return false;
        }

        return !Hecho();
    }

    private bool MustHide()
    {
        return Hecho();
    }

    protected abstract bool Hecho();
}