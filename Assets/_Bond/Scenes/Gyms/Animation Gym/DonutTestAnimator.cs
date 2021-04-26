using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;

public class DonutTestAnimator : MonoBehaviour
{

    private EnemyAnimator animator => GetComponent<EnemyAnimator>();

    private void OnButton1()
    {
        animator.Spawn();
        animator.Pause();
        Debug.Log("Prep");
        
    }

    private void OnButton2()
    {
        animator.Play();
        animator.Spawn();
        Debug.Log("Spawn");
    }

    private void OnButton3()
    {
        animator.Play();
        animator.Attack();
        Debug.Log("Attack");
    }
}
