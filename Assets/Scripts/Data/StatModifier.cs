using System;

namespace Data
{
    [Serializable]
    public class StatModifier
    {
        public string StatName;
        public OperatorType OperatorType;
        public float AdjustmentValue;

        public StatModifier(string statName, OperatorType operatorType, float adjustmentValue)
        {
            StatName = statName;
            OperatorType = operatorType;
            AdjustmentValue = adjustmentValue;
        }
    }
}
