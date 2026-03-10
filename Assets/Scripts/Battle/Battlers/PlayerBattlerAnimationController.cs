using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> 
/// If the component is a player & within battle, play animations associated
/// with the action within the battle
/// </summary>
[RequireComponent(typeof(PlayerBattler))]
public class PlayerBattlerAnimationController : MonoBehaviour
{
    private Animator _animator;
    public Animator Animator {
        get {
            return _animator;
        } 
        private set { 
            _animator = value;
        }
    } 

    // When called, get the animator to allow animations to be played
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    // TODO: Add methods to set triggers for animations for 
    // idle, attack, getting damaged, evading, etc.?
}