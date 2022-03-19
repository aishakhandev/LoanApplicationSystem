using LoanApplicationContracts;
using LoanApplicationWebApplication.Common;
using LoanApplicationWebApplication.ServiceWrapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace LoanApplicationWebApplication.Controllers
{
    public class ApplicantsController : Controller
    {
        private ILoanAppServiceWrapper _loanAppServiceWrapper;
        private ILogger<ApplicantsController> _logger;
        
        public ApplicantsController(IMemoryCache memoryCache, ILoanAppServiceWrapper loanAppServiceWrapper, ILogger<ApplicantsController> logger)
        {
            _loanAppServiceWrapper = loanAppServiceWrapper;
            _logger = logger;
        }
   
        public IActionResult GetApplicantsList()
        {
                          
                List<Applicant> applicantList = (List<Applicant>)_loanAppServiceWrapper.GetApplicantsData();
                return View(applicantList);
           
        }

        public IActionResult GetApplicantDetails(int ApplicantId)
        {
           
            LoanApplicationDetails applicantDetail = (LoanApplicationDetails)_loanAppServiceWrapper.GetApplicantDetails(ApplicantId);
            return View(applicantDetail) ;
        }
      
        [HttpGet]
        public ActionResult SearchApplicants()
        {
            List<LoanApplicationDetails> applicantsList = (List<LoanApplicationDetails>)_loanAppServiceWrapper.GetLatestApplicants(50);
            return View(applicantsList);
        }

        [HttpPost]
        public ActionResult SearchApplicants(LoanApplicationSearch searchPayload)
        {           
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
                // return RedirectToAction("SearchApplicants");
            }

            var applicantsList = (List<LoanApplicationDetails>)_loanAppServiceWrapper.SearchApplicantsData(searchPayload);
            return View(applicantsList);
        }

        [HttpGet]

        public ActionResult CreateApplicant()
        {
            if (TempData["loanApplicationData"] != null)
            {
                LoanApplicationDetails objLoanApp = TempData.Get<LoanApplicationDetails>("loanApplicationData");               
                return View(objLoanApp.applicant);
            }
            else
                return View();
        }

        [HttpGet]
        public ActionResult EditApplicanttemp(int id)
        {          
           LoanApplicationDetails objLoanApp = (LoanApplicationDetails)_loanAppServiceWrapper.GetApplicantDetails(id);
            TempData.Put<LoanApplicationDetails>("loanApplicationEditData", objLoanApp);
            return View("EditApplicant", objLoanApp.applicant);      
           
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public ActionResult EditApplicant(Applicant applicantData, int prev=0)
        {
            if (!ModelState.IsValid)
            {
                    return BadRequest(ModelState);
                    // return RedirectToAction("SearchApplicants");
                }

                LoanApplicationDetails objLoanApp = TempData.Get<LoanApplicationDetails>("loanApplicationEditData");
                if (applicantData != null)
                {
                    if (prev == (int)CreateActions.CreateApplicant) 
                    {
                        objLoanApp.applicant = applicantData;
                        TempData.Put("loanApplicationEditData", objLoanApp);
                       
                    }
                    else
                    {
                        TempData.Put("loanApplicationEditData", objLoanApp);
                        return View(objLoanApp.applicant);
                    }
                }
                return RedirectToAction("EditBusiness");
               
        }
        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public ActionResult CreateApplicant(Applicant applicantData, int prev)
        {
            if (TempData["loanApplicationData"] == null)
            {
                LoanApplicationDetails objLoanApp = new LoanApplicationDetails();
                objLoanApp.applicant = applicantData;                
                TempData.Put("loanApplicationData", objLoanApp);               
                
                return RedirectToAction("CreateBusiness");
            }
            else
            {
                LoanApplicationDetails objLoanApp = TempData.Get<LoanApplicationDetails>("loanApplicationData");
                if (applicantData != null)
                {
                    if (prev == (int)CreateActions.CreateApplicant)
                    {
                        objLoanApp.applicant = applicantData; 
                        TempData.Put("loanApplicationData", objLoanApp);
                        return RedirectToAction("CreateBusiness");
                    }
                    else
                    {
                        TempData.Put("loanApplicationData", objLoanApp);
                        return View(objLoanApp.applicant);
                    }
                 }
                return RedirectToAction("CreateBusiness");

            }
               
          
        }

        [HttpPost]
        public ActionResult CreateApplicantTest(Applicant applicant1)
        {
            TempData["loanApplicationData"] = null;
            if (TempData["loanApplicationData"] == null)
            {
                LoanApplicationDetails objLoanApp = new LoanApplicationDetails();
                objLoanApp.applicant = applicant1;
                TempData.Put("loanApplicationData", objLoanApp);
            }
            if (applicant1.ApplicantId != 0)
            {               
                return View();
            }
            else
                return RedirectToAction("CreateBusiness");
        }

        [HttpGet]
        public ActionResult CreateBusiness()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateBusiness(Business business, int prev)
        {
        
            if (TempData["loanApplicationData"] != null)
            {
                LoanApplicationDetails objLoanApp = TempData.Get<LoanApplicationDetails>("loanApplicationData");
               
                if (prev == (int)CreateActions.CreateBusiness)
                {
                    objLoanApp.business = business;
                    TempData.Put("loanApplicationData", objLoanApp);
                    return RedirectToAction("CreateLoanApplication");
                   
                }
                else if (prev == (int)CreateActions.CreateLoan) 
                {
                     TempData.Put("loanApplicationData", objLoanApp);
                    return View(objLoanApp.business);
                   
                }
                else if (prev == (int)CreateActions.CreateApplicant) 
                {
                   TempData.Put("loanApplicationData", objLoanApp);
                    return RedirectToAction("CreateLoanApplication");

                }
            }
            return RedirectToAction("CreateLoanApplication");
          
        }
        [HttpGet]
        public ActionResult EditBusiness()
        {
            try
            {
                LoanApplicationDetails objLoanApp = TempData.Get<LoanApplicationDetails>("loanApplicationEditData");
                TempData.Put<LoanApplicationDetails>("loanApplicationEditData", objLoanApp);
                return View("EditBusiness", objLoanApp.business);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "Error in EditBusiness Get Action");
                return View("Error", "Error in EditBusiness Get Action, Please check logs for more details");
            }
           
    
        }
        [HttpPost]
        public ActionResult EditBusiness(Business business, int prev)
        {

            try
            {
                LoanApplicationDetails objLoanApp = TempData.Get<LoanApplicationDetails>("loanApplicationEditData");
                if (prev == (int)CreateActions.CreateBusiness) 
                {
                    objLoanApp.business = business;
                    TempData.Put("loanApplicationEditData", objLoanApp);
                    return RedirectToAction("EditLoanApplication");

                }
                else if (prev == (int)CreateActions.CreateLoan) 
                {
                    TempData.Put("loanApplicationEditData", objLoanApp);
                    return View(objLoanApp.business);

                }
                else if (prev == (int)CreateActions.CreateApplicant) 
                {
                     TempData.Put("loanApplicationEditData", objLoanApp);
                    return RedirectToAction("EditLoanApplication");

                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "Error in EditBusiness Post Action");
                return View("Error", "Error in EditBusiness Post Action, Please check logs for more details");
            }
           
               
            return RedirectToAction("EditLoanApplication");

        }
        [HttpGet]
        public ActionResult CreateLoanApplication()
        {
            RandomLoanParamsCalc LoanParamCalculatorObj = new RandomLoanParamsCalc();

            Loan loan = new Loan();
            loan.Apr = LoanParamCalculatorObj.GetAPRRate();



            return View(loan);
        }
        
        [HttpPost]
        public ActionResult CreateLoanApplication(Loan loan)
        {
            LoanApplicationDetails objLoanApp = TempData.Get<LoanApplicationDetails>("loanApplicationData");

            RandomLoanParamsCalc LoanParamCalculatorObj = new RandomLoanParamsCalc();
            loan.CreditRating = LoanParamCalculatorObj.GetCreditRating();
            loan.TotalOutstandingDebt = LoanParamCalculatorObj.GetTotalOutstandingDebt();         
            loan.LatePaymentsin5years = LoanParamCalculatorObj.GetNumberofLatePaymentsinLast5Years();
            loan.NoOfOutstaningDebts = LoanParamCalculatorObj.GetNumberofOutstandingDebts();
            loan.RiskRating = LoanParamCalculatorObj.GetRiskRating();
            loan.DateApplied = DateTime.Now;
            objLoanApp.loan= loan;

            TempData.Put("loanApplicationData", objLoanApp);

            if (ModelState.IsValid)
            {
                _loanAppServiceWrapper.CreateLoanApplication(objLoanApp);
            }
            return RedirectToAction("GetApplicantsList");
        }
        [HttpGet]
        public ActionResult EditLoanApplication()
        {
            LoanApplicationDetails objLoanApp = TempData.Get<LoanApplicationDetails>("loanApplicationEditData");
                
            RandomLoanParamsCalc LoanParamCalculatorObj = new RandomLoanParamsCalc();
            objLoanApp.loan.Apr = LoanParamCalculatorObj.GetAPRRate();
            TempData.Put("loanApplicationEditData", objLoanApp);

            return View(objLoanApp.loan);
        }
        [HttpPost]
        public ActionResult EditLoanApplication(Loan loan)
        {
            try
            {
                LoanApplicationDetails objLoanApp = TempData.Get<LoanApplicationDetails>("loanApplicationEditData");

                //Data that is generated randomlhy
                RandomLoanParamsCalc LoanParamCalculatorObj = new RandomLoanParamsCalc();
                objLoanApp.loan.CreditRating = LoanParamCalculatorObj.GetCreditRating();
                objLoanApp.loan.TotalOutstandingDebt = LoanParamCalculatorObj.GetTotalOutstandingDebt();
                objLoanApp.loan.LatePaymentsin5years = LoanParamCalculatorObj.GetNumberofLatePaymentsinLast5Years();
                objLoanApp.loan.NoOfOutstaningDebts = LoanParamCalculatorObj.GetNumberofOutstandingDebts();
                objLoanApp.loan.RiskRating = LoanParamCalculatorObj.GetRiskRating();
                //Data that came in from View
                objLoanApp.loan.AmountRequested = loan.AmountRequested;
                objLoanApp.loan.NoOfMonthsToPayback = loan.NoOfMonthsToPayback;
                objLoanApp.loan.NoOfYearsToPayback = loan.NoOfYearsToPayback;
                objLoanApp.loan.NoOfOutstaningDebts = loan.NoOfOutstaningDebts;
                objLoanApp.loan.Apr = loan.Apr;

                // put updated object back in TempData
                TempData.Put("loanApplicationEditData", objLoanApp);

                if (ModelState.IsValid)
                {
                    _loanAppServiceWrapper.UpdateLoanApplication(objLoanApp);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "Error in EditLoanApplication Post Action");
                return View("Error", "Error in EditBusiness Post Action, Please check logs for more details");
            }
            return RedirectToAction("GetApplicantsList");
        }
        public ActionResult Delete(int id)
        {
            if (id == 0)
            {
                ModelState.AddModelError("","id is Missing");
            }
            if (id != 0 && ModelState.IsValid)
            {
                _loanAppServiceWrapper.DeleteApplicant(id);              
            }

            return RedirectToAction("GetApplicantsList");
        }
    }

   
}
