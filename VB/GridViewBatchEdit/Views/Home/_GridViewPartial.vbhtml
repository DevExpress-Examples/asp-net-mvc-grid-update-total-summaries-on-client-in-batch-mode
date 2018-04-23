@Code
    Dim grid = Html.DevExpress().GridView(Sub(settings)
                                                  settings.Name = "GridView"
                                                  settings.CallbackRouteValues = New With {.Controller = "Home", .Action = "GridViewPartial"}
                                                  settings.SettingsEditing.BatchUpdateRouteValues = New With {.Controller = "Home", .Action = "BatchUpdatePartial"}
                                                  settings.SettingsEditing.Mode = GridViewEditingMode.Batch
                                                  settings.Width = 600
                                                  settings.CommandColumn.Visible = True
                                                  settings.CommandColumn.ShowDeleteButton = True
                                                  settings.CommandColumn.ShowNewButtonInHeader = True

                                                  settings.KeyFieldName = "ID"

                                                  settings.Columns.Add("C1")
                                                  settings.Columns.Add(Sub(column)

                                                                               column.FieldName = "C2"
                                                                               column.ColumnType = MVCxGridViewColumnType.SpinEdit
                                                                               column.Width = 120
                                                                               Dim summaryItem As New ASPxSummaryItem(column.FieldName, DevExpress.Data.SummaryItemType.Sum)
                                                                               summaryItem.Tag = column.FieldName & "_Sum"
                                                                               summaryItem.DisplayFormat = "{0}"
                                                                               settings.TotalSummary.Add(summaryItem)

                                                                               column.SetFooterTemplateContent(Sub(c)
                                                                                                                       Html.DevExpress().Label(Sub(lbSettings)

                                                                                                                                                       Dim fieldName As String = (TryCast(c.Column, GridViewDataColumn)).FieldName
                                                                                                                                                       lbSettings.Name = "labelSum"
                                                                                                                                                       lbSettings.Properties.EnableClientSideAPI = True
                                                                                                                                                       Dim summaryItem1 As ASPxSummaryItem = c.Grid.TotalSummary.First(Function(i) i.Tag = (fieldName & "_Sum"))
                                                                                                                                                       lbSettings.Text = c.Grid.GetTotalSummaryValue(summaryItem1).ToString()
                                                                                                                                               End Sub).Render()
                                                                                                               End Sub)
                                                                       End Sub)
                                                  settings.Columns.Add("C3")
                                                  settings.Columns.Add(Sub(column)

                                                                               column.FieldName = "C4"
                                                                               column.ColumnType = MVCxGridViewColumnType.CheckBox
                                                                       End Sub)
                                                  settings.Columns.Add(Sub(column)

                                                                               column.FieldName = "C5"

                                                                               column.ColumnType = MVCxGridViewColumnType.DateEdit
                                                                       End Sub)


                                                  settings.ClientSideEvents.BatchEditEndEditing = "OnBatchEditEndEditing"
                                                  settings.ClientSideEvents.BatchEditRowDeleting = "OnBatchEditRowDeleting"
                                                  settings.ClientSideEvents.BatchEditChangesCanceling = "OnChangesCanceling"

                                                  settings.CellEditorInitialize = Sub(s, e)

                                                                                          Dim editor As ASPxEdit = CType(e.Editor, ASPxEdit)
                                                                                          editor.ValidationSettings.Display = Display.Dynamic
                                                                                  End Sub

                                                  settings.Settings.ShowFooter = True
                                          End Sub)
    If ViewData("EditError") IsNot Nothing Then
        grid.SetEditErrorText(CStr(ViewData("EditError")))
    End If
End Code
@grid.Bind(Model).GetHtml()
