﻿using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(Rigidbody))]
public class Character : MonoBehaviour
{
    public Animator animator;

    public float MoveSpeed;
    public float TurnSpeed;
    public float DashForce;
    public float DashCooldown;

    private Rigidbody rb;
    private Quaternion targetRot;

    private float dashRemainingTime = 0f;
    private bool isDashing = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (dashRemainingTime > 0)
        {
            dashRemainingTime = Mathf.Clamp(dashRemainingTime - Time.deltaTime, 0f, DashCooldown);
        }

        if (!isDashing)
        {
            rb.rotation = Quaternion.RotateTowards(rb.rotation, targetRot, TurnSpeed * Time.deltaTime);
        }
    }

    public void Move(float xValue, float zValue)
    {
        if (isDashing) return;

        Vector3 forwardDir = Camera.main.transform.forward;
        Vector3 rightDir = Camera.main.transform.right;

        forwardDir.y = 0f;
        forwardDir = forwardDir.normalized * zValue;

        rightDir.y = 0f;
        rightDir = rightDir.normalized * xValue;

        Vector3 newVelocity = (forwardDir + rightDir) * MoveSpeed;

        if (newVelocity != Vector3.zero)
        {
            // We rotate to face the new direction
            targetRot = Quaternion.LookRotation(newVelocity.normalized);
        }

        newVelocity.y = rb.velocity.y;

        rb.velocity = newVelocity;
        animator.SetFloat("Walk", rb.velocity.magnitude);
    }

    public bool Dash()
    {
        if (dashRemainingTime > 0f) return false;

        dashRemainingTime = DashCooldown;

        StartCoroutine(DashCoroutine());
        
        return true;
    }

    private IEnumerator DashCoroutine()
    {
        isDashing = true;

        Vector3 fwd = GetComponent<Transform>().forward;
        rb.velocity = Vector3.zero;
        rb.AddForce(fwd * DashForce, ForceMode.Impulse);
        animator.SetTrigger("Dash");

        yield return new WaitForSeconds(0.9f);

        isDashing = false;
    }
}