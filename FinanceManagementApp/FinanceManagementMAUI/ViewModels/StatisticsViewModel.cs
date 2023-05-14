using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FinanceManagementMAUI.Services.Bindings;
using LiveChartsCore;
using LiveChartsCore.Drawing;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.VisualElements;
using SkiaSharp;

namespace FinanceManagementMAUI.ViewModels;
public partial class StatisticsViewModel : ObservableObject
{
    private readonly MutualSimpleTransactionBinding _mutualSimpleTransactionBinding;
    public IEnumerable<ISeries> IncomeByCategorySeries { get; set; } 
    public LabelVisual IncomeByCategoryTitle { get; set; }
    public StatisticsViewModel(MutualSimpleTransactionBinding mutualSimpleTransactionBinding)
    {
        _mutualSimpleTransactionBinding = mutualSimpleTransactionBinding;
        IncomeByCategoryStats();
    }

    void IncomeByCategoryStats()
    {
        try
        {
            var query = _mutualSimpleTransactionBinding.SimpleTransactions.GroupBy(st => st.Category.Name);
            var result = query.Select(g => new
            {
                Name = g.Key,
                Count = g.Count(),
                Percentage = (double)g.Count() / _mutualSimpleTransactionBinding.SimpleTransactions.Count() * 100
            });
            IncomeByCategorySeries = result.AsLiveChartsPieSeries((value, series) =>
            {
                series.Name = $"{value.Name}";
                series.Mapping = (value, p) =>
                {
                    p.PrimaryValue = value.Count;
                };
                series.DataLabelsPaint = new SolidColorPaint(new SKColor(30, 30, 30));
                series.DataLabelsPosition = LiveChartsCore.Measure.PolarLabelsPosition.Outer;
                series.DataLabelsFormatter = p => $"{p.StackedValue.Share:P2}";
            });
            IncomeByCategoryTitle = new LabelVisual
            {
                Text = "Income by category",
                TextSize = 14,
                Padding = new Padding(15),
                Paint = new SolidColorPaint(SKColors.DarkSlateGray)
            };
        }
        catch (Exception e)
        {
            int a = 5;
        }
    }
}
