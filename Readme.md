<!-- default file list -->
*Files to look at*:

* [HomeController.cs](./CS/GridViewBatchEdit/Controllers/HomeController.cs) (VB: [HomeController.vb](./VB/GridViewBatchEdit/Controllers/HomeController.vb))
* [Model.cs](./CS/GridViewBatchEdit/Models/Model.cs) (VB: [Model.vb](./VB/GridViewBatchEdit/Models/Model.vb))
* [_GridViewPartial.cshtml](./CS/GridViewBatchEdit/Views/Home/_GridViewPartial.cshtml)
* **[Index.cshtml](./CS/GridViewBatchEdit/Views/Home/Index.cshtml)**
<!-- default file list end -->
# GridView - How to update total summaries on the client side in Batch Edit mode


This example demonstrates how to update total summaries on the client side when GridView is in Batch Edit mode. 
<p>You can find detailed steps by clicking below the "Show Implementation Details" link .<br><strong><br>See Also:<br></strong><a href="https://www.devexpress.com/Support/Center/p/T124603">GridView - Batch Edit - How to calculate values on the fly</a> <br><a href="https://www.devexpress.com/Support/Center/p/T124151">GridView - Batch Edit - How to calculate unbound column and total summary values on the fly</a> <br><br><strong>ASP.NET Web Forms Example:</strong><a href="https://www.devexpress.com/Support/Center/p/T116925"><br></a> <a href="https://www.devexpress.com/Support/Center/p/T114923">ASPxGridView - How to update total summaries on the client side in Batch Edit mode</a> </p>


<h3>Description</h3>

Starting with v16.1, it's possible to handle the&nbsp;<a href="https://documentation.devexpress.com/#AspNet/DevExpressWebScriptsASPxClientGridView_BatchEditRowDeletingtopic">ASPxClientGridView.BatchEditRowDeleting</a>&nbsp; and&nbsp;<a href="https://documentation.devexpress.com/#AspNet/DevExpressWebScriptsASPxClientGridView_BatchEditChangesCancelingtopic">ASPxClientGridView.BatchEditChangesCanceling</a>&nbsp;events to avoid using custom command buttons. (see step.3)<br><br>To implement the required task, perform the following steps:<br><br>
<p>1.&nbsp;Add a total summary item for a required column. The&nbsp;<a href="https://documentation.devexpress.com/#AspNet/DevExpressWebASPxGridViewASPxSummaryItem_Tagtopic">Tag</a>&nbsp;property is used to find this summary item and&nbsp;get its value:&nbsp;</p>


```cs
settings.Columns.Add(column =>
{
	column.FieldName = "C2";
	column.ColumnType = MVCxGridViewColumnType.SpinEdit;

	ASPxSummaryItem summaryItem = new ASPxSummaryItem(column.FieldName, DevExpress.Data.SummaryItemType.Sum);
	summaryItem.Tag = column.FieldName + "_Sum";
	summaryItem.DisplayFormat = "{0}";
	settings.TotalSummary.Add(summaryItem);
});
```


<p>&nbsp;2. Replace&nbsp;the summary item with a custom Footer template:</p>


```cs
	column.SetFooterTemplateContent(c =>
	{
		Html.DevExpress().Label(lbSettings =>
		{
			string fieldName = (c.Column as GridViewDataColumn).FieldName;
			lbSettings.Name = "labelSum";
			lbSettings.Properties.EnableClientSideAPI = true;
			ASPxSummaryItem summaryItem1 = c.Grid.TotalSummary.First(i => i.Tag == (fieldName + "_Sum"));
			lbSettings.Text = c.Grid.GetTotalSummaryValue(summaryItem1).ToString();
		}).Render();
	});
```


<p>&nbsp;</p>
<p>&nbsp;3. &nbsp; Handle the&nbsp;<a href="https://documentation.devexpress.com/#AspNet/DevExpressWebScriptsASPxClientGridView_BatchEditRowDeletingtopic">ASPxClientGridView.BatchEditRowDeleting</a>&nbsp;, <a href="https://documentation.devexpress.com/#AspNet/DevExpressWebScriptsASPxClientGridView_BatchEditEndEditingtopic">ASPxClientGridView.BatchEditEndEditing</a>&nbsp;, &nbsp;<a href="https://documentation.devexpress.com/#AspNet/DevExpressWebScriptsASPxClientGridView_BatchEditChangesCancelingtopic">ASPxClientGridView.BatchEditChangesCanceling</a>&nbsp;events to recalculate summary.&nbsp;</p>


```js
        function OnBatchEditEndEditing(s, e) {
            CalculateSummary(s, e.rowValues, e.visibleIndex, false);
        }
        function CalculateSummary(grid, rowValues, visibleIndex, isDeleting) {
            var originalValue = grid.batchEditApi.GetCellValue(visibleIndex, "C2");
            var newValue = rowValues[(grid.GetColumnByField("C2").index)].value;
            var dif = isDeleting ? -newValue : newValue - originalValue;
            labelSum.SetValue((parseFloat(labelSum.GetValue()) + dif).toFixed(1));
        }
        function OnBatchEditRowDeleting(s, e) {
            CalculateSummary(s, e.rowValues, e.visibleIndex, true);
        }
        function OnChangesCanceling(s, e) {
            if (s.batchEditApi.HasChanges())
                setTimeout(function () {
                    s.Refresh();
                }, 0);
        }
```



<br/>


