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
            if (OperatorType == OperatorType.Add) {
                return $"{StatType.Name} + {AdjustmentValue}";
            } else if (OperatorType == OperatorType.Multiply)
            {
                return $"{StatType.Name} x {AdjustmentValue}";
            }
            return "";
        }
    }
}
