using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Properties
{
    public const string PLAYER_GAME_OBJECT_NAME = "Player";
    public const string PLAYER_ELEVATION_REFERENCE_NAME = "ElevationReference";
    public const string PLAYER_GROUND_CHECK_NAME = "GroundCheck";
    public const int PLAYER_BULLET = 10;
    public const int PLAYER_BOUNDARY = 9;
    public const int PLAYER_BULLET_SORTING_ORDER = 1;

    public const string ANIMATOR_PLAYER_IS_GROUNDED = "isGrounded";
    public const string ANIMATOR_PLAYER_IS_DUCKING = "isDucking";
    public const string ANIMATOR_PLAYER_IS_RUNNING = "isRunning";

    public const string GROUND_TAG_CANNOT_FALL_NAME = "Ground";
    public const string GROUND_TAG_CAN_FALL_NAME = "Ground - Can Fall";
    public const string GROUND_LAYER_CANNOT_INTERACT_NAME = "Ground - CANNOT INTERACT";
    public const string GROUND_LAYER_CAN_INTERACT_NAME = "Ground - CAN INTERACT";

    public const int GROUND_LAYER_CANNOT_INTERACT_NUMBER = 8;
    public const int GROUND_LAYER_CAN_INTERACT_NUMBER = 7;

    public const string ENEMY_HIT_BOX = "Hit Box";
    public const string STANDING_SHOOTING_ENEMY = "Standing Shooting Enemy Left";
    public const int ENEMY_BOUNDARY = 11;
    public const int ENEMY_PLATFORM = 15;
}
