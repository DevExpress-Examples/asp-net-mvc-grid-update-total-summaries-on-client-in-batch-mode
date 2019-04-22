<script type="text/javascript">
    function OnBatchEditEndEditing(s, e) {
        CalculateSummary(s, e.rowValues, e.visibleIndex, false);
    }
    var savedValue;
    function OnEndCallback(s, e) {
        if (!savedValue) return;
        labelSum.SetValue(savedValue);
    }

    function CalculateSummary(grid, rowValues, visibleIndex, isDeleting) {
        var originalValue = grid.batchEditApi.GetCellValue(visibleIndex, "C2");
        var newValue = rowValues[(grid.GetColumnByField("C2").index)].value;
        var dif = isDeleting ? -newValue : newValue - originalValue;
        var sum = (parseFloat(labelSum.GetValue()) + dif).toFixed(1);
        savedValue = sum;
        labelSum.SetValue(sum);
    }
    function OnBatchEditRowDeleting(s, e) {
        CalculateSummary(s, e.rowValues, e.visibleIndex, true);
    }
    function OnChangesCanceling(s, e) {
        if (s.batchEditApi.HasChanges())
            setTimeout(function () {
                savedValue = null;
                s.Refresh();
            }, 0);
    }
</script>
<form>
    @Html.Action("GridViewPartial")
</form>