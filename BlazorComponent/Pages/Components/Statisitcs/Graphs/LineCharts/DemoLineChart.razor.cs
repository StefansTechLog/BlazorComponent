using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorComponent.Pages.Components.Statisitcs.Graphs.LineCharts
{
    public partial class DemoLineChart
    {
        [Inject] IJSRuntime? JSRuntime { get; set; }

        private IJSObjectReference? JsModule { get; set; }


        private int MaxDataPoints { get; set; } = 250;
        private int RerenderFrequency { get; set; } = 5;

        private CancellationTokenSource? CancellationTokenSource;

        private int x = 0;

        protected override async Task OnInitializedAsync()
        {
            //JsModule = await JSRuntime!.InvokeAsync<IJSObjectReference>("import", "/Pages/Components/Statisitcs/Graphs/LineCharts/DemoLineChart.razor.js");
            JsModule = await JSRuntime!.InvokeAsync<IJSObjectReference>("import", $"/js/{nameof(DemoLineChart)}.js");
            await JsModule!.InvokeVoidAsync("InitializeChart", RerenderFrequency, MaxDataPoints);
        }

        private async Task UpdateMaxDataPoints()
        {
            await JsModule!.InvokeVoidAsync("UpdateMaxDataPoints", MaxDataPoints);
        }

        private async Task UpdateRerenderFrequency()
        {
            await JsModule!.InvokeVoidAsync("UpdateRerenderFrequency", RerenderFrequency);
        }

        private async Task StartStream()
        {
            CancellationTokenSource = new CancellationTokenSource();
            while (!CancellationTokenSource!.IsCancellationRequested)
            {
                await JsModule!.InvokeVoidAsync("AddDataPoint", x++, Random.Shared.Next(130, 140));
                await Task.Delay(1);
            }
        }

        private void StopStream()
        {
            CancellationTokenSource?.Cancel();
        }

        private async Task ResetStream()
        {
            CancellationTokenSource?.Cancel();
            await JsModule!.InvokeVoidAsync("ResetDataPoints");
        }
    }
}