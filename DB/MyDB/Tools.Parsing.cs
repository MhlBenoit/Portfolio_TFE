using System;
using System.Linq;

namespace _DB
{
    //x <summary>
    //! Contient des fonctionnalités utilitaires relatives aux traitements de données
    //x </summary>
    public static partial class Tools
    {
        //x <summary>
        //! Style pour la conversion de chaîne en valeur réelle
        //x </summary>
        private const System.Globalization.NumberStyles c_RealStyle = System.Globalization.NumberStyles.AllowLeadingSign | System.Globalization.NumberStyles.AllowDecimalPoint;

        //x <summary>
        //! Culture régionale de langue anglaise (EN-US)
        //x </summary>
        private static readonly System.Globalization.CultureInfo c_EnglishCulture = System.Globalization.CultureInfo.GetCultureInfo("EN-US");

        //x <summary>
        //! Tente de convertir cette chaîne en une valeur entière (type sbyte)
        //! <para>Méthode d'extension de la classe string</para>
        //! <para>Ignore les espaces de début et de fin de chaîne</para>
        //x </summary>
        //! <param name="Text">Texte pour lequel on tente une conversion en valeur entière (type sbyte)</param>
        //! <param name="Value">Valeur résultant de la conversion si possible, sinon valeur par défaut spécifiée</param>
        //! <param name="Defaultvalue">Valeur par défaut à affecter au paramètre sortant Value si la conversion n'a pas pu être effectuée</param>
        //! <returns>Vrai si la conversion a pu se faire, sinon faux</returns>
        public static bool TryParse(this string Text, out sbyte Value, sbyte Defaultvalue = default(sbyte))
        {
            if ((Text == null) || !sbyte.TryParse(Text.Trim(), out Value))
            {
                Value = Defaultvalue;
                return false;
            }
            return true;
        }

        //x <summary>
        //! Tente de convertir cette chaîne en une valeur entière (type byte)
        //! <para>Méthode d'extension de la classe string</para>
        //! <para>Ignore les espaces de début et de fin de chaîne</para>
        //x </summary>
        //! <param name="Text">Texte pour lequel on tente une conversion en valeur entière (type byte)</param>
        //! <param name="Value">Valeur résultant de la conversion si possible, sinon valeur par défaut spécifiée</param>
        //! <param name="Defaultvalue">Valeur par défaut à affecter au paramètre sortant Value si la conversion n'a pas pu être effectuée</param>
        //! <returns>Vrai si la conversion a pu se faire, sinon faux</returns>
        public static bool TryParse(this string Text, out byte Value, byte Defaultvalue = default(byte))
        {
            if ((Text == null) || !byte.TryParse(Text.Trim(), out Value))
            {
                Value = Defaultvalue;
                return false;
            }
            return true;
        }

        //x <summary>
        //! Tente de convertir cette chaîne en une valeur entière (type short)
        //! <para>Méthode d'extension de la classe string</para>
        //! <para>Ignore les espaces de début et de fin de chaîne</para>
        //x </summary>
        //! <param name="Text">Texte pour lequel on tente une conversion en valeur entière (type short)</param>
        //! <param name="Value">Valeur résultant de la conversion si possible, sinon valeur par défaut spécifiée</param>
        //! <param name="Defaultvalue">Valeur par défaut à affecter au paramètre sortant Value si la conversion n'a pas pu être effectuée</param>
        //! <returns>Vrai si la conversion a pu se faire, sinon faux</returns>
        public static bool TryParse(this string Text, out short Value, short Defaultvalue = default(short))
        {
            if ((Text == null) || !short.TryParse(Text.Trim(), out Value))
            {
                Value = Defaultvalue;
                return false;
            }
            return true;
        }

        //x <summary>
        //! Tente de convertir cette chaîne en une valeur entière (type ushort)
        //! <para>Méthode d'extension de la classe string</para>
        //! <para>Ignore les espaces de début et de fin de chaîne</para>
        //x </summary>
        //! <param name="Text">Texte pour lequel on tente une conversion en valeur entière (type ushort)</param>
        //! <param name="Value">Valeur résultant de la conversion si possible, sinon valeur par défaut spécifiée</param>
        //! <param name="Defaultvalue">Valeur par défaut à affecter au paramètre sortant Value si la conversion n'a pas pu être effectuée</param>
        //! <returns>Vrai si la conversion a pu se faire, sinon faux</returns>
        public static bool TryParse(this string Text, out ushort Value, ushort Defaultvalue = default(ushort))
        {
            if ((Text == null) || !ushort.TryParse(Text.Trim(), out Value))
            {
                Value = Defaultvalue;
                return false;
            }
            return true;
        }


        //x <summary>
        //! Tente de convertir cette chaîne en une valeur entière (type int)
        //! <para>Méthode d'extension de la classe string</para>
        //! <para>Ignore les espaces de début et de fin de chaîne</para>
        //x </summary>
        //! <param name="Text">Texte pour lequel on tente une conversion en valeur entière (type int)</param>
        //! <param name="Value">Valeur résultant de la conversion si possible, sinon valeur par défaut spécifiée</param>
        //! <param name="Defaultvalue">Valeur par défaut à affecter au paramètre sortant Value si la conversion n'a pas pu être effectuée</param>
        //! <returns>Vrai si la conversion a pu se faire, sinon faux</returns>
        public static bool TryParse(this string Text, out int Value, int Defaultvalue = default(int))
        {
            if ((Text == null) || !int.TryParse(Text.Trim(), out Value))
            {
                Value = Defaultvalue;
                return false;
            }
            return true;
        }

        //x <summary>
        //! Tente de convertir cette chaîne en une valeur entière (type uint)
        //! <para>Méthode d'extension de la classe string</para>
        //! <para>Ignore les espaces de début et de fin de chaîne</para>
        //x </summary>
        //! <param name="Text">Texte pour lequel on tente une conversion en valeur entière (type uint)</param>
        //! <param name="Value">Valeur résultant de la conversion si possible, sinon valeur par défaut spécifiée</param>
        //! <param name="Defaultvalue">Valeur par défaut à affecter au paramètre sortant Value si la conversion n'a pas pu être effectuée</param>
        //! <returns>Vrai si la conversion a pu se faire, sinon faux</returns>
        public static bool TryParse(this string Text, out uint Value, uint Defaultvalue = default(uint))
        {
            if ((Text == null) || !uint.TryParse(Text.Trim(), out Value))
            {
                Value = Defaultvalue;
                return false;
            }
            return true;
        }

        //x <summary>
        //! Tente de convertir cette chaîne en une valeur entière (type long)
        //! <para>Méthode d'extension de la classe string</para>
        //! <para>Ignore les espaces de début et de fin de chaîne</para>
        //x </summary>
        //! <param name="Text">Texte pour lequel on tente une conversion en valeur entière (type long)</param>
        //! <param name="Value">Valeur résultant de la conversion si possible, sinon valeur par défaut spécifiée</param>
        //! <param name="Defaultvalue">Valeur par défaut à affecter au paramètre sortant Value si la conversion n'a pas pu être effectuée</param>
        //! <returns>Vrai si la conversion a pu se faire, sinon faux</returns>
        public static bool TryParse(this string Text, out long Value, long Defaultvalue = default(long))
        {
            if ((Text == null) || !long.TryParse(Text.Trim(), out Value))
            {
                Value = Defaultvalue;
                return false;
            }
            return true;
        }

        //x <summary>
        //! Tente de convertir cette chaîne en une valeur entière (type ulong)
        //! <para>Méthode d'extension de la classe string</para>
        //! <para>Ignore les espaces de début et de fin de chaîne</para>
        //x </summary>
        //! <param name="Text">Texte pour lequel on tente une conversion en valeur entière (type ulong)</param>
        //! <param name="Value">Valeur résultant de la conversion si possible, sinon valeur par défaut spécifiée</param>
        //! <param name="Defaultvalue">Valeur par défaut à affecter au paramètre sortant Value si la conversion n'a pas pu être effectuée</param>
        //! <returns>Vrai si la conversion a pu se faire, sinon faux</returns>
        public static bool TryParse(this string Text, out ulong Value, ulong Defaultvalue = default(ulong))
        {
            if ((Text == null) || !ulong.TryParse(Text.Trim(), out Value))
            {
                Value = Defaultvalue;
                return false;
            }
            return true;
        }

        //x <summary>
        //! Tente de convertir cette chaîne en une valeur réelle (type float)
        //! <para>Méthode d'extension de la classe string</para>
        //! <para>Ignore les espaces de début et de fin de chaîne</para>
        //! <para>Accepte aussi bien le point que la virgule comme séparateur décimal</para>
        //x </summary>
        //! <param name="Text">Texte pour lequel on tente une conversion en valeur réelle (type float)</param>
        //! <param name="Value">Valeur résultant de la conversion si possible, sinon valeur par défaut spécifiée</param>
        //! <param name="Defaultvalue">Valeur par défaut à affecter au paramètre sortant Value si la conversion n'a pas pu être effectuée</param>
        //! <returns>Vrai si la conversion a pu se faire, sinon faux</returns>
        public static bool TryParse(this string Text, out float Value, float Defaultvalue = default(float))
        {
            if ((Text == null) || !float.TryParse(Text.Trim().Replace(',', '.'), c_RealStyle, c_EnglishCulture, out Value))
            {
                Value = Defaultvalue;
                return false;
            }
            return true;
        }

        //x <summary>
        //! Tente de convertir cette chaîne en une valeur réelle (type double)
        //! <para>Méthode d'extension de la classe string</para>
        //! <para>Ignore les espaces de début et de fin de chaîne</para>
        //! <para>Accepte aussi bien le point que la virgule comme séparateur décimal</para>
        //x </summary>
        //! <param name="Text">Texte pour lequel on tente une conversion en valeur réelle (type double)</param>
        //! <param name="Value">Valeur résultant de la conversion si possible, sinon valeur par défaut spécifiée</param>
        //! <param name="Defaultvalue">Valeur par défaut à affecter au paramètre sortant Value si la conversion n'a pas pu être effectuée</param>
        //! <returns>Vrai si la conversion a pu se faire, sinon faux</returns>
        public static bool TryParse(this string Text, out double Value, double Defaultvalue = default(double))
        {
            if ((Text == null) || !double.TryParse(Text.Trim().Replace(',', '.'), c_RealStyle, c_EnglishCulture, out Value))
            {
                Value = Defaultvalue;
                return false;
            }
            return true;
        }

        //x <summary>
        //! Tente de convertir cette chaîne en une valeur réelle (type decimal)
        //! <para>Méthode d'extension de la classe string</para>
        //! <para>Ignore les espaces de début et de fin de chaîne</para>
        //! <para>Accepte aussi bien le point que la virgule comme séparateur décimal</para>
        //x </summary>
        //! <param name="Text">Texte pour lequel on tente une conversion en valeur réelle (type decimal)</param>
        //! <param name="Value">Valeur résultant de la conversion si possible, sinon valeur par défaut spécifiée</param>
        //! <param name="Defaultvalue">Valeur par défaut à affecter au paramètre sortant Value si la conversion n'a pas pu être effectuée</param>
        //! <returns>Vrai si la conversion a pu se faire, sinon faux</returns>
        public static bool TryParse(this string Text, out decimal Value, decimal Defaultvalue = default(decimal))
        {
            if ((Text == null) || !decimal.TryParse(Text.Trim().Replace(',', '.'), c_RealStyle, c_EnglishCulture, out Value))
            {
                Value = Defaultvalue;
                return false;
            }
            return true;
        }

        //x <summary>
        //! Modalités de conversion d'une chaîne en DateTime
        //x </summary>
        [Flags]
        public enum DateTimeFlags
        {
            //x <summary>
            //! Indique que la partie date est obligatoire
            //x </summary>
            RequireDatePart = 1,
            //x <summary>
            //! Indique que la partie heure est obligatoire
            //x </summary>
            RequireTimePart = 2,
            //x <summary>
            //! Indique qu'à la fois la partie date et la partie heure sont obligatoires
            //x </summary>
            RequireBothPart = RequireDatePart | RequireTimePart,
            //x <summary>
            //! Autorise la barre de division comme séparateur au sein de la partie date
            //! <para>Séparateur par défaut au sein de la partie date</para>
            //x </summary>
            AllowDateSlash = 4,
            //x <summary>
            //! Autorise le tiret comme séparateur au sein de la partie date
            //x </summary>
            AllowDateDash = 8,
            //x <summary>
            //! Autorise le slash comme séparateur au sein de la partie date
            //! <para>Séparateur par défaut au sein de la partie heure</para>
            //x </summary>
            AllowTimeColon = 16,
            //x <summary>
            //! Autorise les lettres respectives h, m (et éventuellement s) comme séparateur au sein de la partie heure
            //x </summary>
            AllowTimeLetter = 32,
            //x <summary>
            //! Mode de conversion pour obtenir seulement une date
            //! <para>Il ne doit pas y avoir de partie pour l'heure</para>
            //x </summary>
            GetDate = RequireDatePart | AllowDateSlash,
            //x <summary>
            //! Mode de conversion pour obtenir seulement une heure
            //! <para>Il ne doit pas y avoir de partie pour la date</para>
            //x </summary>
            GetTime = RequireTimePart | AllowTimeColon,
            //x <summary>
            //! Mode de conversion pour obtenir une date et une heure
            //! <para>Les deux parties sont obligatoires</para>
            //x </summary>
            GetDateAndTime = GetDate | GetTime,
            //x <summary>
            //! Indique que les secondes sont obligatoires dans la partie heure
            //x </summary>
            RequireSeconds = 256,
            //x <summary>
            //! Modalités par défaut
            //x </summary>
            Default = GetDateAndTime
        }

        //x <summary>
        //! Tente de convertir cette chaîne en une valeur date/heure (type DateTime)
        //! <para>Méthode d'extension de la classe string</para>
        //! <para>Ignore les espaces de début et de fin de chaîne</para>
        //! <para>Utilise les modalités de conversion par défaut</para>
        //x </summary>
        //! <param name="Text">Texte pour lequel on tente une conversion en valeur date/heure (type DateTime)</param>
        //! <param name="Value">Valeur résultant de la conversion si possible, sinon valeur par défaut spécifiée</param>
        //! <param name="Defaultvalue">Valeur par défaut à affecter au paramètre sortant Value si la conversion n'a pas pu être effectuée</param>
        //! <returns>Vrai si la conversion a pu se faire, sinon faux</returns>
        public static bool TryParse(this string Text, out DateTime Value, DateTime Defaultvalue = default(DateTime))
        {
            return Tools.TryParse(Text, out Value, DateTimeFlags.Default, Defaultvalue);
        }

        //x <summary>
        //! Tente de convertir cette chaîne en une valeur date/heure (type DateTime)
        //! <para>Méthode d'extension de la classe string</para>
        //! <para>Ignore les espaces de début et de fin de chaîne</para>
        //x </summary>
        //! <param name="Text">Texte pour lequel on tente une conversion en valeur date/heure (type DateTime)</param>
        //! <param name="Value">Valeur résultant de la conversion si possible, sinon valeur par défaut spécifiée</param>
        //! <param name="Flags">Modalités de conversion</param>
        //! <param name="Defaultvalue">Valeur par défaut à affecter au paramètre sortant Value si la conversion n'a pas pu être effectuée</param>
        //! <returns>Vrai si la conversion a pu se faire, sinon faux</returns>
        public static bool TryParse(this string Text, out DateTime Value, DateTimeFlags Flags, DateTime Defaultvalue = default(DateTime))
        {
            if (string.IsNullOrWhiteSpace(Text) || !TryDateTimeParser(Text.Trim(), Flags, out Value))
            {
                Value = Defaultvalue;
                return false;
            }
            return true;
        }

        #region Méthodes utilitaires qui sont nécessaires à l'implémentation de TryParse pour obtenir un DateTime
        private static bool TryDateTimeParser(string Text, DateTimeFlags Flags, out DateTime Value)
        {
            Value = default(DateTime);
            int Day, Month, Year, Hour, Minute, Second;
            bool RequireDatePart = ((Flags & DateTimeFlags.RequireDatePart) == DateTimeFlags.RequireDatePart);
            bool RequireTimePart = ((Flags & DateTimeFlags.RequireTimePart) == DateTimeFlags.RequireTimePart);
            if (!RequireDatePart && !RequireTimePart) return false;
            bool RequireSeconds = ((Flags & DateTimeFlags.RequireSeconds) == DateTimeFlags.RequireSeconds);
            bool AllowDateDash = ((Flags & DateTimeFlags.AllowDateDash) == DateTimeFlags.AllowDateDash);
            bool AllowDateSlash = ((Flags & DateTimeFlags.AllowDateSlash) == DateTimeFlags.AllowDateSlash) || ((Flags & DateTimeFlags.AllowDateDash) != DateTimeFlags.AllowDateDash);
            string DateSeparators = (AllowDateDash ? "-" : "") + (AllowDateSlash ? "/" : "");
            bool AllowTimeLetter = ((Flags & DateTimeFlags.AllowTimeLetter) == DateTimeFlags.AllowTimeLetter);
            bool AllowTimeColon = ((Flags & DateTimeFlags.AllowTimeColon) == DateTimeFlags.AllowTimeColon) || ((Flags & DateTimeFlags.AllowTimeLetter) != DateTimeFlags.AllowTimeLetter);
            string TimeSeparators_H = (AllowTimeLetter ? "Hh" : "") + (AllowTimeColon ? ":" : "");
            string TimeSeparators_M = (AllowTimeLetter ? "Mm" : "") + (AllowTimeColon ? ":" : "");
            string TimeSeparators_S = AllowTimeLetter ? "Ss" : "";
            int Index = 0;
            if (RequireDatePart)
            {
                char DateSeparator = (char)0;
                if (!TryExtractDateTimeValue(Text, ref Index, out Day, 1, 2)) return false;
                if (!TryDefineSeparator(Text, ref Index, ref DateSeparator, DateSeparators)) return false;
                if (!TryExtractDateTimeValue(Text, ref Index, out Month, 1, 2)) return false;
                if (!TryDefineSeparator(Text, ref Index, ref DateSeparator, DateSeparators)) return false;
                if (!TryExtractDateTimeValue(Text, ref Index, out Year, 4, 4)) return false;
                if ((Day < 1) || (Day > 31) || (Month < 1) || (Month > 12) || (Year < DateTime.MinValue.Year) || (Year > DateTime.MaxValue.Year)) return false;
                if (Day > DateTime.DaysInMonth(Year, Month)) return false;
                if (RequireTimePart)
                {
                    string DateTimeSeparators = " ";
                    char DateTimeSeparator = (char)0;
                    if (!TryDefineSeparator(Text, ref Index, ref DateTimeSeparator, DateTimeSeparators)) return false;
                    while (TryDefineSeparator(Text, ref Index, ref DateTimeSeparator, DateTimeSeparators)) ;
                }
            }
            else
            {
                Day = 1;
                Month = 1;
                Year = 1;
            }
            if (RequireTimePart)
            {
                char TimeSeparator = (char)0;
                if (!TryExtractDateTimeValue(Text, ref Index, out Hour, 1, 2)) return false;
                if (!TryDefineSeparator(Text, ref Index, ref TimeSeparator, TimeSeparators_H)) return false;
                if (!TryExtractDateTimeValue(Text, ref Index, out Minute, 1, 2)) return false;
                if (!TryDefineSeparator(Text, ref Index, ref TimeSeparator, TimeSeparators_M))
                {
                    if (RequireSeconds) return false;
                    Second = 0;
                }
                else
                {
                    if (!TryExtractDateTimeValue(Text, ref Index, out Second, 1, 2))
                    {
                        if (RequireSeconds) return false;
                        Second = 0;
                    }
                    if (RequireSeconds && char.IsLetter(TimeSeparator) && !TryDefineSeparator(Text, ref Index, ref TimeSeparator, TimeSeparators_S)) return false;
                }
                if ((Hour < 0) || (Hour > 23) || (Minute < 0) || (Minute > 59) || (Second < 0) || (Second > 59)) return false;
            }
            else
            {
                Hour = 0;
                Minute = 0;
                Second = 0;
            }
            Value = new DateTime(Year, Month, Day, Hour, Minute, Second);
            return true;
        }

        private static bool TryDefineSeparator(string Text, ref int Index, ref char ExpectedSeparator, string AllowedSeparators)
        {
            if (Index >= Text.Length) return false;
            if (!AllowedSeparators.Contains(Text[Index])) return false;
            if (ExpectedSeparator == (char)0)
            {
                ExpectedSeparator = Text[Index];
                Index++;
                return true;
            }
            else if (!char.IsLetter(ExpectedSeparator))
            {
                if (Text[Index] != ExpectedSeparator) return false;
                Index++;
                return true;
            }
            else
            {
                if (!char.IsLetter(Text[Index])) return false;
                Index++;
                return true;
            }
        }

        private static bool TryExtractDateTimeValue(string Text, ref int Index, out int Value, int MinimumLength, int MaximumLength)
        {
            Value = int.MinValue;
            if (Index >= Text.Length) return false;
            int StartIndex = Index;
            while ((Index < Text.Length) && char.IsDigit(Text[Index])) Index++;
            int Length = Index - StartIndex;
            if ((Length < MinimumLength) || (Length > MaximumLength)) return false;
            Value = int.Parse(Text.Substring(StartIndex, Length));
            return true;
        }
        #endregion
    }
}
