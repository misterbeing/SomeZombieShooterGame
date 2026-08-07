using System;
using UnityEngine;

public static class GameManager 
{
    public static CharacterControllerScript characterController;
    public static CharacterStateMachine characterStateMachine;
    public static WeaponHandler weaponHandler;
    public static ZombieSpawner zombieSpawner;

    public static Action<Zombie> onZombieEnteringPlayersView;
    public static Action<Zombie> onZombieExitingPlayersView;
}
