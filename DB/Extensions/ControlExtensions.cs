using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using _DB;

//x <summary>
//! Contient du code permettant d'étendre les fonctionnalités des classes de System.Windows.Forms
//x </summary>
public static class ControlExtensions
{
    //x <summary>
    //! Texte permettant de déterminer la hauteur maximale que peut avoir une chaîne de caractères selon une certaine police de caractères
    //x </summary>
    private const string c_StringUsedToComputeMaximumHeight = "ÎÓ|ç§jy{[(\\/²_;";

    //x <summary>
    //! Type de méthode ayant pour but de fournir le texte à afficher pour un certain élément
    //x </summary>
    //! <param name="item">Élément dont on veut déterminer un texte représentatif de celui-ci</param>
    public delegate string GetItemTextMethod(object item);

    //x <summary>
    //! Contient les données associées à une ListBox/ComboBox afin de pouvoir en personnaliser l'affichage de ces éléments
    //x </summary>
    //! <typeparam name="TList">Type de contrôle de liste à personnaliser</typeparam>
    private class CustomizeItemTextData<TList>
        where TList : ListControl
    {
        //x <summary>
        //! Registre global de ces données complémentaires par ListBox/ComboBox "personnalisée"
        //x </summary>
        private static Dictionary<TList, CustomizeItemTextData<TList>> Register { get; } = new Dictionary<TList, CustomizeItemTextData<TList>>();

        //x <summary>
        //! Référence de la ListBox/ComboBox personnalisée
        //x </summary>
        private TList m_ListControl;

        //x <summary>
        //! Référence de l'objet Padding pour lequel on peut suivre tout changement de valeur
        //x </summary>
        private ValueChangeable<Padding> m_ChangeablePadding;

        //x <summary>
        //! Méthode permettant de déterminer la taille d'un élément à afficher
        //x </summary>
        private MeasureItemEventHandler m_MeasureItemMethod;

        //x <summary>
        //! Méthode permettant de dessiner un élément à afficher
        //x </summary>
        private DrawItemEventHandler m_DrawItemMethod;

        //x <summary>
        //! Constructeur spécifique
        //x </summary>
        //! <param name="listControl">Référence de la ListBox/ComboBox personnalisée</param>
        //! <param name="changeablePadding">Référence de l'objet Padding pour lequel on peut suivre tout changement de valeur</param>
        //! <param name="measureItemMethod">Méthode permettant de déterminer la taille d'un élément à afficher</param>
        //! <param name="drawItemMethod">Méthode permettant de dessiner un élément à afficher</param>
        private CustomizeItemTextData(TList listControl, ValueChangeable<Padding> changeablePadding, MeasureItemEventHandler measureItemMethod, DrawItemEventHandler drawItemMethod)
        {
            m_ListControl = listControl;
            m_ChangeablePadding = changeablePadding;
            m_MeasureItemMethod = measureItemMethod;
            m_DrawItemMethod = drawItemMethod;
            m_ChangeablePadding.ValueChanged += ChangeablePadding_ValueChanged;
            if (listControl is ListBox)
            {
                var listBox = listControl as ListBox;
                listBox.DrawMode = DrawMode.OwnerDrawVariable;
                listBox.MeasureItem += m_MeasureItemMethod;
                listBox.DrawItem += m_DrawItemMethod;
            }
            else if (listControl is ComboBox)
            {
                var comboBox = listControl as ComboBox;
                comboBox.DrawMode = DrawMode.OwnerDrawVariable;
                comboBox.MeasureItem += m_MeasureItemMethod;
                comboBox.DrawItem += m_DrawItemMethod;
            }
            m_ListControl.Disposed += (s, e) =>
            {
                m_ChangeablePadding.ValueChanged -= ChangeablePadding_ValueChanged;
                Register.Remove(m_ListControl);
            };
            Refresh();
        }

        //x <summary>
        //! Permet de forcer le recalcul de la taille de tous les éléments de la liste
        //x </summary>
        private void Refresh()
        {
            m_ListControl.Visible = false;
            m_ListControl.SuspendLayout();
            if (m_ListControl is ListBox)
            {
                var listBox = m_ListControl as ListBox;
                listBox.DrawMode = DrawMode.Normal;
                listBox.DrawMode = DrawMode.OwnerDrawVariable;
            }
            else if (m_ListControl is ComboBox)
            {
                var comboBox = m_ListControl as ComboBox;
                comboBox.DrawMode = DrawMode.Normal;
                comboBox.DrawMode = DrawMode.OwnerDrawVariable;
            }
            m_ListControl.Invalidate();
            m_ListControl.ResumeLayout(true);
            m_ListControl.Visible = true;
        }

        //x <summary>
        //! Méthode appelée quand l'objet Padding voit sa valeur changer
        //x </summary>
        //! <param name="sender">Objet Padding pour lequel sa valeur a changé</param>
        //! <param name="previousValue">Valeur précédente</param>
        //! <param name="currentValue">Valeur actuelle</param>
        private void ChangeablePadding_ValueChanged(ValueChangeable<Padding> sender, Padding previousValue, Padding currentValue)
        {
            Refresh();
        }

        //x <summary>
        //! Finalise la personnalistation d'affichage des éléments d'une ListBox/ComboBox
        //x </summary>
        private void Terminate()
        {
            m_ChangeablePadding.ValueChanged -= ChangeablePadding_ValueChanged;
            if (m_ListControl is ListBox)
            {
                var listBox = m_ListControl as ListBox;
                listBox.MeasureItem -= m_MeasureItemMethod;
                listBox.DrawItem -= m_DrawItemMethod;
                listBox.DrawMode = DrawMode.Normal;
            }
            else if (m_ListControl is ComboBox)
            {
                var comboBox = m_ListControl as ComboBox;
                comboBox.MeasureItem -= m_MeasureItemMethod;
                comboBox.DrawItem -= m_DrawItemMethod;
                comboBox.DrawMode = DrawMode.Normal;
            }
        }

        //x <summary>
        //! Permet de créer et enregistrer un objet contenant les données complémentaires et nécessaires à la personnalisation d'affichage des éléments d'une ListBox/ComboBox
        //x </summary>
        //! <param name="listControl">Référence de la ListBox/ComboBox personnalisée</param>
        //! <param name="changeablePadding">Référence de l'objet Padding pour lequel on peut suivre tout changement de valeur</param>
        //! <param name="measureItemMethod">Méthode permettant de déterminer la taille d'un élément à afficher</param>
        //! <param name="drawItemMethod">Méthode permettant de dessiner un élément à afficher</param>
        //! <returns>Vrai si la personnalisation a pu être appliquée, sinon faux</returns>
        public static bool Apply(TList listControl, ValueChangeable<Padding> changeablePadding, MeasureItemEventHandler measureItemMethod, DrawItemEventHandler drawItemMethod)
        {
            if (listControl == null) return false;
            if (listControl is ListBox)
            {
                var listBox = listControl as ListBox;
                if (listBox.DrawMode != DrawMode.Normal) return false;
            }
            else if (listControl is ComboBox)
            {
                var comboBox = listControl as ComboBox;
                if (comboBox.DrawMode != DrawMode.Normal) return false;
            }
            if (Register.ContainsKey(listControl) || (changeablePadding == null) || (measureItemMethod == null) || (drawItemMethod == null)) return false;
            Register.Add(listControl, new CustomizeItemTextData<TList>(listControl, changeablePadding, measureItemMethod, drawItemMethod));
            return true;
        }

        //x <summary>
        //! Permet d'annuler la personnalisation d'affichage des éléments d'une ListBox/ComboBox
        //x </summary>
        //! <param name="listControl">Référence de la ListBox/ComboBox personnalisée</param>
        //! <returns>Vrai si l'annulation de personnalisation a pu être faite, sinon faux</returns>
        public static bool Terminate(TList listControl)
        {
            CustomizeItemTextData<TList> customizeItemTextData;
            if (!Register.TryGetValue(listControl, out customizeItemTextData)) return false;
            customizeItemTextData.Terminate();
            return Register.Remove(listControl);
        }
    }

    //x <summary>
    //! Permet de personnaliser l'affichage des éléments de cette ListBox/ComboBox
    //x </summary>
    //! <typeparam name="TList">Type de contrôle de liste à personnaliser</typeparam>
    //! <param name="listControl">Référence de la ListBox/ComboBox personnalisée</param>
    //! <param name="getItemTextMethod">Méthode permettant de fournir le texte à afficher de chaque élément</param>
    //! <param name="changeablePadding">Objet de suivi des marges intérieures des éléments au sein de la liste</param>
    //! <returns>Vrai si la personnalisation a pu être appliquée, sinon faux</returns>
    private static bool CustomizeItemText<TList>(TList listControl, GetItemTextMethod getItemTextMethod, ValueChangeable<Padding> changeablePadding)
        where TList : ListControl
    {
        if (getItemTextMethod == null) getItemTextMethod = (item) => (item != null) ? item.ToString() : string.Empty;
        // Transformation du mode d'affichage en mode "personnalisé" avec hauteur variable
        return CustomizeItemTextData<TList>.Apply(
            listControl,
            changeablePadding,
            (s, e) =>
            {
                e.ItemWidth = (int)e.Graphics.ClipBounds.Width;
                e.ItemHeight = (int)e.Graphics.MeasureString(c_StringUsedToComputeMaximumHeight, listControl.Font).Height + changeablePadding.Value.Vertical;
            },
            (s, e) =>
            {
                var selectedItemBackgroundBrush = SystemBrushes.Highlight;
                var selectedItemTextBrush = SystemBrushes.HighlightText;
                using (var itemBackgroundBrush = new SolidBrush((listControl != null) ? listControl.BackColor : default(Color)))
                {
                    using (var itemTextBrush = new SolidBrush((listControl != null) ? listControl.ForeColor : default(Color)))
                    {
                        // Remplissage du fond de l'élément (en lieu et place de e.DrawBackground())
                        if ((e.State & DrawItemState.Selected) != DrawItemState.Selected)
                        {
                            // Etat normal (avec éventuellement le rectangle de focus)
                            e.Graphics.FillRectangle(itemBackgroundBrush, e.Bounds);
                        }
                        else // ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                        {
                            // Etat sélectionné (avec éventuellement le rectangle de focus)
                            e.Graphics.FillRectangle(selectedItemBackgroundBrush, e.Bounds);
                        }
                        // Si il y a un élément en particulier à "dessiner"
                        if (e.Index >= 0)
                        {
                            // Récupération de l'élément à "transformer en chaîne" avant de l'afficher
                            object item = null;
                            if (listControl is ListBox)
                            {
                                var listBox = listControl as ListBox;
                                item = listBox.Items[e.Index];
                            }
                            else if (listControl is ComboBox)
                            {
                                var comboBox = listControl as ComboBox;
                                item = comboBox.Items[e.Index];
                            }
                            var itemText = getItemTextMethod(item);
                            // Affichage du texte décrivant l'élément selon l'état demandé
                            var padding = changeablePadding.Value;
                            var textRectangle = e.Bounds;
                            textRectangle.X += padding.Left;
                            textRectangle.Y += padding.Top;
                            textRectangle.Width = Math.Max(0, textRectangle.Width - padding.Horizontal);
                            textRectangle.Height = Math.Max(0, textRectangle.Height - padding.Vertical);
                            if ((e.State & DrawItemState.Selected) != DrawItemState.Selected)
                            {
                                // Etat normal (avec éventuellement le rectangle de focus)
                                e.Graphics.DrawString(itemText, e.Font, itemTextBrush, textRectangle);
                            }
                            else // ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                            {
                                // Etat sélectionné (avec éventuellement le rectangle de focus)
                                e.Graphics.DrawString(itemText, e.Font, selectedItemTextBrush, textRectangle);
                            }
                            // Dessin de l'événtuel rectangle de focus entourant cet élément
                            e.DrawFocusRectangle();
                        }
                    }
                }
            });
    }

    //x <summary>
    //! Permet d'annuler la personnalisation de l'affichage des éléments de cette ListBox
    //x </summary>
    //! <param name="listControl">Référence de la ListBox personnalisée</param>
    //! <returns>Vrai si l'annulation de personnalisation a pu être faite, sinon faux</returns>
    public static bool UncustomizeItemText(this ListBox listControl)
    {
        return CustomizeItemTextData<ListBox>.Terminate(listControl);
    }

    //x <summary>
    //! Permet de personnaliser l'affichage des éléments de cette ListBox
    //x </summary>
    //! <param name="listControl">Référence de la ListBox personnalisée</param>
    //! <param name="getItemTextMethod">Méthode permettant de fournir le texte à afficher de chaque élément</param>
    //! <returns>Vrai si la personnalisation a pu être appliquée, sinon faux</returns>
    public static bool CustomizeItemText(this ListBox listControl, GetItemTextMethod getItemTextMethod)
    {
        return CustomizeItemText<ListBox>(listControl, getItemTextMethod, new ValueChangeable<Padding>(Padding.Empty));
    }

    //x <summary>
    //! Permet de personnaliser l'affichage des éléments de cette ListBox
    //x </summary>
    //! <param name="listControl">Référence de la ListBox personnalisée</param>
    //! <param name="getItemTextMethod">Méthode permettant de fournir le texte à afficher de chaque élément</param>
    //! <param name="padding">Marges intérieures des éléments au sein de la liste</param>
    //! <returns>Vrai si la personnalisation a pu être appliquée, sinon faux</returns>
    public static bool CustomizeItemText(this ListBox listControl, GetItemTextMethod getItemTextMethod, Padding padding)
    {
        return CustomizeItemText<ListBox>(listControl, getItemTextMethod, new ValueChangeable<Padding>(padding));
    }

    //x <summary>
    //! Permet de personnaliser l'affichage des éléments de cette ComboBox
    //x </summary>
    //! <param name="listControl">Référence de la ComboBox personnalisée</param>
    //! <param name="getItemTextMethod">Méthode permettant de fournir le texte à afficher de chaque élément</param>
    //! <param name="changeablePadding">Objet de suivi des marges intérieures des éléments au sein de la liste</param>
    //! <returns>Vrai si la personnalisation a pu être appliquée, sinon faux</returns>
    public static bool CustomizeItemText(this ListBox listControl, GetItemTextMethod getItemTextMethod, ValueChangeable<Padding> changeablePadding)
    {
        return CustomizeItemText<ListBox>(listControl, getItemTextMethod, changeablePadding);
    }

    //x <summary>
    //! Permet d'annuler la personnalisation de l'affichage des éléments de cette ComboBox
    //x </summary>
    //! <param name="listControl">Référence de la ComboBox personnalisée</param>
    //! <returns>Vrai si l'annulation de personnalisation a pu être faite, sinon faux</returns>
    public static bool UncustomizeItemText(this ComboBox listControl)
    {
        return CustomizeItemTextData<ComboBox>.Terminate(listControl);
    }

    //x <summary>
    //! Permet de personnaliser l'affichage des éléments de cette ComboBox
    //x </summary>
    //! <param name="listControl">Référence de la ComboBox personnalisée</param>
    //! <param name="getItemTextMethod">Méthode permettant de fournir le texte à afficher de chaque élément</param>
    //! <returns>Vrai si la personnalisation a pu être appliquée, sinon faux</returns>
    public static bool CustomizeItemText(this ComboBox listControl, GetItemTextMethod getItemTextMethod)
    {
        return CustomizeItemText<ComboBox>(listControl, getItemTextMethod, new ValueChangeable<Padding>(Padding.Empty));
    }

    //x <summary>
    //! Permet de personnaliser l'affichage des éléments de cette ComboBox
    //x </summary>
    //! <param name="listControl">Référence de la ComboBox personnalisée</param>
    //! <param name="getItemTextMethod">Méthode permettant de fournir le texte à afficher de chaque élément</param>
    //! <param name="padding">Marges intérieures des éléments au sein de la liste</param>
    //! <returns>Vrai si la personnalisation a pu être appliquée, sinon faux</returns>
    public static bool CustomizeItemText(this ComboBox listControl, GetItemTextMethod getItemTextMethod, Padding padding)
    {
        return CustomizeItemText<ComboBox>(listControl, getItemTextMethod, new ValueChangeable<Padding>(padding));
    }

    //x <summary>
    //! Permet de personnaliser l'affichage des éléments de cette ComboBox
    //x </summary>
    //! <param name="listControl">Référence de la ComboBox personnalisée</param>
    //! <param name="getItemTextMethod">Méthode permettant de fournir le texte à afficher de chaque élément</param>
    //! <param name="changeablePadding">Objet de suivi des marges intérieures des éléments au sein de la liste</param>
    //! <returns>Vrai si la personnalisation a pu être appliquée, sinon faux</returns>
    public static bool CustomizeItemText(this ComboBox listControl, GetItemTextMethod getItemTextMethod, ValueChangeable<Padding> changeablePadding)
    {
        return CustomizeItemText<ComboBox>(listControl, getItemTextMethod, changeablePadding);
    }
}
