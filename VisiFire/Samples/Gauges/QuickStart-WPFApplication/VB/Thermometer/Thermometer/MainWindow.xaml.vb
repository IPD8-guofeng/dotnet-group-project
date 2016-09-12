﻿Imports Visifire.Gauges

Class MainWindow

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        CreateGauge()

    End Sub

    Private Sub CreateGauge()

        ' Create a gauge
        Dim gauge As New Gauge()
        gauge.Width = 100
        gauge.Height = 300

        ' Set Type property in Gauge
        gauge.Type = GaugeTypes.Linear

        ' Set DefaultTemplate property in gauge
        gauge.DefaultTemplate = DefaultTemplates.Thermometer

        ' Create a Bar Indicator
        Dim indicator As New BarIndicator()
        indicator.Value = 40

        ' Add indicator to Indicators collection of gauge
        gauge.Indicators.Add(indicator)

        ' Add gauge to the LayoutRoot for display
        LayoutRoot.Children.Add(gauge)

    End Sub

End Class
