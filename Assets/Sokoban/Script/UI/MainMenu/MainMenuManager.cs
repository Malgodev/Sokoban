using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [Header("Button")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button settingButton;
    [SerializeField] private Button exitButton;

    [SerializeField] private Button returnButton;

    [Header("Panel")]
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private GameObject playPanel;

    private void Awake()
    {
        playButton.onClick.AddListener(ShowPanel);
    }

    private void ShowPanel()
    {
        playPanel.SetActive(true);
    }
}
