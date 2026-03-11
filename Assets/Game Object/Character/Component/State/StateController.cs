using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour
{
    [Header("State")]
    [SerializeField] private BaseState initialState;
    [SerializeField] private List<BaseState> stateList;

    private BaseState currentState;
    private Dictionary<BaseState.StateName, BaseState> stateDictionary =
        new Dictionary<BaseState.StateName, BaseState>();

    // ====================================================================================================
    //                     Virtual Functions
    // ====================================================================================================
    #region Virtual
    private void Start()
    {
        // Check state list
        if (stateList.Count == 0)
        {
            Debug.Log("State list is empty");
            return;
        }
        // Map all state
        foreach (BaseState state in stateList)
        {
            // Check key
            if (stateDictionary.ContainsKey(state.stateName))
            {
                Debug.Log("State has duplicate");
                continue;
            }
            // Add key to dictionary
            stateDictionary.Add(state.stateName, state);
        }
        // Check initial state
        if (!initialState)
        {
            Debug.Log("Initial state is empty, first state in state list is set as initial state");
            initialState = stateList[0];
        }
        if (!stateDictionary.ContainsKey(initialState.stateName))
        {
            Debug.Log(
                "Initial state is not mapped, first state in state list is set as initial state"
            );
            initialState = stateList[0];
        }
        // Set initial state
        currentState = initialState;
        currentState.EnterState();
    }

    private void Update()
    {
        if(currentState) currentState.UpdateState();
    }

    private void FixedUpdate()
    {
        if(currentState) currentState.FixedUpdateState();
    }
    #endregion

    // ====================================================================================================
    //                     State Functions
    // ====================================================================================================
    #region State
    public void ChangeState(BaseState.StateName newStateName)
    {
        // Check if can change state
        if(newStateName == GetCurrentStateName()) return;
        if(!stateDictionary.ContainsKey(newStateName))
        {
            Debug.Log("Change to a state that does not exist");
            return;
        }
        // Change state
        if (currentState) currentState.ExitState();
        currentState = stateDictionary[newStateName];
        currentState.EnterState();
    }

    public BaseState.StateName GetCurrentStateName()
    {
        return currentState.stateName;
    }
    #endregion
}