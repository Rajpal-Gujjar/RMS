using Microsoft.AspNetCore.Components;
using RMS.UI.Models;
using System.Net.Http.Json;

namespace RMS.UI.Pages
{
    public partial class JobSeekerDetails
    {
        [Parameter]
        public int UserId { get; set; }
        public Pagination pagination;
        public JobSeeker jobSeeker;
        public List<JobPost> posts;
        public List<Company> companies;
        public JobApplied jobApplieds;
        bool _isApplied;
        string _textColor;
        int _skip = 0;
        public string Take { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                if (UserId != 0)
                {
                    Take = "5";
                    companies = await Http.GetFromJsonAsync<List<Company>>("api/Company") ?? new();
                    GetPaginationData();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        async void GetPaginationData()
        {
            try
            {
                jobSeeker = await Http.GetFromJsonAsync<JobSeeker>("api/JobSeeker/" + UserId) ?? new();
                pagination = await Http.GetFromJsonAsync<Pagination>($"api/JobPost/PaginationWithCategory?skip={_skip}&take={int.Parse(Take)}&isPagination=true&category={jobSeeker.Category}");
                posts = pagination.JobPosts;
                StateHasChanged();
            }
            catch (Exception)
            {
                throw;
            }

        }

        async void NextBtn()
        {
            if (_skip < pagination.TotalCount - int.Parse(Take))
                _skip += int.Parse(Take);
            GetPaginationData();
        }

        async void PreBtn()
        {
            if (_skip >= int.Parse(Take))
                _skip -= int.Parse(Take);
            GetPaginationData();
        }

        protected void PaginationRecordView()
        {
            GetPaginationData();
        }

        protected async Task Apply(int postId)
        {
            jobApplieds.JobSeekerId = UserId;
            jobApplieds.JobPostId = postId;
            Http.PostAsJsonAsync("api/JobsApplied", jobApplieds);
            navigationManager.NavigateTo("/jobSeekerDetails/" + UserId);
        }

        void Logout()
        {
            navigationManager.NavigateTo("/logout");
        }

        void EditProfile()
        {
            navigationManager.NavigateTo("signUpJobSeeker/" + UserId);
        }
    }
}