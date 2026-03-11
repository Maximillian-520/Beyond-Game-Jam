using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlateDirt : MonoBehaviour, IPointerEnterHandler
{
    public event EventHandler OnPlateDirtCleaned;

    [Header("Component and Object")]
    [SerializeField] private UnityEngine.UI.Image image;
    [Header("Dirt")]
    [SerializeField] private List<Sprite> dirtTextureList = new List<Sprite>();

    public int Thickness
    {
        set
        {
            // Set value
            currentThickness = value;
            // Check for thickness
            if (currentThickness <= 0) DespawnPlateDirt();
            // Update sprite
            UpdatePlateDirtSprite();
        }
        get{return currentThickness;}
    }

    private int currentThickness = 0;

    // ====================================================================================================
    //                     Virtual Functions
    // ====================================================================================================
    #region Virtual
    private void Start()
    {
        // Assertion check
        Debug.Assert(image, "image is missing");
        Debug.Assert(dirtTextureList.Count > 0, "dirtTextureList is empty");
    }
    #endregion

    // ====================================================================================================
    //                     Pointer Functions
    // ====================================================================================================
    #region Pointer
    public void OnPointerEnter(PointerEventData eventData)
    {
        // Check if sponge is scrubbing
        if (!Sponge.Instance.Scrubbing) return;
        // Reduce thickness
        Thickness--;
    }
    #endregion
    
    // ====================================================================================================
    //                     Dirt Functions
    // ====================================================================================================
    #region Dirt
    private void UpdatePlateDirtSprite()
    {
        // Check if dirt texture is sufficient
        if (currentThickness > dirtTextureList.Count)
        {
            Debug.Log("dirtTextureList is insufficient textures");
            image.sprite = dirtTextureList[dirtTextureList.Count - 1];
        }
        // Set sprite from thickness
        else image.sprite = dirtTextureList[Math.Max(currentThickness - 1 , 0)];
    }

    private void DespawnPlateDirt()
    {
        OnPlateDirtCleaned.Invoke(this, EventArgs.Empty);
        Destroy(gameObject);
    }
    #endregion
}
