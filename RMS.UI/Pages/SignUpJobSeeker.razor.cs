using Microsoft.AspNetCore.Components;
using RMS.UI.Models;
using System.Net.Http.Json;

namespace RMS.UI.Pages
{
    public partial class SignUpJobSeeker
    {
        [Parameter]
        public int userId { get; set; }
        protected string Title = "Add";
        public JobSeeker jobSeeker = new();

        protected override async Task OnParametersSetAsync()
        {
            if (userId != 0)
            {
                Title = "Edit";
                jobSeeker = await Http.GetFromJsonAsync<JobSeeker>("api/JobSeeker/" + userId);
                jobSeeker.ConfirmPassword = jobSeeker.Password;
            }
        }

        protected async Task Save()
        {

            if (userId != 0)
            {
                var updatePost = await Http.PutAsJsonAsync("api/JobSeeker/" + userId, jobSeeker);
                //bool personResponse = await updatePost.Content.ReadFromJsonAsync<bool>();
                //if (personResponse)
                //{
                //    await JsRuntime.InvokeVoidAsync("alert", "Updated Successfully!");
                navigationManager.NavigateTo("jobSeekerDetails/" + userId);
                //}
            }
            else
            {
                jobSeeker.ConfirmPassword = jobSeeker.Password;
                var newPost = Http.PostAsJsonAsync("/api/JobSeeker", jobSeeker);
                //bool personResponse = await newPost.Content.ReadFromJsonAsync<bool>();
                //if (personResponse)
                //{
                //    await JsRuntime.InvokeVoidAsync("alert", "Inserted Successfully!");
                navigationManager.NavigateTo("/");
                //}
            }
        }
        void Cancel()
        {
            if (userId != 0)
            {
                navigationManager.NavigateTo("jobSeekerDetails/" + userId);
            }
            else
            {
                navigationManager.NavigateTo("/");
            }
        }
    }
}