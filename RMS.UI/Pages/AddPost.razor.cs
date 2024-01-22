using Microsoft.AspNetCore.Components;
using RMS.UI.Models;
using System.Net.Http.Json;

namespace RMS.UI.Pages
{
    public partial class AddPost
    {
        [Parameter]
        public int userId { get; set; }
        protected string Title = "Add";
        public JobPost jobPost = new();

        protected override async Task OnParametersSetAsync()
        {
            if (userId != 0)
            {
                Title = "Edit";
                jobPost = await Http.GetFromJsonAsync<JobPost>("api/JobPost/" + userId);
            }
        }

        protected async Task Save()
        {
            if (userId != 0)
            {
                var updatePost = await Http.PutAsJsonAsync("api/JobPost/" + userId, jobPost);
                //bool personResponse = await updatePost.Content.ReadFromJsonAsync<bool>();
                //if (personResponse)
                //{
                //    await JsRuntime.InvokeVoidAsync("alert", "Updated Successfully!");
                //}
                navigationManager.NavigateTo("companyDetails");
            }
            else
            {
                if (jobPost.CompanyId != 0)
                {
                    var newPost = await Http.PostAsJsonAsync("api/JobPost", jobPost);
                    navigationManager.NavigateTo("/companyDetails");
                }
                //bool personResponse = await newPost.Content.ReadFromJsonAsync<bool>();
                //if (personResponse)
                //{
                //    await JsRuntime.InvokeVoidAsync("alert", "Inserted Successfully!");
                //}
            }
        }
        void Cancel()
        {
            if (userId != 0)
            {
                navigationManager.NavigateTo("companyDetails");
            }
            else
            {
                navigationManager.NavigateTo("/");
            }
        }
    }
}