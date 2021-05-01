using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;

public class DonutTestAnimator : MonoBehaviour
{

    private DonutAnimator animator => GetComponent<DonutAnimator>();

    private void OnSemicolon()
    {
        animator.Pause();
    }

    private void OnL()
    {
        animator.Play();
    }

    private void OnP()
    {
        animator.Spawn();
        animator.Pause();
        Debug.Log("Prep");
    }

    private void OnO()
    {
        animator.Play();
        animator.Spawn();
        Debug.Log("Spawn");
    }
    // Mapped to WASD
    private void OnMovement(InputValue value)
    {
        var direction = value.Get<Vector2>();
        direction.Normalize();

        var direction3 = new Vector3( direction.x, 0, direction.y );
        animator.Move(direction3);
    }

    private void OnButton1()
    {
        animator.Play();
        animator.Attack();
        Debug.Log("Attack");
    }

    private void OnButton2()
    {
        
    }

    private void OnButton3()
    {
        
    }

    private void OnButton4()
    {
        
    }

    private void OnButton5()
    {
        
    }

    private void OnButton6()
    {
        
    }

    private void OnButton7()
    {
        
    }

    private void OnButton8()
    {
        
    }

    private void OnButton9()
    {
        
    }

    private void OnButton0()
    {
        
    }

    private void OnQ()
    {
        animator.Hitstun();
    }

    private void OnE()
    {
        animator.HitstunDone();
    }

    private void OnF()
    {

    }

    private void OnZ()
    {
        animator.Death();
    }

    private void OnX()
    {

    }

    private void OnC()
    {

    }
    
}
