using System.Collections;
using UnityEngine;

public class CharacterStateMachine : MonoBehaviour
{
    public CharacterStates currentState;
    public Animator CharacterAnimator;

    private void OnEnable()
    {
        GameManager.characterStateMachine = this;
    }
    public void ChangeState(CharacterStates newState)
    {
        currentState = newState;
        //CharacterAnimator?.SetInteger("State", (int)currentState);
        CharacterAnimator?.SetFloat("Blend", (int)currentState);
    }
}


public enum CharacterStates
{
    idle,
    walkForward,
    walkBackward,
    walkLeft,
    walkRight,
}

