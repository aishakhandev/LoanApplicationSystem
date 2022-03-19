using LoanApplicationContracts;
using LoanApplicationWebApplication.Common;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace LoanApplicationWebApplication.ServiceWrapper
{
    public class LoanAppServiceWrapper: ILoanAppServiceWrapper
    {
        private ILogger<LoanAppServiceWrapper> _logger;
        private readonly IMemoryCache _memoryCache;
        IConfiguration _iConfig;
       

        public LoanAppServiceWrapper(ILogger<LoanAppServiceWrapper> logger, IConfiguration iConfig, IMemoryCache memoryCache)
        {
            _logger = logger;
            _memoryCache = memoryCache;
            _iConfig = iConfig;
        }

       static HttpClient client = new HttpClient();
        public IEnumerable<Applicant> GetApplicantsData()
        {
            Task<List<Applicant>> data = GetApplicantAsync();
            return data.Result;   
        }

        public IEnumerable<LoanApplicationDetails> SearchApplicantsData( LoanApplicationSearch searchParams)
        {
            var search = JsonSerializer.Serialize<LoanApplicationSearch>(searchParams);
            Task<List<LoanApplicationDetails>> data = SearchApplicantAsync(search);
            return data.Result;

        }

        public IEnumerable<LoanApplicationDetails> GetLatestApplicants(int size)
        {
            Task<List<LoanApplicationDetails>> data = GetLatestApplicantsAsync( size);
            return data.Result;
        }
      
        public string CreateLoanApplication(LoanApplicationDetails applicationDetails)
        {
            Task<string> data = PostLoanAppDataAsync(applicationDetails, Constants.CREATE_LOAN_APPLICANTION_KEY);
            return data.Result;

        }
        public string UpdateLoanApplication(LoanApplicationDetails applicationDetails)
        {
            Task<string> data =  PostLoanAppDataAsync(applicationDetails, Constants.UPDATE_APPLICANTS_KEY);// UpdateLoanApplicantionAsync( applicationDetails);
            return data.Result;

        }

        public string DeleteApplicant(int applicantId)
        {
            Task<string> data = DeleteApplicantAsync(applicantId);
            return data.Result;

        }

        public LoanApplicationDetails GetApplicantDetails(int applicantId)
        {
            Task<LoanApplicationDetails> data = GetApplicantDetailsAsync(applicantId);
            return data.Result;

        }

        async Task<LoanApplicationDetails> GetApplicantDetailsAsync( int applicantId)
        {
            string applicant = null;
           LoanApplicationDetails applicantData = null;
            string requestURL = String.Concat(GetLoanApplicationServiceURL(), Constants.GET_APPLICANT_DETAILS_KEY, "?applicantId=" , applicantId);
            HttpResponseMessage response = await client.GetAsync(requestURL); //path + "?applicantId=" + applicantId);
            if (response.IsSuccessStatusCode)
            {
                applicant = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions();
                options.PropertyNameCaseInsensitive = true;
                applicantData = JsonSerializer.Deserialize<LoanApplicationDetails>(applicant, options);

            }
            return applicantData;
        }

        async Task<List<LoanApplicationDetails>> SearchApplicantAsync(string search)
        {
            List<LoanApplicationDetails> applicantData=null ;

        
            string requestURL = String.Concat(GetLoanApplicationServiceURL(), Constants.SEARCH_APPLICANTS_KEY);
            HttpResponseMessage response = await client.PostAsync(requestURL, new StringContent(search, Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
            {
               string applicant = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions();
                options.PropertyNameCaseInsensitive = true;
                applicantData = JsonSerializer.Deserialize<List<LoanApplicationDetails>>(applicant, options);

            }
            return applicantData;
        }
        public async Task<string> PostLoanAppDataAsync(LoanApplicationDetails applicantData, string apiKey)
        {
            var applicant1 = JsonSerializer.Serialize<LoanApplicationDetails>(applicantData);
            string requestURL = String.Concat(GetLoanApplicationServiceURL(), apiKey);
            HttpResponseMessage response = await client.PostAsync(requestURL, new StringContent(applicant1, Encoding.UTF8, "application/json"));
            return response.StatusCode.ToString();
        }

        async Task<List<LoanApplicationDetails>> GetLatestApplicantsAsync( int size)
        {
            List<LoanApplicationDetails> applicantData = null;

            string requestURL = String.Concat(GetLoanApplicationServiceURL(), Constants.GET_LATEST_APPLICANTS_KEY, "?size=" , size);
            HttpResponseMessage response = await client.GetAsync(requestURL);
            if (response.IsSuccessStatusCode)
            {
                string applicant = await response.Content.ReadAsStringAsync();
                var options = new JsonSerializerOptions();
                options.PropertyNameCaseInsensitive = true;
                applicantData = JsonSerializer.Deserialize<List<LoanApplicationDetails>>(applicant, options);

            }
            return applicantData;
        }
        public async Task<string> DeleteApplicantAsync( int applicantId)
        {
            string requestURL = String.Concat(GetLoanApplicationServiceURL(), Constants.DELETE_APPLICANTS_KEY, "?applicantId=", applicantId);
            HttpResponseMessage response = await client.DeleteAsync(requestURL);
            return response.StatusCode.ToString();
        }

        public async Task<string> CreateLoanApplicationAsync(LoanApplicationDetails applicantData)
        {
            var applicant1 = JsonSerializer.Serialize<LoanApplicationDetails>(applicantData);
            string requestURL = String.Concat(GetLoanApplicationServiceURL(), Constants.CREATE_LOAN_APPLICANTION_KEY);
            HttpResponseMessage response = await client.PostAsync(requestURL, new StringContent(applicant1, Encoding.UTF8, "application/json"));
            return response.StatusCode.ToString();
        }

        public async Task<string> UpdateLoanApplicantionAsync(LoanApplicationDetails applicantData)
        {

            var applicant1 = JsonSerializer.Serialize<LoanApplicationDetails>(applicantData);
            string requestURL = String.Concat(GetLoanApplicationServiceURL(), Constants.UPDATE_APPLICANTS_KEY);
            HttpResponseMessage response = await client.PostAsync(requestURL, new StringContent(applicant1, Encoding.UTF8, "application/json"));
            return response.StatusCode.ToString();
        }

       
        async Task<List<Applicant>> GetApplicantAsync()
        {
            string applicant = null;
            List<Applicant> applicantData = null;
            string requestURL= String.Concat(GetLoanApplicationServiceURL(),Constants.GET_ALL_APPLICANTS_KEY);
            try
            {
                HttpResponseMessage response = await client.GetAsync(requestURL);
                if (response.IsSuccessStatusCode)
                {
                    applicant = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions();
                    options.PropertyNameCaseInsensitive = true;
                    applicantData = JsonSerializer.Deserialize<List<Applicant>>(applicant, options);
                }
                else
                {
                    _logger.LogError($"Error Code {response.StatusCode} - Inside GetApplicantAsync - Response Error from APi call");                  
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($" {ex.Message} - Inside GetApplicantAsync - Response Error from API call");

            }
            return applicantData;
        }


        async Task<List<Applicant>> GetLoanAppDataAsync(string apiKey)
        {
            string applicant = null;
            List<Applicant> applicantData = null;
            string requestURL = String.Concat(GetLoanApplicationServiceURL(), Constants.GET_ALL_APPLICANTS_KEY);
            try
            {
                HttpResponseMessage response = await client.GetAsync(requestURL);
                if (response.IsSuccessStatusCode)
                {
                    applicant = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions();
                    options.PropertyNameCaseInsensitive = true;
                    applicantData = JsonSerializer.Deserialize<List<Applicant>>(applicant, options);
                }
                else
                {
                    _logger.LogError($"Error Code {response.StatusCode} - Inside GetApplicantAsync - Response Error from APi call");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($" {ex.Message} - Inside GetApplicantAsync - Response Error from API call");

            }
            return applicantData;
        }
        public string GetLoanApplicationServiceURL( )
        {
           
            string content = "https://localhost:44375/LoanApplicationService/Applicants/"; // defaultValue
            var cacheKey = "APIURL";

            //to check if the key exists in cache
            if (!_memoryCache.TryGetValue(cacheKey, out string URlVal))
            {
                try
                {

                    URlVal = _iConfig.GetSection("ApplicationConfig").GetSection(Constants.LOAN_APPLICATION_API_KEY).Value;
                }
                catch (Exception ex)
                {
                    _logger.LogError($" {ex.Message} - Error reading config data for {Constants.LOAN_APPLICATION_API_KEY} from appSettings");
                }

                 var cacheExpiryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddSeconds(6000),
                    Priority = CacheItemPriority.High,
                    SlidingExpiration = TimeSpan.FromSeconds(200)
                };
                //setting cache entries
                _memoryCache.Set(cacheKey, URlVal, cacheExpiryOptions);
            }
            else
                content = _memoryCache.Get(cacheKey).ToString();
           
            return content;
        }
    }
}
