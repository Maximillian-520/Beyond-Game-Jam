using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerSprite : MonoBehaviour
{
    public event EventHandler OnPunchAnimationCompleted;

    [Header("Component and Object")]
    [SerializeField] private Animator animator;
    [SerializeField] private List<SpriteRenderer> spriteList;
    [Tooltip("Can be left empty")]
    [SerializeField] private SpriteRenderer shadow;
    [Header("Animation")]
    [SerializeField] private string idleAnimationName = "PlayerIdle";
    [SerializeField] private string punchAnimationName = "PlayerPunch";
    [SerializeField] private Color hurtFlashColor = Color.red;
    [SerializeField] private float disappearFreezeDuration = 1f;
    [SerializeField] private float disappearTransitionDuration = 1f;

    private int currentFacingDirection = 1;

    // ====================================================================================================
    //                     Virtual Functions
    // ====================================================================================================
    #region Virtual
    private void Start()
    {
        // Assertion check
        Debug.Assert(animator, "animator is missing");
        // Check spriteList
        if (spriteList.Count == 0) Debug.Log("spriteList is empty");
    }
    #endregion

    // ====================================================================================================
    //                     Sprite Functions
    // ====================================================================================================
    #region Sprite
    // NOT USED
    public void UpdateFacingDirection(int newDirection)
    {
        if (newDirection == 0) return;
        if (newDirection != currentFacingDirection)
        {
            Vector3 newScale = transform.localScale;
            newScale.x *= -1;
            transform.localScale = newScale;
            currentFacingDirection = newDirection;
        }
    }

    public void DoIdle() {animator.SetBool("isWalking", false);}

    public void DoWalk() {animator.SetBool("isWalking", true);}

    public void DoPunch() {animator.Play(punchAnimationName);}

    public void DoHurt(float duration) {StartCoroutine(DisplayHurtFlash(duration));}

    public void DoDisappear() {StartCoroutine(DisappearSequence());}

    public void InteruptAllAnimation() {animator.Play(idleAnimationName, 0);}

    public void PunchAnimationCompleted() {OnPunchAnimationCompleted.Invoke(this, EventArgs.Empty);}

    private IEnumerator DisplayHurtFlash(float duration)
    {
        foreach (SpriteRenderer sprite in spriteList)
        {
            sprite.color = hurtFlashColor;
        }
        yield return new WaitForSecondsRealtime(duration);
        foreach (SpriteRenderer sprite in spriteList)
        {
            sprite.color = Color.white;
        }
    }

    private IEnumerator DisappearSequence()
    {
        foreach (SpriteRenderer sprite in spriteList)
        {
            sprite.color = hurtFlashColor;
        }
        yield return new WaitForSeconds(disappearFreezeDuration);
        foreach (SpriteRenderer sprite in spriteList)
        {
            sprite.DOColor(new Color(0, 0, 0, 0), disappearTransitionDuration);
        }
        if (shadow) shadow.DOColor(new Color(0, 0, 0, 0), disappearTransitionDuration);
    }
    #endregion
}
