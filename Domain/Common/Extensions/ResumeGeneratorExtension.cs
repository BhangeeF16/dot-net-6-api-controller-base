using Domain.Common.Models.CandidateModule;
using Domain.Common.Models.UserModule;
using Domain.Common.Utilities;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.Net.Http;
using PageSize = iTextSharp.text.PageSize;

namespace Domain.Common.Extensions
{
    public static class ResumeGeneratorExtension
    {
        public const string JobExperienceListItem = "JobExpeienceListItem";
        public const string EducationExperienceListItem = "EducationExpeienceListItem";
        public const string ResumeTemplateName = "ResumeTemplateForCVGenrator";

        public static string GenerateResumeHtml(this GenerateResumeModel model)
        {
            var cvBuiler = new StringBuilder(GetHtmlTemplate(ResumeTemplateName));
            cvBuiler.SetCandidateDetails(model.Candidate, model.FirstName, model.LastName)
                    .Replace("{{jobExperienceListItems}}", string.Join(' ', model.JobExperiences.Select(x => x.SetJobExperienceItem())))
                    .Replace("{{educationExperienceListItems}}", string.Join(' ', model.EducationExperiences.Select(x => x.SetEducationExperienceItem())));

            var hpdf = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $@"Common\HtmlTemplates\test.pdf");
            File.WriteAllBytes(hpdf, GenratePdfBytes(cvBuiler.ToString()));

            return cvBuiler.ToString();
        }
        public static StringBuilder SetCandidateDetails(this StringBuilder cvBuiler, UserCandidateProfileDto candidate, string FirstName, string LastName)
        {
            cvBuiler.Replace("{{firstName}}", FirstName)
                    .Replace("{{lastName}}", LastName)
                    .Replace("{{phoneNumber}}", candidate.ContactPhone)
                    .Replace("{{email}}", candidate.ContactEmail)
                    .Replace("{{linkedInUserName}}", candidate.LinkedInUserName)
                    .Replace("{{about}}", candidate.About)
                    .Replace("{{facecbookUserName}}", candidate.FacecbookUserName)
                    .Replace("{{jobTitle}}", candidate.JobTitle);
            return cvBuiler;
        }
        public static string SetJobExperienceItem(this JobExperienceDto jobExperience)
        {
            var jobItemBuilder = new StringBuilder(GetHtmlTemplate(JobExperienceListItem));
            jobItemBuilder.Replace("{{corporateName}}", jobExperience.CorporateName)
                          .Replace("{{roleName}}", jobExperience.RoleName)
                          .Replace("{{fromYear}}", jobExperience.StartedAt.Year.ToString())
                          .Replace("{{toYear}}", jobExperience.IsPresentExperience ? "Present" : jobExperience.EndedAt.Value.Year.ToString())
                          .Replace("{{details}}", jobExperience.Details);
            return jobItemBuilder.ToString();
        }
        public static string SetEducationExperienceItem(this EducationExperienceDto educationExperience)
        {
            var educationItemBuilder = new StringBuilder(GetHtmlTemplate(EducationExperienceListItem));
            educationItemBuilder.Replace("{{instituteName}}", educationExperience.InstitueName)
                          .Replace("{{educationLevel}}", educationExperience.LevelOfEdujcation.GetDescription())
                          .Replace("{{subject}}", educationExperience.EducationSubject.Subject)
                          .Replace("{{fromYear}}", educationExperience.StartedAt.Year.ToString())
                          .Replace("{{toYear}}", educationExperience.IsPresentExperience ? "Present" : educationExperience.EndedAt.Value.Year.ToString())
                          .Replace("{{details}}", educationExperience.Details);
            return educationItemBuilder.ToString();
        }
        private static string GetHtmlTemplate(string HtmlTemplateName)
        {
            var htmlFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $@"Common\HtmlTemplates\{HtmlTemplateName}.html");
            return File.ReadAllText(htmlFile);
        }

        public static byte[] GenratePdfBytes(string ResumeHtml)
        {
            var pdfBytes = (new NReco.PdfGenerator.HtmlToPdfConverter()).GeneratePdf(ResumeHtml);

            //byte[] pdfBytes;
            //var pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
            //var html = new StringReader(ResumeHtml);
            //var htmlparser = new HTMLWorker(pdfDoc);
            //using (var memoryStream = new MemoryStream())
            //{
            //    var writer = PdfWriter.GetInstance(pdfDoc, memoryStream);
            //    pdfDoc.Open();
            //    htmlparser.Parse(html);
            //    pdfDoc.Close();
            //    pdfBytes = memoryStream.ToArray();
            //}
            return pdfBytes;
        }
    }
}
