using RMS.UI.Models;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace RMS.UI.Pages
{
    public partial class Index
    {
        public string _userName;
        public string _password;
        public string validationTextColor;
        public string userValidateMessage;
        public List<JobSeeker> jobSeekers = new();
        public JobSeeker jobSeeker = new();

        async Task SaveAsync()
        {

            if (string.IsNullOrEmpty(_userName) || string.IsNullOrEmpty(_password))
            {
                validationTextColor = "d-block text-danger text-center my-2";
                userValidateMessage = "Email / User Name and Password is not Match";
            }
            else if (!_userName.Contains("@"))
            {
                Admin admin = new();
                admin.UserName = _userName;
                admin.Password = _password;
                var msg = await Http.PostAsJsonAsync("/api/JwtAuth", admin);

                if (msg.IsSuccessStatusCode)
                {
                    var result = msg.Content.ReadAsStringAsync().Result;
                    await localStorage.SetItemAsync("token", result);

                    Http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", result);

                    validationTextColor = "d-none";
                    userValidateMessage = "";
                    navigationManager.NavigateTo("companyDetails");
                }
                else
                {
                    validationTextColor = "d-block text-danger text-center my-2";
                    userValidateMessage = "Email / User Name and Password is not Match";
                }
            }
            else if (_userName.Contains("@"))
            {
                JobSeeker user = new();
                user.Email = _userName;
                user.Password = _password;
                var msg = await Http.PostAsJsonAsync("/api/JwtAuth/JobSeeker", user);
                if (msg.IsSuccessStatusCode)
                {
                    var result = msg.Content.ReadAsStringAsync().Result;
                    await localStorage.SetItemAsync("token", result);

                    Http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", result);
                    jobSeekers = await Http.GetFromJsonAsync<List<JobSeeker>>("api/JobSeeker");
                    JobSeeker person = jobSeekers.FirstOrDefault(x => x.Email == _userName && x.Password == _password);

                    validationTextColor = "d-none";
                    userValidateMessage = "";
                    navigationManager.NavigateTo("jobSeekerDetails/" + person.Id);
                }
                else
                {
                    validationTextColor = "d-block text-danger text-center my-2";
                    userValidateMessage = "Email / User Name and Password is not Match";
                }
            }
        }

        void SignUpDispatchPage()
        {
            navigationManager.NavigateTo("signUpJobSeeker");
        }
    }
}