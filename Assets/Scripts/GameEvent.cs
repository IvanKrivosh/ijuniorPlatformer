using System;
using UnityEngine;
using UnityEngine.Events;

namespace GameEvent
{
    [Serializable] public class IntEvent : UnityEvent<int> { };

    [Serializable] public class CharacterEvent : UnityEvent<Character> { };

    [Serializable] public class TransformEvent : UnityEvent<Transform> { };
}