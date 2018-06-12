﻿using DevExpress.XtraEditors.Controls;
using OSPSuite.Assets;

namespace OSPSuite.UI.RepositoryItems
{
   /// <summary>
   ///    A repository only containing an image used as a button
   /// </summary>
   public class UxRepositoryItemButtonImage : UxRepositoryItemButtonEdit
   {
      public UxRepositoryItemButtonImage(ApplicationIcon applicationIcon, string toolTip = null)
         : base(ButtonPredefines.Glyph)
      {
         TextEditStyle = TextEditStyles.HideTextEditor;
         UpdateButton(Buttons[0], applicationIcon, toolTip);
      }

      public EditorButton AddButton(ApplicationIcon applicationIcon, string toolTip = null)
      {
         var newButton = AddButton(ButtonPredefines.Glyph);
         return UpdateButton(newButton, applicationIcon, toolTip);
      }

      public EditorButton UpdateButton(EditorButton editorButton, ApplicationIcon applicationIcon, string toolTip = null)
      {
         editorButton.Image = applicationIcon;
         editorButton.ToolTip = toolTip;
         return editorButton;
      }
   }
}