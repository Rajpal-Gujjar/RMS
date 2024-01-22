using Microsoft.AspNetCore.Components;
using RMS.UI.Models;
using System.Net.Http.Json;

namespace RMS.UI.Pages
{
    public partial class DeletePost
    {
        [Parameter]
        public int userId { get; set; }

        JobPost jobPost = new();
        protected override async Task OnInitializedAsync()
        {
            jobPost = await Http.GetFromJsonAsync<JobPost>("/api/JobPost/" + userId);
        }

        protected async Task RemoveUser(int userID)
        {
            await Http.DeleteAsync("api/JobPost/" + userID);
            navigationManager.NavigateTo("/companyDetails");
        }

        void Cancel()
        {
            navigationManager.NavigateTo("/companyDetails");
        }
    }
}