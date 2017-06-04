﻿using System.Collections.Generic;
using System.Linq;
using FakeItEasy;
using OSPSuite.BDDHelper;
using OSPSuite.BDDHelper.Extensions;
using OSPSuite.Core.Chart;
using OSPSuite.Core.Domain;
using OSPSuite.Core.Domain.Data;
using OSPSuite.Core.Domain.Mappers;
using OSPSuite.Core.Domain.UnitSystem;
using OSPSuite.Core.Services;
using OSPSuite.Helpers;

namespace OSPSuite.Core
{
   public abstract class concern_for_CurveNamer : ContextSpecification<CurveNamer>
   {
      private IQuantityPathToQuantityDisplayPathMapper _quantityPathToQuantityDisplayPathMapper;
      protected List<ICurveChart> _charts;
      protected ISimulation _simulation;
      protected DataColumn _dataColumn;
      private DataRepository _dataRepository;
      private ICurveChart _curveChart;
      private IDimensionFactory _dimensionFactory;
      protected ICurve _curve;
      protected ICurve _anotherCurve;

      protected override void Context()
      {
         _quantityPathToQuantityDisplayPathMapper = A.Fake<IQuantityPathToQuantityDisplayPathMapper>();
         sut = new CurveNamer(_quantityPathToQuantityDisplayPathMapper);
         _simulation = A.Fake<ISimulation>().WithName("simulationName");
         _dataRepository = DomainHelperForSpecs.SimulationDataRepositoryFor(_simulation.Name);
         _dataColumn = _dataRepository.AllButBaseGrid().First();

         A.CallTo(() => _quantityPathToQuantityDisplayPathMapper.DisplayPathAsStringFor(_simulation, _dataColumn, true)).ReturnsLazily(() => $"{_simulation.Name}:{_dataColumn.Name}");

         _curveChart = new CurveChart();
         _dimensionFactory = A.Fake<IDimensionFactory>();
         _curve = _curveChart.CreateCurve(_dataRepository.BaseGrid, _dataColumn, $"{_simulation.Name}:{_dataColumn.Name}", _dimensionFactory);
         _anotherCurve = _curveChart.CreateCurve(_dataRepository.BaseGrid, _dataColumn, "AnotherColumnName", _dimensionFactory);
         _curveChart.AddCurve(_curve);
         _curveChart.AddCurve(_anotherCurve);
         _charts = new List<ICurveChart> {_curveChart};
      }
   }

   public class When_renaming_curves : concern_for_CurveNamer
   {
      private string _newName;
      protected override void Context()
      {
         base.Context();
         _simulation.Name = "NewSimulationName";
      }

      protected override void Because()
      {
         _newName = sut.CurveNameForColumn(_simulation, _dataColumn);
      }

      [Observation]
      public void the_new_curve_name_should_use_new_simulation_name()
      {
         _newName.ShouldBeEqualTo($"{_simulation.Name}:{_dataColumn.Name}");
      }
   }

   public class When_gathering_curves_that_have_original_name_for_simulation : concern_for_CurveNamer
   {
      private IEnumerable<ICurve> _originallyNamedCurves;

      protected override void Because()
      {
         _originallyNamedCurves = sut.CurvesWithOriginalName(_simulation, _charts);
      }

      [Observation]
      public void the_originally_named_columns_are_returned()
      {
         _originallyNamedCurves.ShouldOnlyContain(_curve);
      }
   }
}