using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;

namespace WebView
{
    public partial class CalenderTime : BaseComponent
    {
        [Parameter] public DateTimeFormatter? DateTimeFormatter { get; set; }
        [Parameter] public DateTime SelectedDate { get; set; }
        [Parameter] public DateTime NavigatedDate { get; set; }
        [Parameter] public EventCallback<NavigatedDateResult> OnNavigateDate { get; set; }

        protected List<Action> SelectMonthCallbacks = new();

        protected override Task OnInitializedAsync()
        {
            for (int i = 0; i < 24; i++)
            {
                int index = i;
                SelectMonthCallbacks.Add(() => OnSelectMonth(index));
            }

            return base.OnInitializedAsync();
        }

        private void OnSelectMonth(int newTime)
        {
           // SelectedDate = SelectedDate.Date.AddHours(newTime);
            OnNavigateDate.InvokeAsync(new NavigatedDateResult() { Date = SelectedDate.Date.AddHours(newTime), FocusOnNavigatedDay = true });
        }
    }


}
