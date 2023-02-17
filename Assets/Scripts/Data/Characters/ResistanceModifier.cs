using Data.Health;

namespace Data.Characters
{
    public class ResistanceModifier
    {
        public DamageType ResistanceType;
        public OperatorType OperatorType;
        public int AdjustmentValue;

        public ResistanceModifier(DamageType resistanceType, OperatorType operatorType, int adjustmentValue)
        {
            ResistanceType = resistanceType;
            OperatorType = operatorType;
            AdjustmentValue = adjustmentValue;
        }

        public override string ToString()
        {
            if (OperatorType == OperatorType.Add)
            {
                return $"+{AdjustmentValue} {ResistanceType} resistance";
            }
            else if (OperatorType == OperatorType.Multiply)
            {
                return $"x{AdjustmentValue} {ResistanceType} resistance";
            }
            return "";
        }
    }
}