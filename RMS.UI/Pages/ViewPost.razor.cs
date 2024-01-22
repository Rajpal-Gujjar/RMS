using Microsoft.AspNetCore.Components;
using RMS.UI.Models;
using System.Net.Http.Json;

namespace RMS.UI.Pages
{
    public partial class ViewPost
    {
        [Parameter]
        public int UserId { get; set; }
        public Pagination pagination;
        public List<JobPost> jobPosts;
        public List<JobPost> posts;
        public List<Company> companies;
        int _skip = 0;
        public string Take { get; set; }

        protected override async void OnParametersSet()
        {
            GetPaginationData();
        }

        protected override async Task OnInitializedAsync()
        {
            Take = "5";
            companies = await Http.GetFromJsonAsync<List<Company>>("api/Company");
            GetPaginationData();
        }

        async void NextBtn()
        {
            if (_skip < pagination.TotalCount - int.Parse(Take))
                _skip += int.Parse(Take);
            navigationManager.NavigateTo("/viewPost/" + UserId);
            GetPaginationData();
        }

        async void GetPaginationData()
        {
            try
            {
                pagination = await Http.GetFromJsonAsync<Pagination>($"api/JobPost/PaginationWithId?skip={_skip}&take={int.Parse(Take)}&isPagination=true&id={UserId}");
                jobPosts = pagination.JobPosts;
                StateHasChanged();
            }
            catch (Exception)
            {
                throw;
            }

        }

        protected void PaginationRecordView()
        {
            GetPaginationData();
        }

        async void PreBtn()
        {
            if (_skip >= int.Parse(Take))
                _skip -= int.Parse(Take);
            navigationManager.NavigateTo("/viewPost/" + UserId);
            GetPaginationData();
        }

        void BackPage()
        {
            navigationManager.NavigateTo("/companyDetails");
        }
    }
}