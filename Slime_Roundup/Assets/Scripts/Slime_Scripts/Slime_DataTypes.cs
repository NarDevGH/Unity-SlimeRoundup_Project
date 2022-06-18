using System;
using UnityEngine;

namespace Slime_Roundup.Slime.DataTypes
{
    [Serializable]
    struct str_MovementStats
    {
        [Min(0)] public float speed;
        [Min(0)] public float jumpForce;
    }

    [Serializable]
    struct str_RestStats
    {
        [Min(0)] public float minTime;
        [Min(0)] public float maxTime;
        [Range(0, 100)] public float probability;
    }

    [Serializable]
    struct str_jumps
    {
        public int min;
        public int max;
    }
}
