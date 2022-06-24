Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Drawing
Imports System.Linq
Imports System.Windows.Forms
Imports DevExpress.DashboardCommon
Imports DevExpress.DashboardCommon.Native
Imports DevExpress.DashboardWin.Localization
Imports DevExpress.DashboardWin.Native
Imports DevExpress.DataAccess.Native
Imports DevExpress.XtraEditors
Imports DevExpress.XtraLayout
Imports DevExpress.XtraMap
Imports DevExpress.XtraMap.Native
Imports DevExpress.XtraMap.Native.SupportSkin

Namespace DevExpresss.DashboardWin.CustomItemExtension.Heatmap
    Partial Public Class HeatmapOptionsControl
        Inherits DashboardUserControl

        Private Shared Function ScaleEquals(ByVal scale1 As ColorizerScale, ByVal scale2 As ColorizerScale) As Boolean
            If scale1 Is Nothing AndAlso scale2 Is Nothing Then
                Return True
            End If
            If (scale1 IsNot Nothing AndAlso scale2 Is Nothing) OrElse (scale1 Is Nothing AndAlso scale2 IsNot Nothing) OrElse (scale1.Count <> scale2.Count) Then
                Return False
            End If
            For i As Integer = 0 To scale1.Count - 1
                If Not scale1(i).Color.Equals(scale2(i).Color) OrElse scale1(i).Range <> scale2(i).Range Then
                    Return False
                End If
            Next i
            Return True
        End Function
        Private Const defaultMarksCount As Integer = 10
        Private ReadOnly locker As New Locker()
        Private ReadOnly autoColors As ColorCollection
        Private ReadOnly absoluteScale As ColorizerScale = Nothing
        Private ReadOnly percentScale As ColorizerScale = Nothing
        Private paletteComparer As New MapPaletteEqualityComparer()
        Private scaleComparer As New MapScaleEqualityComparer()
        Private userColor1 As Color = Color.Empty
        Private userColor2 As Color = Color.Empty
        Private percents As Boolean = True
        Private allowChanges As Boolean = False
        Private userColors As Boolean = False
        Private percentMarksCount As Integer = defaultMarksCount
        Private absoluteMarksCount As Integer = defaultMarksCount
        Private initialScale As MapScale
        Private initialPalette As MapPalette
        Public ReadOnly Property Changed() As Boolean
            Get
                Return Not (scaleComparer.Equals(initialScale, GetScale()) AndAlso paletteComparer.Equals(initialPalette, GetPalette()))
            End Get
        End Property

        Public Event OnChanged As EventHandler

        Public Sub New()
            InitializeComponent()
            autoColors = SkinMapPaletteHolder.GetGradientColors(LookAndFeel)
            userColor1 = autoColors(0)
            userColor2 = autoColors(1)
            absoluteScale = New ColorizerScale(userColor1, userColor2)
            percentScale = New ColorizerScale(userColor1, userColor2)
            AddHandler absoluteScale.ListChanged, AddressOf ScaleListChanged
            AddHandler percentScale.ListChanged, AddressOf ScaleListChanged
            RefreshAbsoluteScale()
            RefreshPercentScale()
            gridControl.DataSource = percentScale
            UpdateUI()
            DirectCast(layoutControl1, ILayoutControl).FakeFocusContainer.Enabled = False
        End Sub
        Private Sub RaiseChanged()
            RaiseEvent OnChanged(Me, EventArgs.Empty)
        End Sub
        Private Sub RefreshAbsoluteScale()
            Dim marks = GetAbsoluteRangeStops(absoluteMarksCount)
            absoluteScale.UpdateScale(marks)
        End Sub

        Private Function GetAbsoluteRangeStops(ByVal marksCount As Integer) As List(Of Double)
            Dim marks = New List(Of Double)(marksCount)
            For i As Integer = 0 To marksCount - 1
                marks.Add(100 * i)
            Next i
            Return marks
        End Function

        Private Sub RefreshPercentScale()
            Dim marks As List(Of Double) = ValueMapScaleHelper.GetPercentRangeStops(percentMarksCount)
            percentScale.UpdateScale(marks)
        End Sub
        Private Sub UpdateUI()
            locker.Lock()
            Try
                colorEdit1.Color = userColor1
                colorEdit2.Color = userColor2
                percentLevelsEdit.Value = Convert.ToDecimal(percentMarksCount)
                absoluteLevelsEdit.Value = Convert.ToDecimal(absoluteMarksCount)
                colorEdit1.Enabled = userColors
                colorEdit2.Enabled = userColors
                customColorsCheckEdit.Checked = userColors
                absoluteLevelsEdit.Enabled = Not percents
                absoluteScaleCheckEdit.Checked = Not percents
                percentLevelsEdit.Enabled = percents
                percentsCheckEdit.Checked = percents
                allowEditCheckEdit.Checked = allowChanges
                gridView.OptionsBehavior.Editable = allowChanges
                gridView.OptionsBehavior.ReadOnly = Not allowChanges
                absoluteLevelsEdit.Enabled = Not (percents OrElse allowChanges)
                percentLevelsEdit.Enabled = percents AndAlso Not allowChanges
                gridControl.DataSource = If(percents, percentScale, absoluteScale)
                gridControl.RefreshDataSource()
            Finally
                locker.Unlock()
            End Try
        End Sub
        Private Sub ScaleListChanged(ByVal sender As Object, ByVal e As ListChangedEventArgs)
            RaiseChanged()
        End Sub
        Private Sub levelsEdit_EditValueChanged(ByVal sender As Object, ByVal e As EventArgs) Handles percentLevelsEdit.EditValueChanged
            If locker.IsLocked Then
                Return
            End If
            percentMarksCount = Convert.ToInt32(percentLevelsEdit.Value)
            RefreshPercentScale()
            UpdateUI()
            RaiseChanged()
        End Sub
        Private Sub customColorsCheckEdit_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles customColorsCheckEdit.CheckedChanged
            If locker.IsLocked Then
                Return
            End If
            userColors = customColorsCheckEdit.Checked
            If userColors Then
                UpdateColors(userColor1, userColor2)
            Else
                UpdateColors(autoColors(0), autoColors(1))
            End If
            UpdateUI()
            RaiseChanged()
        End Sub

        Private Sub UpdateColors(ByVal color1 As Color, ByVal color2 As Color)
            percentScale.Color1 = color1
            absoluteScale.Color1 = color1
            percentScale.Color2 = color2
            absoluteScale.Color2 = color2
        End Sub
        Private Sub colorEdit1_EditValueChanged(ByVal sender As Object, ByVal e As EventArgs) Handles colorEdit1.EditValueChanged
            If locker.IsLocked Then
                Return
            End If
            userColor1 = colorEdit1.Color
            UpdateColors(userColor1, userColor2)
            gridControl.RefreshDataSource()
            RaiseChanged()
        End Sub

        Private Sub colorEdit2_EditValueChanged(ByVal sender As Object, ByVal e As EventArgs) Handles colorEdit2.EditValueChanged
            If locker.IsLocked Then
                Return
            End If
            userColor2 = colorEdit2.Color
            UpdateColors(userColor1, userColor2)
            gridControl.RefreshDataSource()
            RaiseChanged()
        End Sub

        Private Sub percentsCheckEdit_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles absoluteScaleCheckEdit.CheckedChanged, percentsCheckEdit.CheckedChanged
            If locker.IsLocked Then
                Return
            End If
            percents = percentsCheckEdit.Checked
            RaiseChanged()
            UpdateUI()
        End Sub

        Private Sub allowEditCheckEdit_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles allowEditCheckEdit.CheckedChanged
            If locker.IsLocked Then
                Return
            End If
            allowChanges = allowEditCheckEdit.Checked
            If Not allowChanges AndAlso ((percents AndAlso percentScale.HasChanges) OrElse (Not percents AndAlso absoluteScale.HasChanges)) Then
                If XtraMessageBox.Show(LookAndFeel, Me, DashboardWinLocalizer.GetString(DashboardWinStringId.MapLayerOptionsResetCustomScale), DashboardWinLocalizer.GetString(DashboardWinStringId.MessageBoxWarningTitle), MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = DialogResult.No Then
                    allowChanges = True
                    locker.Lock()
                    Try
                        allowEditCheckEdit.Checked = allowChanges
                    Finally
                        locker.Unlock()
                    End Try
                Else
                    If percents Then
                        RefreshPercentScale()
                    Else
                        RefreshAbsoluteScale()
                    End If
                    RaiseChanged()
                End If
            End If
            UpdateUI()
        End Sub

        Private Sub absoluteLevelsSpinEdit_EditValueChanged(ByVal sender As Object, ByVal e As EventArgs) Handles absoluteLevelsEdit.EditValueChanged
            If locker.IsLocked Then
                Return
            End If
            absoluteMarksCount = Convert.ToInt32(absoluteLevelsEdit.Value)
            RefreshAbsoluteScale()
            RaiseChanged()
            UpdateUI()
        End Sub
        Friend Function GetPalette() As MapPalette
            Dim currentScale As ColorizerScale = If(percents, percentScale, absoluteScale)
            If userColors OrElse allowChanges Then
                If currentScale.Any(Function(sc) sc.ColorChanged) Then
                    Dim customPalette As New CustomHeatmapPalette()
                    customPalette.Colors.AddRange(currentScale.Select(Function(cs) cs.Color).ToList())
                    Return customPalette
                ElseIf userColors Then
                    Dim gradientPalette As New GradientHeatmapPalette()
                    gradientPalette.StartColor = userColor1
                    gradientPalette.EndColor = userColor2
                    Return gradientPalette
                End If
            End If
            Return Nothing
        End Function

        Friend Function GetScale() As MapScale
            Dim currentScale As ColorizerScale = If(percents, percentScale, absoluteScale)
            Dim currentRanges As List(Of Double) = currentScale.Select(Function(cs) cs.Range).OrderBy(Function(rangeStop) rangeStop).ToList()
            If Not percents OrElse Not currentRanges.SequenceEqual(ValueMapScaleHelper.GetPercentRangeStops(percentMarksCount)) Then
                Dim scale As CustomScale = New CustomHeatmapScale()
                scale.IsPercent = percents
                scale.RangeStops.AddRange(currentRanges)
                Return scale
            Else
                Dim scale As UniformScale = New UniformHeatmapScale()
                scale.LevelsCount = percentMarksCount
                Return scale
            End If
        End Function

        Friend Sub InitializeFrom(ByVal palette As MapPalette, ByVal scale As MapScale)
            initialPalette = palette
            initialScale = scale
            Dim marks As List(Of Double) = Nothing
            If scale IsNot Nothing Then
                Dim customScale As CustomScale = TryCast(scale, CustomScale)
                If customScale IsNot Nothing Then
                    percents = customScale.IsPercent
                    marks = New List(Of Double)(customScale.RangeStops)
                    percentMarksCount = marks.Count
                    absoluteMarksCount = percentMarksCount
                Else
                    Dim uniformScale As UniformScale = TryCast(scale, UniformScale)
                    If uniformScale IsNot Nothing Then
                        percents = True
                        percentMarksCount = uniformScale.LevelsCount
                    End If
                End If
            End If
            If marks Is Nothing Then
                percentScale.UpdateScale(ValueMapScaleHelper.GetPercentRangeStops(percentMarksCount))
                absoluteScale.UpdateScale(GetAbsoluteRangeStops(absoluteMarksCount))
            Else
                If percents Then
                    percentScale.UpdateScale(marks)
                Else
                    absoluteScale.UpdateScale(marks)
                End If
            End If
            If palette IsNot Nothing Then
                Dim gradientPalette As GradientPalette = TryCast(palette, GradientPalette)
                If gradientPalette IsNot Nothing Then
                    userColors = True
                    userColor1 = gradientPalette.StartColor
                    userColor2 = gradientPalette.EndColor
                    UpdateColors(userColor1, userColor2)
                Else
                    Dim customPalette As CustomPalette = TryCast(palette, CustomPalette)
                    If customPalette IsNot Nothing Then
                        For i As Integer = 0 To customPalette.Colors.Count - 1
                            If i < percentScale.Count Then
                                percentScale(i).Color = customPalette.Colors(i)
                            End If
                            If i < absoluteScale.Count Then
                                absoluteScale(i).Color = customPalette.Colors(i)
                            End If
                        Next i
                    Else
                        userColors = False
                    End If
                End If
            End If
            Dim marksCount As Integer = If(percents, percentMarksCount, absoluteMarksCount)
            allowChanges = Not ScaleEquals(CreateDefaultScale(percents, marksCount), If(percents, percentScale, absoluteScale))
            UpdateUI()
        End Sub
        Private Function CreateDefaultScale(ByVal isPercent As Boolean, ByVal marksCount As Integer) As ColorizerScale
            Dim startColor As Color = If(userColors, userColor1, autoColors(0))
            Dim endColor As Color = If(userColors, userColor2, autoColors(1))
            Dim scale As New ColorizerScale(startColor, endColor)
            Dim marks As List(Of Double) = If(isPercent, ValueMapScaleHelper.GetPercentRangeStops(marksCount), GetAbsoluteRangeStops(marksCount))
            scale.UpdateScale(marks)
            Return scale
        End Function
    End Class

    Public Class ColorizerScale
        Inherits BindingList(Of ColorizerScaleMark)
        Private _color2 As Color
        Private _color1 As Color
        Private _hasChanges As Boolean = False

        Public ReadOnly Property HasChanges() As Boolean
            Get
                Return _hasChanges
            End Get
        End Property
        Public Property Color1() As Color
            Get
                Return _color1
            End Get
            Set(ByVal value As Color)
                If _color1 <> value Then
                    _color1 = value
                    UpdateColors()
                End If
            End Set
        End Property
        Public Property Color2() As Color
            Get
                Return _color2
            End Get
            Set(ByVal value As Color)
                If _color2 <> value Then
                    _color2 = value
                    UpdateColors()
                End If
            End Set
        End Property

        Public Sub New(ByVal color1 As Color, ByVal color2 As Color)
            AddHandler ListChanged, AddressOf ColorizerScale_ListChanged
            Me._color1 = color1
            Me._color2 = color2
            Me.AllowNew = True
        End Sub
        Public Sub UpdateScale(ByVal marks As IList(Of Double))
            Clear()
            For i As Integer = 0 To marks.Count - 1
                Add(New ColorizerScaleMark(marks(i), ValueMapScaleHelper.GetGradientColor(_color1, _color2, i, marks.Count)))
            Next i
            _hasChanges = False
        End Sub
        Protected Overrides Function AddNewCore() As Object
            Dim scaleMark As New ColorizerScaleMark(0, Color.Empty)
            Add(scaleMark)
            Return scaleMark
        End Function


        Private Sub ColorizerScale_ListChanged(ByVal sender As Object, ByVal e As ListChangedEventArgs)
            If (e.ListChangedType = ListChangedType.ItemChanged AndAlso e.PropertyDescriptor.Name = "Range") OrElse e.ListChangedType = ListChangedType.ItemAdded OrElse e.ListChangedType = ListChangedType.ItemDeleted OrElse e.ListChangedType = ListChangedType.ItemMoved Then
                UpdateColors()
            End If
            _hasChanges = True
        End Sub

        Private Sub UpdateColors()
            Dim i As Integer = 0
            Do While i < Count
                Dim mark As ColorizerScaleMark = Me(i)
                If Not mark.ColorChanged Then
                    mark.SetColor(ValueMapScaleHelper.GetGradientColor(_color1, _color2, i, Count))
                End If
                i += 1
            Loop
        End Sub
    End Class
    Public Class ColorizerScaleMark
        Implements INotifyPropertyChanged

        Private _colorChanged As Boolean = False
        Private _color As Color
        Private _range As Double
        Public ReadOnly Property ColorChanged() As Boolean
            Get
                Return _colorChanged
            End Get
        End Property
        Public Property Range() As Double
            Get
                Return _range
            End Get
            Set(ByVal value As Double)
                If _range <> value Then
                    _range = value
                    RaisePropertyChanged("Range")
                End If
            End Set
        End Property
        Public Property Color() As Color
            Get
                Return _color
            End Get
            Set(ByVal value As Color)
                If _color <> value Then
                    SetColor(value)
                    _colorChanged = True
                    RaisePropertyChanged("Color")
                End If
            End Set
        End Property

        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

        Public Sub New(ByVal range As Double, ByVal color As Color)
            Me._range = range
            Me._color = color
        End Sub

        Public Sub SetColor(ByVal color As Color)
            Me._color = color
        End Sub
        Private Sub RaisePropertyChanged(ByVal propertyName As String)
            Dim args As New PropertyChangedEventArgs(propertyName)
            RaiseEvent PropertyChanged(Me, args)
        End Sub
    End Class
End Namespace
