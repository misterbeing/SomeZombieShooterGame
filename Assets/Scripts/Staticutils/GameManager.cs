using System;
using UnityEngine;

public static class GameManager 
{
    public static CharacterControllerScript characterController;
    public static CharacterStateMachine characterStateMachine;
    public static WeaponHandler weaponHandler;
    public static ZombieSpawner zombieSpawner;

    public static GreanadeLauncher grenadelauncher;

    public static Action<Zombie> onZombieEnteringPlayersView;
    public static Action<Zombie> onZombieExitingPlayersView;
    public static Action onAllZombiesDead;


    public static Action<float, float> onUpdateHealth;

    public static Action<float> grenadeRelaodTime;

    public static Action<Coin> onCoinCollected;

}

public enum coinType
{
    Bronze,
    Silver,
    Gold
}
