using System;

namespace Data
{
    [Serializable]
    public class StatModifier
    {
        public Type StatType;
        public OperatorType OperatorType;
        public int AdjustmentValue;

        public StatModifier(Type statType, OperatorType operatorType, int adjustmentValue)
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
