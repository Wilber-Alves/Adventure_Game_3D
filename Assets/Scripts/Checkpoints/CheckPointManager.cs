using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Se usar UI legada (Text), troque para: using UnityEngine.UI;
using EDGEE.Core.Singleton;

public class CheckPointManager : Singleton<CheckPointManager>
{
    public int lastCheckPointKey = 0;

    public List<CheckPointBase> checkpoints;

    [Header("UI - Mensagem de Checkpoint")]
    public GameObject panel;      // Painel com o texto (arraste no Inspector)
    public TextMeshProUGUI label; // Texto que vai mostrar a mensagem
    public string message = "Checkpoint!";
    public float displayTime = 2f;

    private Coroutine _routine;

    protected override void Awake()
    {
        base.Awake();

        if (panel != null)
        {
            panel.SetActive(false);
        }
    }

    public bool HasCheckPoint()
    {
        return lastCheckPointKey > 0;
    }

    public void SaveCheckPoint(int i)
    {
        if (lastCheckPointKey < i)
        {
            lastCheckPointKey = i;
            ShowCheckPointMessage();
        }
    }

    private void ShowCheckPointMessage()
    {
        if (label != null)
        {
            label.text = message;
        }

        if (panel != null)
        {
            panel.SetActive(true);
        }

        if (_routine != null)
        {
            StopCoroutine(_routine);
        }

        _routine = StartCoroutine(HideAfterDelay());
    }

    private IEnumerator HideAfterDelay()
    {
        yield return new WaitForSeconds(displayTime);

        if (panel != null)
        {
            panel.SetActive(false);
        }
    }

    public Vector3 GetPositionFromLastCheckPoint()
    {
        var checkpoint = checkpoints.Find(i => i.key == lastCheckPointKey);
        return checkpoint.transform.position;
    }
}