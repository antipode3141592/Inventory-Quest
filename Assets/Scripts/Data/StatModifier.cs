using System;

namespace Data
{
    [Serializable]
    public class StatModifier
    {
        public StatType Stat;
        public OperatorType OperatorType;
        public float AdjustmentValue;

        public StatModifier(StatType stat, OperatorType operatorType, float adjustmentValue)
        {
            Stat = stat;
            OperatorType = operatorType;
            AdjustmentValue = adjustmentValue;
        }
    }
}
