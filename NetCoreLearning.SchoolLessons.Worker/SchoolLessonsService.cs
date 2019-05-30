using Microsoft.Extensions.Options;
using NetCoreLearning.SchoolLessons.DomainModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreLearning.SchoolLessons.Worker
{
    /// <summary>
    /// Implementation of <see cref="ISchoolLessonsService"/>
    /// </summary>
    /// <seealso cref="NetCoreLearning.SchoolLessons.Worker.ISchoolLessonsService" />
    public class SchoolLessonsService : ISchoolLessonsService
    {
        private string _token;
        private readonly IHttpClientFactory _clientFactory;
        private readonly string _remoteServiceBaseUrl;
        private readonly TokenRequest _tokenRequest;

        /// <summary>
        /// Initializes a new instance of the <see cref="SchoolLessonsService" /> class.
        /// </summary>
        /// <param name="clientFactory">The client factory.</param>
        /// <param name="settings">The settings.</param>
        /// <exception cref="System.ArgumentNullException">httpClient
        /// or
        /// settings
        /// or
        /// Value
        /// or
        /// URL</exception>
        public SchoolLessonsService(IHttpClientFactory clientFactory, IOptions<SchoolLesonsAPISettings> settings)
        {
            _clientFactory = clientFactory ?? throw new ArgumentNullException(nameof(clientFactory));
            if (settings == null) throw new ArgumentNullException(nameof(settings));
            if (settings.Value == null) throw new ArgumentNullException(nameof(settings.Value));
            if (string.IsNullOrWhiteSpace(settings.Value.Url)) throw new ArgumentNullException("URL");
            _remoteServiceBaseUrl = settings.Value.Url;
            _tokenRequest = new TokenRequest() { Username = settings.Value.Username, Password = settings.Value.Password };
        }

        /// <summary>
        /// Authenticates to the API.
        /// </summary>
        public async Task Authenticate()
        {
            var httpClient = _clientFactory.CreateClient();
            var response = await httpClient.PostAsync(
                _remoteServiceBaseUrl + "api/Authentication",
                new StringContent(JsonConvert.SerializeObject(_tokenRequest),
                Encoding.UTF8,
                "application/json"));
            response.EnsureSuccessStatusCode();
            _token = await response.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// Creates the grade assignment asynchronous.
        /// </summary>
        /// <param name="gradeAssigment">The grade assignment.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">gradeAssigment</exception>
        public async Task CreateGradeAssignmentAsync(GradeAssignment gradeAssigment)
        {
            if (gradeAssigment == null) throw new ArgumentNullException(nameof(gradeAssigment));
            var httpClient = CreateClientWithTokenHeader();

            HttpResponseMessage response = await httpClient.PostAsync(
                _remoteServiceBaseUrl + "api/GradesAssignments",
                new StringContent(JsonConvert.SerializeObject(gradeAssigment),
                Encoding.UTF8,
                "application/json"));

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)// if token is expired
            {
                await Authenticate();
                // re post after successful authentication
                response = await httpClient.PostAsync(
                _remoteServiceBaseUrl + "api/GradesAssignments",
                new StringContent(JsonConvert.SerializeObject(gradeAssigment),
                Encoding.UTF8,
                "application/json"));
            }
            response.EnsureSuccessStatusCode();
        }

        /// <summary>
        /// Gets the students asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Student>> GetStudentsAsync()
        {
            var httpClient = CreateClientWithTokenHeader();
            var response = await httpClient.GetStringAsync(_remoteServiceBaseUrl + "api/Students");
            return JsonConvert.DeserializeObject<IEnumerable<Student>>(response);
        }

        /// <summary>
        /// Gets the lessons asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Lesson>> GetLessonsAsync()
        {
            var httpClient = CreateClientWithTokenHeader();
            var response = await httpClient.GetStringAsync(_remoteServiceBaseUrl + "api/Lessons");
            return JsonConvert.DeserializeObject<IEnumerable<Lesson>>(response);
        }

        /// <summary>
        /// Gets the professors asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Professor>> GetProfessorsAsync()
        {
            var httpClient = CreateClientWithTokenHeader();
            var response = await httpClient.GetStringAsync(_remoteServiceBaseUrl + "api/Professors");
            return JsonConvert.DeserializeObject<IEnumerable<Professor>>(response);
        }

        private HttpClient CreateClientWithTokenHeader()
        {
            var httpClient = _clientFactory.CreateClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);
            return httpClient;
        }
    }
}
