namespace cope
{
    public enum LengthUnit
    {
        //Kilometer,
        Meter,
        Centimeter,
        Decimeter,
        Millimeter,
        //Mycrometer,
        //Nanometer,
        //Picometer,
        //Mile,
        //Inch,
        //HundrethsOfAnInch,
        //ThreeHundrethsOfAnInch,
        //ThousanthsOfAnInch,
        //Twip
    }

    public static class UnitConverter
    {
        public const float CENTIMETER_PER_INCH_F = 2.54f;
        public const decimal CENTIMETER_PER_INCH_DEC = (decimal) 2.54;
        public const float METER_PER_MILE_F = 1609.344f;
        public const decimal METER_PER_MILE_DEC = (decimal) 1609.344;

        public static float InchToCentimeter(float inch)
        {
            return inch * CENTIMETER_PER_INCH_F;
        }

        public static float InchToMillimeter(float inch)
        {
            return inch * CENTIMETER_PER_INCH_F * 10f;
        }

        public static float CentimenterToInch(float centimeter)
        {
            return centimeter / CENTIMETER_PER_INCH_F;
        }

        public static float MilesToMeter(float miles)
        {
            return miles * METER_PER_MILE_F;
        }

        public static float MeterToMiles(float meter)
        {
            return meter / METER_PER_MILE_F;
        }

        public static float ToMillimeter(float value, LengthUnit unit)
        {
            switch (unit)
            {
                case LengthUnit.Centimeter:
                    return value * 10f;
                case LengthUnit.Decimeter:
                    return value * 100f;
                case LengthUnit.Meter:
                    return value * 1000f;
                case LengthUnit.Millimeter:
                    return value;
            }
            return float.NaN;
        }
    }
}