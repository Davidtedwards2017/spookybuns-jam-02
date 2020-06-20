﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameInfo 
{
    public enum Character {
        clockhead,
        demon,
        statue,
        planethead,
        test,
    }

    public enum Expression
    {
        neutral,
        happy,
        angry
    }

    public enum Reincarnate
    {
        humanoid,
        demon,
        cat,
        barnacle
    }
}