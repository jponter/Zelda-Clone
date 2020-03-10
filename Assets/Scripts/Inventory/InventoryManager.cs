﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryManager : MonoBehaviour
{

    [Header("Inventory Information")]
    public PlayerInventory playerInventory;
    [SerializeField] private GameObject blankInventorySlot;
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private GameObject useButton;
    public InventoryItem currentItem;


    public void SetTextAndButton(string description, bool buttonEnabled)
    {
        descriptionText.text = description;
        useButton.SetActive(buttonEnabled);
    }

    void MakeInventorySlots()
    {
        if (playerInventory)
        {
            for (int i = 0; i < playerInventory.myInventory.Count; i++)
            {
                GameObject temp = Instantiate(blankInventorySlot,inventoryPanel.transform.position, Quaternion.identity);
                temp.transform.SetParent(inventoryPanel.transform);

                InventorySlot newSlot = temp.GetComponent<InventorySlot>();

                if (newSlot)
                {
                    
                    newSlot.Setup(playerInventory.myInventory[i], this);
                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        MakeInventorySlots();
        SetTextAndButton("", false);

    }

    public void SetupDescriptionAndButton(string description, bool buttonEnabled, InventoryItem newItem)
    {
        currentItem = newItem;
        descriptionText.text = description;
        useButton.SetActive(buttonEnabled);
    }

    public void UseButtonPressed()
    {
        if (currentItem)
        {
            currentItem.Use();
        }
    }

}
