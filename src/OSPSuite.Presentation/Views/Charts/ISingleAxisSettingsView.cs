﻿using OSPSuite.Core.Chart;
using OSPSuite.Presentation.Presenters.Charts;

namespace OSPSuite.Presentation.Views.Charts
{
   public interface ISingleAxisSettingsView : IView<ISingleAxisSettingsPresenter>
   {
      void BindToSource(Axis axis);

      /// <summary>
      /// Hides default color and default line style editors.
      /// </summary>
      void HideDefaultStyles();
   }
}