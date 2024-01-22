using Microsoft.AspNetCore.Components;
using RMS.UI.Models;
using System.Net.Http.Json;

namespace RMS.UI.Pages
{
    public partial class CompanyDetails
    {

        [Parameter]
        public int UserId { get; set; }
        public List<JobPost> jobPostsTotal;
        public JobPost? jobPost;
        public List<Company>? comp;
        public List<JobSeeker> jobSeekers = new();
        public List<JobApplied>? jobApplied;
        public int totalApplieds;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                comp = await Http.GetFromJsonAsync<List<Company>>("api/Company");
                jobPostsTotal = await Http.GetFromJsonAsync<List<JobPost>>($"api/JobPost?skip=0&take=0&isPagination={false}");

                jobSeekers = await Http.GetFromJsonAsync<List<JobSeeker>>("api/JobSeeker");
                jobApplied = await Http.GetFromJsonAsync<List<JobApplied>>("api/JobApplied");
                totalApplieds=jobApplied.Count();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        void Logout()
        {
            navigationManager.NavigateTo("/");
        }

        protected async Task ViewPosts()
        {
            navigationManager.NavigateTo("/viewPost");
        }
        public void Cancel()
        {
            navigationManager.NavigateTo("/companyDetails");
        }

        void AddPostDispatchPage()
        {
            navigationManager.NavigateTo("/addPost");
        }
    }
}