using Data.Characters;
using System;

namespace Data
{
    [Serializable]
    public class StatModifier
    {
        public StatTypes StatType;
        public OperatorType OperatorType;
        public int AdjustmentValue;

        public StatModifier(StatTypes statType, OperatorType operatorType, int adjustmentValue)
        {
            StatType = statType;
            OperatorType = operatorType;
            AdjustmentValue = adjustmentValue;
        }

        public override string ToString()
        {
            if (OperatorType == OperatorType.Add) {
                return $"{StatType} + {AdjustmentValue}";
            } else if (OperatorType == OperatorType.Multiply)
            {
                return $"{StatType} x {AdjustmentValue}";
            }
            return "";
        }
    }
}
