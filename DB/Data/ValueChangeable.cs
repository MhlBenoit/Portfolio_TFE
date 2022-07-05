namespace _DB
{
    //x <summary>
    //! Encapsule une valeur afin de pouvoir en suivre les changements par événement
    //x </summary>
    //! <typeparam name="T">Type de valeur à encapsuler</typeparam>
    public class ValueChangeable<T>
    {
        //x <summary>
        //! Stockage de la valeur encapsulée
        //x </summary>
        private T m_Value;

        //x <summary>
        //! Type de méthode appelée par l'événement de changement de valeur
        //x </summary>
        //! <param name="sender">Objet de suivi de changement de valeur ayant déclenché cet événement</param>
        //! <param name="previousValue">Valeur précédant ce changement</param>
        //! <param name="currentValue">Valeur actuelle après ce changement</param>
        public delegate void ValueChangedMethod(ValueChangeable<T> sender, T previousValue, T currentValue);

        //x <summary>
        //! Événement déclenché en cas de changement de valeur
        //x </summary>
        public event ValueChangedMethod ValueChanged;

        //x <summary>
        //! Valeur
        //x </summary>
        public T Value
        {
            get => m_Value;
            set
            {
                bool hasChanged = false;
                if (value == null)
                {
                    if (m_Value != null)
                    {
                        hasChanged = true;
                    }
                }
                else
                {
                    if (!value.Equals(m_Value))
                    {
                        hasChanged = true;
                    }
                }
                if (hasChanged)
                {
                    if (ValueChanged != null)
                    {
                        T previousValue = m_Value;
                        m_Value = value;
                        ValueChanged(this, previousValue, m_Value);
                    }
                    else
                    {
                        m_Value = value;
                    }
                }
            }
        }

        //x <summary>
        //! Constructeur par défaut
        //x </summary>
        public ValueChangeable()
        {
            m_Value = default(T);
        }

        //x <summary>
        //! Constructeur
        //x </summary>
        //! <param name="value">Valeur initiale</param>
        public ValueChangeable(T value)
        {
            m_Value = value;
        }
    }
}
