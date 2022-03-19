using AutoMapper;
using Contract = LoanApplicationContracts;
using EFModel = LoanApplicationService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using LoanApplicationService.Models;

namespace LoanApplicationService.Controllers
{
    [Route("LoanApplicationService/[Controller]")]
    [ApiController]
    public class ApplicantsController : ControllerBase
    {
        private ILogger<ApplicantsController> _logger;
        private IApplicantRepository _applicantRepository;

        public ApplicantsController(IApplicantRepository applicantRepository, ILogger<ApplicantsController> logger)
        {
            _logger = logger;
            _applicantRepository = applicantRepository;
        }

        [Route("GetAllApplicants")]
        [HttpGet]
        public IActionResult GetAllApplicants()
        {

            var ApplicantsData = _applicantRepository.GetAllApplicants();
            return Ok(ApplicantsData);
        }

        [Route("CreateLoanApplicantion")]
        [HttpPost]
        public IActionResult CreateLoanApplicantion(Contract.LoanApplicationDetails loanApplicationDetails)
        {
            Applicant applicant = _applicantRepository.CreateLoanApplicantion(loanApplicationDetails);

            return Ok(applicant);
        }

        [Route("UpdateLoanApplicantion")]
        [HttpPost]
        public IActionResult UpdateLoanApplicantion(Contract.LoanApplicationDetails loanApplicationDetails)
        {
            Applicant applicant = _applicantRepository.UpdateLoanApplicantion(loanApplicationDetails);
            return Ok(applicant);
        }
        [Route("DeleteApplicant")]
        [HttpDelete]
        public IActionResult DeleteApplicant(int ApplicantId)
        {
            if (ApplicantId == 0)
            {
                return NotFound();
            }

            var applicantRecord =  _applicantRepository.DeleteApplicant(ApplicantId);
            return Ok(applicantRecord);
        }

        [Route("GetApplicantDetails")]
        [HttpGet]
        public IActionResult GetApplicantDetails(int ApplicantId)
        {
            var applicantRecord = _applicantRepository.GetApplicantDetails(ApplicantId);

            return Ok(applicantRecord);
        }
        [Route("SearchApplicants")]
        [HttpPost]
        public IActionResult SearchApplicants(Contract.LoanApplicationSearch searchParams)
        {
            var applicantRecord = _applicantRepository.SearchApplicants(searchParams);
            return Ok(applicantRecord);
        }

        [Route("GetLatestApplicants")]
        [HttpGet]
        public IActionResult GetLatestApplicants(int size = 100)
        {
            if (size <= 0)
                size = 10;  // Min # of records should be 10
            var applicantRecord = _applicantRepository.GetLatestApplicants(size);
            return Ok(applicantRecord);
        }
    }
}
