# GridView - How to update total summaries on the client side in Batch Edit mode


This example demonstrates how to update total summaries on the client side when GridView is in Batch Edit mode. 
<p>You can find detailed steps by clicking below the "Show Implementation Details" link .<br><strong><br>See Also:<br></strong><a href="https://www.devexpress.com/Support/Center/p/T124603">GridView - Batch Edit - How to calculate values on the fly</a> <br><a href="https://www.devexpress.com/Support/Center/p/T124151">GridView - Batch Edit - How to calculate unbound column and total summary values on the fly</a> <br><br><strong>ASP.NET Web Forms Example:</strong><a href="https://www.devexpress.com/Support/Center/p/T116925"><br></a> <a href="https://www.devexpress.com/Support/Center/p/T114923">ASPxGridView - How to update total summaries on the client side in Batch Edit mode</a> </p>


<h3>Description</h3>

To implement the required task, perform the following steps:<br><br>
<p>1.&nbsp;Add a total summary item for a required column. The&nbsp;<a href="https://documentation.devexpress.com/#AspNet/DevExpressWebASPxGridViewASPxSummaryItem_Tagtopic">Tag</a>&nbsp;property is used to find this summary item and&nbsp;get its value:&nbsp;</p>
<code lang="cs">settings.Columns.Add(column =&gt;
{
	column.FieldName = "C2";
	column.ColumnType = MVCxGridViewColumnType.SpinEdit;

	ASPxSummaryItem summaryItem = new ASPxSummaryItem(column.FieldName, DevExpress.Data.SummaryItemType.Sum);
	summaryItem.Tag = column.FieldName + "_Sum";
	summaryItem.DisplayFormat = "{0}";
	settings.TotalSummary.Add(summaryItem);
});</code>
<p>&nbsp;2. Replace&nbsp;the summary item with a custom Footer template:</p>
<code lang="cs">	column.SetFooterTemplateContent(c =&gt;
	{
		Html.DevExpress().Label(lbSettings =&gt;
		{
			string fieldName = (c.Column as GridViewDataColumn).FieldName;
			lbSettings.Name = "labelSum";
			lbSettings.Properties.EnableClientSideAPI = true;
			ASPxSummaryItem summaryItem1 = c.Grid.TotalSummary.First(i =&gt; i.Tag == (fieldName + "_Sum"));
			lbSettings.Text = c.Grid.GetTotalSummaryValue(summaryItem1).ToString();
		}).Render();
	});</code>
<p><br>&nbsp;3. Handle the grid's client-side&nbsp;<a href="https://documentation.devexpress.com/#AspNet/DevExpressWebASPxGridViewScriptsASPxClientGridView_BatchEditEndEditingtopic">BatchEditEndEditing</a>&nbsp;event to calculate a new summary value and set it when any cell value has been changed:</p>
<code lang="js">function OnBatchEditEndEditing(s, e) {
    var originalValue = s.batchEditApi.GetCellValue(e.visibleIndex, "C2");
    var newValue = e.rowValues[(s.GetColumnByField("C2").index)].value;

    var dif = newValue - originalValue;
    labelSum.SetValue((parseFloat(labelSum.GetValue()) + dif).toFixed(1));
}</code>
<p>4. Finally, replace standard <em><strong>Save changes</strong></em> and <em><strong>Cancel changes</strong></em> buttons with custom buttons to refresh a summary value when all modifications have been canceled:</p>
<code lang="cs">settings.SetStatusBarTemplateContent(c =&gt;
{
	ViewContext.Writer.Write("&lt;div style='text-align: right'&gt;");

	Html.DevExpress().HyperLink(hlSettings =&gt;
	{
		hlSettings.Name = "hlSave";
		hlSettings.Properties.Text = "Save changes";
		hlSettings.Properties.ClientSideEvents.Click = "function(s, e){ GridView.UpdateEdit(); }";
	}).Render();
	ViewContext.Writer.Write(" ");

	Html.DevExpress().HyperLink(hlSettings =&gt;
	{
		hlSettings.Name = "hlCancel";
		hlSettings.Properties.Text = "Cancel changes";
		hlSettings.Properties.ClientSideEvents.Click = "function(s, e){ GridView.CancelEdit(); GridView.Refresh(); }";
	}).Render();

	ViewContext.Writer.Write("&lt;/div&gt;");
});</code>

<br/>


