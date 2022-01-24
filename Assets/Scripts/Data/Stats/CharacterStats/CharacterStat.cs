using System;

namespace Data
{

    public abstract class CharacterStat : IStat
    {
        public virtual Type Type { get; }

        public float InitialValue { get; }

        public float CurrentValue => InitialValue + Modifier;

        public float Modifier { get; set; }

        public CharacterStat(float initialValue)
        {
            InitialValue = initialValue;
        }
    }

    public interface IStat
    {
        float InitialValue { get; }
        float CurrentValue { get; }
        public float Modifier { get; set; }
    }

    public class Strength : CharacterStat
    {
        public override Type Type => typeof(Strength);
        public Strength(float initialValue) : base(initialValue)
        {
        }
    }

    public class Dexterity : CharacterStat
    {
        public override Type Type => typeof(Dexterity);
        public Dexterity(float initialValue) : base(initialValue)
        {
        }
    }

    public class Durability : CharacterStat
    {
        public override Type Type => typeof(Durability);
        public Durability(float initialValue) : base(initialValue)
        {
        }
    }

    public class Charisma : CharacterStat
    {
        public override Type Type => typeof(Charisma);

        public Charisma(float initialValue) : base(initialValue)
        {
        }
    }

    public class Speed : CharacterStat
    {
        public override Type Type => typeof(Speed);
        public Speed(float initialValue) : base(initialValue)
        {
        }
    }

    public class Intelligence : CharacterStat
    {
        public override Type Type => typeof(Intelligence);
        public Intelligence(float initialValue) : base(initialValue)
        {
        }
    }

    public class Wisdom : CharacterStat
    {
        public override Type Type => typeof(Wisdom);
        public Wisdom(float initialValue) : base(initialValue)
        {
        }
    }

    
}
