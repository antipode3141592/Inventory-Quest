using System;

namespace Data
{

    public abstract class CharacterStat<T>
    {
        public virtual Type Type { get; }

        public float InitialValue { get; }

        public float CurrentValue { get; set; }

        public CharacterStat(float initialValue)
        {
            InitialValue = initialValue;
            CurrentValue = initialValue;
        }
    }

    public class Strength : CharacterStat<Strength>
    {
        public override Type Type => typeof(Strength);
        public Strength(float initialValue) : base(initialValue)
        {
        }
    }

    public class Dexterity : CharacterStat<Dexterity>
    {
        public override Type Type => typeof(Dexterity);
        public Dexterity(float initialValue) : base(initialValue)
        {
        }
    }

    public class Durability : CharacterStat<Durability>
    {
        public override Type Type => typeof(Durability);
        public Durability(float initialValue) : base(initialValue)
        {
        }
    }

    public class Charisma : CharacterStat<Charisma>
    {
        public override Type Type => typeof(Charisma);

        public Charisma(float initialValue) : base(initialValue)
        {
        }
    }

    public class Speed : CharacterStat<Speed>
    {
        public override Type Type => typeof(Speed);
        public Speed(float initialValue) : base(initialValue)
        {
        }
    }

    public class Intelligence : CharacterStat<Intelligence>
    {
        public override Type Type => typeof(Intelligence);
        public Intelligence(float initialValue) : base(initialValue)
        {
        }
    }

    public class Wisdom : CharacterStat<Wisdom>
    {
        public override Type Type => typeof(Wisdom);
        public Wisdom(float initialValue) : base(initialValue)
        {
        }
    }


}
