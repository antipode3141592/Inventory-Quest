﻿using System;

namespace Data
{

    public abstract class CharacterStat : IStat
    {
        public virtual Type Type { get; }

        public int InitialValue { get; }

        public int CurrentValue => InitialValue + Modifier;

        public int Modifier { get; set; }

        public CharacterStat(int initialValue)
        {
            InitialValue = initialValue;
        }
    }

    public interface IStat
    {
        int InitialValue { get; }
        int CurrentValue { get; }
        public int Modifier { get; set; }
    }

    public class Strength : CharacterStat
    {
        public override Type Type => typeof(Strength);
        public Strength(int initialValue) : base(initialValue)
        {
        }
    }

    public class Vitality : CharacterStat
    {
        public override Type Type => typeof(Vitality);
        public Vitality(int initialValue) : base(initialValue)
        {
        }
    }

    public class Agility : CharacterStat
    {
        public override Type Type => typeof(Agility);
        public Agility(int initialValue) : base(initialValue)
        {
        }
    }

    public class Speed : CharacterStat
    {
        public override Type Type => typeof(Speed);
        public Speed(int initialValue) : base(initialValue)
        {
        }
    }

    public class Charisma : CharacterStat
    {
        public override Type Type => typeof(Charisma);

        public Charisma(int initialValue) : base(initialValue)
        {
        }
    }

    public class Intelligence : CharacterStat
    {
        public override Type Type => typeof(Intelligence);
        public Intelligence(int initialValue) : base(initialValue)
        {
        }
    }

    public class Spirit : CharacterStat
    {
        public override Type Type => typeof(Spirit);
        public Spirit(int initialValue) : base(initialValue)
        {
        }
    }

    public class Arcane : CharacterStat
    {
        public override Type Type => typeof(Arcane);

        public Arcane(int initialValue) : base(initialValue)
        {

        }
    }

    
}
