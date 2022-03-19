# LoanApplicationSystem
A very basic application to demo utilizing to MS SQL Server, C#, Entity Framework , Web API, ASP.Net Core MVC and few design patterns.

I have considered very basic requirement, due to shortage of time. Assumed that loan application creation with consist of Applicant, Loan and business data and all three entities Must be present for complete application to save.

Guidelines:
Application is configured to run with mutiple startup project (API and web App).

1. Web  Application -> appSetting.json contains api url - if in your local mashine it runs on different port, you can replace in ppSetting.json
"ApplicationConfig": {
    "LoanApplicationServiceURL": "https://localhost:44375/LoanApplicationService/Applicants/"

  }

  2. I have used a Local db, Loan Application Service -> appsetting contains the connection string, in case you want to update.
 i have placed DB mdf file in DB file folder. you can take from there an import use as local db.

  "ConnectionStrings": {
    "LoanApplicationDB": "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=LoanApplicationSystem;Integrated Security=True"
  }
