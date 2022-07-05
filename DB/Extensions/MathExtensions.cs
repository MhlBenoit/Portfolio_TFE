public static class MathExtensions
{
    //x <summary>
    //! Tente de déterminer le Plus Grand Commun Diviseur entre ce nombre et celui spécifié
    //! <para>Cette algorithme ne fonctionne qu'avec deux valeurs entières strictement positives ; si ce n'est pas le cas, la méthode retourne une valeur par défaut spécifiée</para>
    //x </summary>
    //! <param name="value">Valeur pour laquelle on recherche le PGCD vis à vis de l'autre valeur spécifiée</param>
    //! <param name="anotherValue">Autre valeur pour laquelle on recherche le PGCD vis à vis de la valeur pour laquelle cette méthode est appelée</param>
    //! <param name="defaultGCD">Valeur par défaut à retourner si au moins une des deux valeurs est négative ou nulle</param>
    //! <returns>Plus grand commun diviseur si il a pu être trouvé, sinon la valeur par défaut</returns>
    public static int GetGCD(this int value, int anotherValue, int defaultGCD = 1)
    {
        return GetGCD(value, anotherValue, out var result) ? result : defaultGCD;
    }

    //x <summary>
    //! Tente de déterminer le Plus Grand Commun Diviseur entre ce nombre et celui spécifié
    //! <para>Cette algorithme ne fonctionne qu'avec deux valeurs entières strictement positives ; si ce n'est pas le cas, la méthode retourne faux</para>
    //x </summary>
    //! <param name="value">Valeur pour laquelle on recherche le PGCD vis à vis de l'autre valeur spécifiée</param>
    //! <param name="anotherValue">Autre valeur pour laquelle on recherche le PGCD vis à vis de la valeur pour laquelle cette méthode est appelée</param>
    //! <param name="gcd">Plus grand commun diviseur si il a pu être trouvé, sinon -1</param>
    //! <returns>Vrai si le Plus Grand Commun Diviseur a pu être déterminé entre les deux valeurs entières, sinon faux</returns>
    public static bool GetGCD(this int value, int anotherValue, out int gcd)
    {
        gcd = -1;
        int minimum, maximum;
        if (value < anotherValue)
        {
            minimum = value;
            maximum = anotherValue;
        }
        else
        {
            minimum = anotherValue;
            maximum = value;
        }
        if (minimum < 0) return false;
        if (minimum == 0)
        {
            gcd = 1;
            return true;
        }
        while (minimum != maximum)
        {
            var difference = maximum - minimum;
            if (difference >= minimum)
            {
                maximum = difference;
            }
            else
            {
                maximum = minimum;
                minimum = difference;
            }
        }
        gcd = minimum;
        return true;
        #region Quelques exemples de cette technique
        /*
        min = 15
        max = 21
        dif = 21 - 15 = 6

        min = 6
        max = 15
        dif = 15 - 6 = 9

        min = 6
        max = 9
        dif = 9 - 6 = 3

        min = 3
        max = 6
        dif = 6 - 3 = 3

        min = 3
        max = 3

        => pgcd = 3

        -----------------

        min = 6
        max = 21
        dif = 15

        min = 6
        max = 15

        .
        .
        .

        => pgcd = 3

        -----------------

        min = 17
        max = 24
        dif = 7

        min = 7
        max = 17
        dif = 10

        min = 7
        max = 10
        dif = 3

        min = 3
        max = 7
        dif = 4

        min = 3
        max = 4
        dif = 1

        min = 1
        max = 3
        dif = 2

        min = 1
        max = 2
        dif = 1

        min = 1
        max = 1

        => pgcd = 1
        */
        #endregion
    }

}
