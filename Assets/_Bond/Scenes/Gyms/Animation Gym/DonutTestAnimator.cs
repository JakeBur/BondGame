using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;

public class DonutTestAnimator : MonoBehaviour
{

    private EnemyAnimator animator => GetComponent<EnemyAnimator>();

    private void OnButton1()
    {
        animator.Play();
        animator.Attack();
        Debug.Log("Play");
    }

    private void OnButton2()
    {
        animator.Spawn();
        //animator.Pause();
        Debug.Log("Pause");
    }
}
