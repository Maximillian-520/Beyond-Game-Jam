using System;
using UnityEngine;

public class UIAnimationController : MonoBehaviour
{
    public event EventHandler OnOpeningSequenceFinished;
    public event EventHandler OnChoreBreakFinished;
    public event EventHandler OnPlayerWinFinihsed;
    public event EventHandler OnPlayerLoseFinihsed;

    [Header("Component and Object")]
    [SerializeField] private Animator animator;

    [Header("Animation")]
    [SerializeField] private string openingSequenceAnimationName = "UIOpeningSequence";
    [SerializeField] private string choreBreakAnimationName = "UIChoreBreak";
    [SerializeField] private string playerWinAnimationName = "UIPlayerWin";
    [SerializeField] private string playerLoseAnimationName = "UIPlayerLose";

    // ====================================================================================================
    //                     Virtual Functions
    // ====================================================================================================
    #region Virtual
    private void Start()
    {
        // Assertion check
        Debug.Assert(animator, "animator is missing");
    }
    #endregion

    // ====================================================================================================
    //                     Animation Functions
    // ====================================================================================================
    #region Animation
    public void DoOpeningSequence()
    {
        animator.Play(openingSequenceAnimationName, 0);
    }

    public void OpeningSequenceFinish() {OnOpeningSequenceFinished.Invoke(this, EventArgs.Empty);}

    public void DoChoreBreak()
    {
        animator.Play(choreBreakAnimationName, 0, 0.0f);
    }

    public void ChoreBreakFinish() {OnChoreBreakFinished.Invoke(this, EventArgs.Empty);}

    public void DoPlayerWin()
    {
        animator.Play(playerWinAnimationName, 0);
    }

    public void PlayerWinFinish() {OnPlayerWinFinihsed.Invoke(this, EventArgs.Empty);}

    public void DoPlayerLose()
    {
        animator.Play(playerLoseAnimationName, 0);
    }

    public void PlayerLoseFinish() {OnPlayerLoseFinihsed.Invoke(this, EventArgs.Empty);}
    #endregion

    // ====================================================================================================
    //                     External Functions
    // ====================================================================================================
    #region External
    public void DoPlayerDisappear() {Player.Instance.playerSprite.DoDisappear();}
    #endregion
}
