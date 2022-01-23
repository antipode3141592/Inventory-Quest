using System;

namespace Data
{
    [Serializable]
    public class StatModifier
    {
        public Type StatType;
        public OperatorType OperatorType;
        public float AdjustmentValue;

        public StatModifier(Type statType, OperatorType operatorType, float adjustmentValue)
        {
            StatType = statType;
            OperatorType = operatorType;
            AdjustmentValue = adjustmentValue;
        }

        public override string ToString()
        {
            return $"{this.GetType()}: {StatType}, {OperatorType}, {AdjustmentValue}";
        }
    }
}
