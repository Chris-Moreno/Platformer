﻿using System;
using UnityEngine;

/// <summary>
/// The main component for the Player entity.  
/// Provides an interface to the player logic for various interested components.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(LadderMovement))]
[RequireComponent(typeof(WalkMovement))]
[RequireComponent(typeof(JumpMovement))]
[RequireComponent(typeof(Feet))]
public class Player : MonoBehaviour, IHaveDeathEffect
{
  #region Data
  /// <summary>
  /// A reference to the current player.  There is never more than one player at a time.
  /// </summary>
  public static Player instance;

  /// <summary>
  /// The current weapon, if any.  Used to ensure that the player does not equipt multiple hammers at the same time.
  /// </summary>
  [NonSerialized]
  public Hammer currentWeapon;

  /// <summary>
  /// Used to control physics on this object.
  /// </summary>
  public Rigidbody2D myBody
  {
    get; private set;
  }

  /// <summary>
  /// Used to determine if we are on the ground.
  /// </summary>
  public Feet feet
  {
    get; private set;
  }

  /// <summary>
  /// Used to control character walking.
  /// </summary>
  public WalkMovement walkMovement
  {
    get; private set;
  }

  /// <summary>
  /// Used to control character jumping.
  /// </summary>
  public JumpMovement jumpMovement
  {
    get; private set;
  }

  /// <summary>
  /// Used to control climbing ladders.
  /// </summary>
  public LadderMovement ladderMovement
  {
    get; private set;
  }
  #endregion

  #region Init
  /// <summary>
  /// On awake, populate variables.
  /// </summary>
  protected void Awake()
  {
    instance = this;

    myBody = GetComponent<Rigidbody2D>();
    feet = GetComponent<Feet>();
    walkMovement = GetComponent<WalkMovement>();
    jumpMovement = GetComponent<JumpMovement>();
    ladderMovement = GetComponent<LadderMovement>();

    Debug.Assert(myBody != null);
    Debug.Assert(feet != null);
    Debug.Assert(walkMovement != null);
    Debug.Assert(jumpMovement != null);
    Debug.Assert(ladderMovement != null);
  }

  /// <summary>
  /// On destroy, clear singleton instance.
  /// </summary>
  protected void OnDestroy()
  {
    if(instance == this)
    { // Only clear the instance if there isn't already a new player in the world
      instance = null;
    }
  }
  #endregion

  #region Events
  /// <summary>
  /// When the player dies, report it to the level manager (which may re-distribute the event to others).
  /// </summary>
  void IHaveDeathEffect.Die()
  {
    LevelManager levelManager = GameObject.FindObjectOfType<LevelManager>();
    levelManager.YouDied();
  }
  #endregion
}
