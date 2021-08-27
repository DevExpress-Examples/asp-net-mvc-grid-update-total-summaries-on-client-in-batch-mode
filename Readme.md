<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128550781/14.1.5%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T137186)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
<!-- default file list -->
*Files to look at*:

* [HomeController.cs](./CS/GridViewBatchEdit/Controllers/HomeController.cs) (VB: [HomeController.vb](./VB/GridViewBatchEdit/Controllers/HomeController.vb))
* [Model.cs](./CS/GridViewBatchEdit/Models/Model.cs) (VB: [Model.vb](./VB/GridViewBatchEdit/Models/Model.vb))
* [_GridViewPartial.cshtml](./CS/GridViewBatchEdit/Views/Home/_GridViewPartial.cshtml)
* [Index.cshtml](./CS/GridViewBatchEdit/Views/Home/Index.cshtml)
<!-- default file list end -->
# GridView - How to update total summaries on the client side in Batch Edit mode
<!-- run online -->
**[[Run Online]](https://codecentral.devexpress.com/t137186/)**
<!-- run online end -->


This example demonstrates how to update total summaries on the client side when GridView is in Batch Edit mode. 
<p>You can find detailed steps by clicking below the "Show Implementation Details" link .<br><strong><br>See Also:<br></strong><a href="https://www.devexpress.com/Support/Center/p/T124603">GridView - Batch Edit - How to calculate values on the fly</a> <br><a href="https://www.devexpress.com/Support/Center/p/T124151">GridView - Batch Edit - How to calculate unbound column and total summary values on the fly</a> <br><br><strong>ASP.NET Web Forms Example:</strong><a href="https://www.devexpress.com/Support/Center/p/T116925"><br></a> <a href="https://www.devexpress.com/Support/Center/p/T114923">ASPxGridView - How to update total summaries on the client side in Batch Edit mode</a> </p>


<h3>Description</h3>

To implement the required task, perform the following steps:<br><br>
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


<p><br>&nbsp;3. Handle the grid's client-side&nbsp;<a href="https://documentation.devexpress.com/#AspNet/DevExpressWebASPxGridViewScriptsASPxClientGridView_BatchEditEndEditingtopic">BatchEditEndEditing</a>&nbsp;event to calculate a new summary value and set it when any cell value has been changed:</p>


```js
function OnBatchEditEndEditing(s, e) {
    var originalValue = s.batchEditApi.GetCellValue(e.visibleIndex, "C2");
    var newValue = e.rowValues[(s.GetColumnByField("C2").index)].value;

    var dif = newValue - originalValue;
    labelSum.SetValue((parseFloat(labelSum.GetValue()) + dif).toFixed(1));
}
```


<p>4. Finally, replace standard <em><strong>Save changes</strong></em> and <em><strong>Cancel changes</strong></em> buttons with custom buttons to refresh a summary value when all modifications have been canceled:</p>


```cs
settings.SetStatusBarTemplateContent(c =>
{
	ViewContext.Writer.Write("<div style='text-align: right'>");

	Html.DevExpress().HyperLink(hlSettings =>
	{
		hlSettings.Name = "hlSave";
		hlSettings.Properties.Text = "Save changes";
		hlSettings.Properties.ClientSideEvents.Click = "function(s, e){ GridView.UpdateEdit(); }";
	}).Render();
	ViewContext.Writer.Write(" ");

	Html.DevExpress().HyperLink(hlSettings =>
	{
		hlSettings.Name = "hlCancel";
		hlSettings.Properties.Text = "Cancel changes";
		hlSettings.Properties.ClientSideEvents.Click = "function(s, e){ GridView.CancelEdit(); GridView.Refresh(); }";
	}).Render();

	ViewContext.Writer.Write("</div>");
});
```



<br/>


