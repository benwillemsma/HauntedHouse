using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterState<Data> : ActorState<Data> where Data : CharacterStateData
{
    public CharacterState(Data characterData) : base(characterData) { }
}
