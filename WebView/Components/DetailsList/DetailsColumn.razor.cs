using WebView.Style;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Collections.Generic;

namespace WebView.Lists
{
    public partial class DetailsColumn<TItem> : BaseComponent
    {
        [Parameter]
        public IDetailsRowColumn<TItem>? Column { get; set; }

        [Parameter]
        public RenderFragment<object>? ColumnHeaderTooltipTemplate { get; set; }

        [Parameter]
        public int ColumnIndex { get; set; }

        [Parameter]
        public object? DragDropHelper { get; set; }

        [Parameter]
        public bool IsDraggable { get; set; }

        [Parameter]
        public bool IsDropped { get; set; }

        [Parameter]
        public EventCallback<IDetailsRowColumn<TItem>> OnColumnClick { get; set; }

        [Parameter]
        public EventCallback<IDetailsRowColumn<TItem>> OnColumnContextMenu { get; set; }

        [Parameter]
        public string? ParentId { get; set; }

        [Parameter]
        public EventCallback<int> UpdateDragInfo { get; set; }

        [Parameter]
        public bool UseFastIcons { get; set; } = true;

        private bool HasAccessibleLabel()
        {
            if (Column != null)
            {
                return !string.IsNullOrEmpty(Column.AriaLabel)
                    || !string.IsNullOrEmpty(Column.FilterAriaLabel)
                || !string.IsNullOrEmpty(Column.SortedAscendingAriaLabel)
                || !string.IsNullOrEmpty(Column.SortedDescendingAriaLabel)
                || !string.IsNullOrEmpty(Column.GroupAriaLabel);
            }
            else
                return false;
        }

        private void HandleColumnClick(MouseEventArgs mouseEventArgs)
        {
            if (Column != null)
            {
                if (Column.ColumnActionsMode == ColumnActionsMode.Disabled)
                    return;
                Column.OnColumnClick?.Invoke(Column);
                OnColumnClick.InvokeAsync(Column);
            }
        }
    }
}
