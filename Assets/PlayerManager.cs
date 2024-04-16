using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [HideInInspector] public Rigidbody2D rbd;
    [HideInInspector] public PlayerMovement pm;
    [HideInInspector] public PlayerAttack pa;
    [HideInInspector] public PlayerHealth ph;
    [HideInInspector] public PlayerInteraction pi;
    [HideInInspector] public PlayerUI pui;
    [HideInInspector] public PlayerCrowAttack pca;
    [HideInInspector] public PlayerAudioController pac;
    [HideInInspector] public ComponentToggler componentToggler;

    private void Awake()
    {
        rbd = GetComponent<Rigidbody2D>();
        pm = GetComponent<PlayerMovement>();
        pa = GetComponent<PlayerAttack>();
        ph = GetComponent<PlayerHealth>();
        pi = GetComponent<PlayerInteraction>();
        pui = GetComponent<PlayerUI>();
        pca = GetComponent<PlayerCrowAttack>();
        pac = GetComponent<PlayerAudioController>();
        componentToggler = GetComponent<ComponentToggler>();
    }
}