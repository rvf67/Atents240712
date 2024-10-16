using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SortPanelUI : MonoBehaviour
{
    public event Action<ItemSortCriteria> onSortRequest;
    private void Awake()
    {
        Transform child = transform.GetChild(0);

        TMP_Dropdown dropdown = child.GetComponent<TMP_Dropdown>();
        dropdown.onValueChanged.AddListener((index) =>
        {
            Debug.Log(index);
        });
        child = transform.GetChild(1);
        Button run = child.GetComponent<Button>();
        run.onClick.AddListener(() =>
        {
            onSortRequest?.Invoke((ItemSortCriteria)dropdown.value);
        });
    }
}
