using System;
using UnityEngine;

public class UIAnimationController : MonoBehaviour
{
    public event EventHandler OnOpeningSequenceFinished;
    public event EventHandler OnChoreBreakFinished;

    [Header("Component and Object")]
    [SerializeField] private Animator animator;

    [Header("Animation")]
    [SerializeField] private string openingSequenceAnimationName = "UIOpeningSequence";
    [SerializeField] private string choreBreakAnimationName = "UIChoreBreak";

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
        animator.Play(choreBreakAnimationName, 0);
    }

    public void ChoreBreakFinish() {OnChoreBreakFinished.Invoke(this, EventArgs.Empty);}
    #endregion
}
